using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using EverscaleNet.Abstract;
using EverscaleNet.Client.Models;
using EverscaleNet.Models;
using EverscaleNet.Serialization;
using Microsoft.Extensions.Logging;

namespace EverscaleNet.Adapter.Base;

/// <inheritdoc />
public abstract class EverClientAdapterBase : IEverClientAdapter {
	private const string EmptyJson = "{}";
	private static readonly TimeSpan CoreExecutionTimeOut = TimeSpan.FromMinutes(5);
	private readonly object _lock = new();
	private readonly ILogger _logger;

	private readonly ConcurrentDictionary<uint, (TaskCompletionSource<string> tsc, Func<string, uint, Task>? callback)> _requestsDict = new();
	private readonly SemaphoreSlim _semaphore = new(1, 1);

	private uint _requestId;

	/// <summary>
	///     Current context id
	/// </summary>
	protected uint ContextId;

	/// <summary>
	///     Adapter .ctor
	/// </summary>
	/// <param name="logger"></param>
	protected EverClientAdapterBase(ILogger logger) {
		_logger = logger;
	}

	/// <inheritdoc />
	public async ValueTask DisposeAsync() {
		await DisposeAsyncCore().ConfigureAwait(false);
		GC.SuppressFinalize(this);
	}

	/// <inheritdoc />
	public async Task Request(string method, CancellationToken cancellationToken = default) {
		await Request(method, EmptyJson, null, cancellationToken);
	}

	/// <inheritdoc />
	public async Task<TResponse> Request<TResponse>(string method, CancellationToken cancellationToken = default)
		where TResponse : new() {
		string responseJson = await Request(method, EmptyJson, null, cancellationToken);

		return JsonSerializer.Deserialize<TResponse>(responseJson, JsonOptionsProvider.JsonSerializerOptions)!;
	}

	/// <inheritdoc />
	public async Task<TResponse> Request<TResponse, TEvent>(string method, Func<TEvent, uint, Task>? callback,
	                                                        CancellationToken cancellationToken = default) where TResponse : new() {
		string responseJson = await Request(method, EmptyJson, callback is null ? null : DeserializeCallback(callback), cancellationToken);

		return JsonSerializer.Deserialize<TResponse>(responseJson, JsonOptionsProvider.JsonSerializerOptions)!;
	}

	/// <inheritdoc />
	public async Task<TResponse> Request<TRequest, TResponse, TEvent>(string method, TRequest request,
	                                                                  Func<TEvent, uint, Task>? callback,
	                                                                  CancellationToken cancellationToken = default) where TRequest : new() where TResponse : new() {
		string requestJson = JsonSerializer.Serialize(request, JsonOptionsProvider.JsonSerializerOptions);

		string responseJson = await Request(method, requestJson, callback is null ? null : DeserializeCallback(callback), cancellationToken);

		return JsonSerializer.Deserialize<TResponse>(responseJson, JsonOptionsProvider.JsonSerializerOptions)!;
	}

	/// <inheritdoc />
	public async Task Request<TRequest>(string method, TRequest request,
	                                    CancellationToken cancellationToken = default) where TRequest : new() {
		string requestJson = JsonSerializer.Serialize(request, JsonOptionsProvider.JsonSerializerOptions);

		await Request(method, requestJson, null, cancellationToken);
	}

	/// <inheritdoc />
	public async Task<TResponse> Request<TRequest, TResponse>(string method, TRequest request,
	                                                          CancellationToken cancellationToken = default) where TRequest : new() where TResponse : new() {
		string requestJson = JsonSerializer.Serialize(request, JsonOptionsProvider.JsonSerializerOptions);

		string responseJson = await Request(method, requestJson, null, cancellationToken);

		return JsonSerializer.Deserialize<TResponse>(responseJson, JsonOptionsProvider.JsonSerializerOptions)!;
	}

	/// <summary>
	///     Wait for delegates finished their work
	/// </summary>
	protected virtual async ValueTask DisposeAsyncCore() {
		bool waitDelegatesResult = await WaitForDelegates();
		if (!waitDelegatesResult) {
			_logger.LogError("Delegates didn't finish during the allotted time. Force clean...");
			_requestsDict.Clear();
		}
	}

	/// <summary>
	///     Deserialize CreateContextResponse from json and return context id
	/// </summary>
	/// <param name="json">CreateContextResponse json string</param>
	/// <returns>Context Id</returns>
	/// <exception cref="EverClientException"></exception>
	protected static uint GetContextIdByCreatedContextJson(string json) {
		var createContextResult = JsonSerializer.Deserialize<CreateContextResponse>(json, JsonOptionsProvider.JsonSerializerOptions);
		ClientError? error = createContextResult?.Error;
		if (error != null) {
			throw EverClientException.CreateExceptionWithCodeWithData(error.Code,
			                                                          error.Data?.ToObject<Dictionary<string, object>>(),
			                                                          error.Message);
		}
		if (createContextResult?.ContextId == null) {
			throw new EverClientException($"Raw result: {json}", new NullReferenceException("Result of context creation or context number is null"));
		}
		return (uint)createContextResult.ContextId;
	}

	private static Func<string, uint, Task> DeserializeCallback<TEvent>(Func<TEvent, uint, Task> callback) {
		return (json, responseType) => callback.Invoke(PolymorphicSerializer.Deserialize<TEvent>(JsonDocument.Parse(json).RootElement), responseType);
	}

	/// <summary>
	///     Create context method
	/// </summary>
	/// <param name="cancellationToken"></param>
	/// <returns>Created context Id</returns>
	protected abstract Task<uint> CreateContext(CancellationToken cancellationToken);

	/// <summary>
	///     Raw request method implementation
	/// </summary>
	/// <param name="requestId"></param>
	/// <param name="requestJson"></param>
	/// <param name="method"></param>
	/// <param name="cancellationToken"></param>
	/// <returns></returns>
	protected abstract Task RequestImpl(uint requestId, string requestJson, string method,
	                                    CancellationToken cancellationToken = default);

	/// <summary>
	///     Raw response handler implementation
	/// </summary>
	/// <param name="requestId"></param>
	/// <param name="responseJson"></param>
	/// <param name="responseType"></param>
	/// <param name="finished"></param>
	/// <exception cref="EverClientException"></exception>
	protected async Task ResponseHandlerBase(uint requestId, string responseJson, uint responseType, bool finished) {
		_logger.LogTrace(
			"Got request response context:{Context} request:{Request} type:{ResponseType} finished:{Finished} body:{Body}",
			ContextId,
			requestId, ((ResponseType)responseType).ToString(), finished, responseJson);
		{
			if (!_requestsDict.ContainsKey(requestId)) {
				_logger.LogWarning("Request {Request} was not found in this context {Context}", requestId,
				                   ContextId);
				return;
			}
		}

		if (!_requestsDict.TryGetValue(requestId, out (TaskCompletionSource<string> tsc, Func<string, uint, Task>? callback) request)) {
			throw new EverClientException("Request not found") {
				Data = {
					{ "ContextId", ContextId },
					{ "RequestId", requestId }
				}
			};
		}

		if (finished) {
			_requestsDict.Remove(requestId, out _);
		}

		switch ((ResponseType)responseType) {
			case ResponseType.Success:
				request.tsc.SetResult(responseJson);
				return;
			case ResponseType.Error:
				EverClientException exception = EverExceptionSerializer.GetEverClientExceptionByResponse(responseJson);
				request.tsc.SetException(exception);
				return;
			// do nothing
			case ResponseType.Nop:
				return;
			case ResponseType.AppRequest:
			case ResponseType.AppNotify:
			case ResponseType.Custom:
			default:
				// it is callback if responseType>=3 
				_logger.LogTrace("Sending callback context:{Context} request:{Request} body:{Body}", ContextId,
				                 requestId, responseJson);
				if (request.callback != null) {
					await request.callback(responseJson, responseType);
				}
				return;
		}
	}

	private async Task<bool> WaitForDelegates() {
		// wait 30 seconds for all work to be completed
		var cts = new CancellationTokenSource(TimeSpan.FromSeconds(30));
		while (true) {
			lock (_lock) {
				if (_requestsDict.IsEmpty) {
					return true;
				}
				_logger.LogWarning("Some delegates not finished: {Count} wait...", _requestsDict.Count);
			}

			try {
				await Task.Delay(TimeSpan.FromSeconds(1), cts.Token).ConfigureAwait(false);
			} catch (TaskCanceledException) {
				return false;
			}
		}
	}

	private async Task<string> Request(string method, string requestJson,
	                                   Func<string, uint, Task>? callback = null,
	                                   CancellationToken cancellationToken = default) {
		await _semaphore.WaitAsync(cancellationToken);
		try {
			if (ContextId == 0) {
				ContextId = await CreateContext(cancellationToken);
			}
		} finally {
			_semaphore.Release();
		}

		uint requestId;
		var tcs = new TaskCompletionSource<string>();
		lock (_lock) {
			_requestId = _requestId == uint.MaxValue ? 0 : _requestId + 1;
			requestId = _requestId;
			_requestsDict.AddOrUpdate(requestId, _ => (tcs, callback), (_, _) => (tcs, callback));
		}

		_logger.LogTrace("Sending request context:{Context} request:{Request} method:{Method} body:{Body}",
		                 ContextId, requestId, method, requestJson);

		try {
			await RequestImpl(requestId, requestJson, method, cancellationToken);
		} catch (Exception? e) {
			throw new EverClientException("Sending request error", e);
		}

		Task executeOrTimeout = await Task.WhenAny(tcs.Task, Task.Delay(CoreExecutionTimeOut, cancellationToken));
		if (tcs.Task == executeOrTimeout) {
			return await tcs.Task;
		}

		throw new EverClientException("Execution timeout expired or cancellation requested");
	}
}
