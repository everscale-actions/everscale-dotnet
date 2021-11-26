using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using ch1seL.TonNet.Abstract;
using ch1seL.TonNet.Client;
using ch1seL.TonNet.Serialization;
using Microsoft.Extensions.Logging;

namespace ch1seL.TonNet.Adapter.Base;

public abstract class TonClientAdapterBase : ITonClientAdapter {
	private const string EmptyJson = "{}";
	private static readonly TimeSpan CoreExecutionTimeOut = TimeSpan.FromMinutes(1);

	protected uint ContextId;
	private readonly object _lock = new object();
	private readonly ILogger _logger;

	private readonly ConcurrentDictionary<uint, (TaskCompletionSource<string>, Action<string, uint>)>
		_requestsDict = new ConcurrentDictionary<uint, (TaskCompletionSource<string>, Action<string, uint>)>();

	private bool _initComplete;
	private uint _requestId;

	protected TonClientAdapterBase(ILogger logger) {
		_logger = logger;
	}

	private static Action<string, uint> DeserializeCallback<TEvent>(Action<TEvent, uint> callback) {
		return (callbackResponseJson, responseType) => { callback?.Invoke(PolymorphicSerializer.Deserialize<TEvent>(callbackResponseJson), responseType); };
	}

	public virtual async ValueTask DisposeAsync() {
		bool waitDelegatesResult = await WaitForDelegates();
		if (!waitDelegatesResult) {
			_logger.LogError("Delegates didn't finish during the allotted time. Force clean...");
			_requestsDict.Clear();
		}

		_logger.LogTrace("Context {Context} disposed", ContextId);
	}

	public async Task Request(string method, CancellationToken cancellationToken = default) {
		await Request(method, EmptyJson, null, cancellationToken);
	}

	public async Task<TResponse> Request<TResponse>(string method, CancellationToken cancellationToken = default)
		where TResponse : new() {
		string responseJson = await Request(method, EmptyJson, null, cancellationToken);

		return JsonSerializer.Deserialize<TResponse>(responseJson, JsonOptionsProvider.JsonSerializerOptions);
	}

	public async Task<TResponse> Request<TResponse, TEvent>(string method, Action<TEvent, uint> callback,
	                                                        CancellationToken cancellationToken = default) where TResponse : new() {
		string responseJson = await Request(method, EmptyJson, DeserializeCallback(callback), cancellationToken);

		return JsonSerializer.Deserialize<TResponse>(responseJson, JsonOptionsProvider.JsonSerializerOptions);
	}

	public async Task<TResponse> Request<TRequest, TResponse, TEvent>(string method, TRequest request,
	                                                                  Action<TEvent, uint> callback,
	                                                                  CancellationToken cancellationToken = default) where TRequest : new() where TResponse : new() {
		string requestJson = JsonSerializer.Serialize(request, JsonOptionsProvider.JsonSerializerOptions);

		string responseJson = await Request(method, requestJson, DeserializeCallback(callback), cancellationToken);

		return JsonSerializer.Deserialize<TResponse>(responseJson, JsonOptionsProvider.JsonSerializerOptions);
	}

	public async Task Request<TRequest>(string method, TRequest request,
	                                    CancellationToken cancellationToken = default) where TRequest : new() {
		string requestJson = JsonSerializer.Serialize(request, JsonOptionsProvider.JsonSerializerOptions);

		await Request(method, requestJson, null, cancellationToken);
	}

	public async Task<TResponse> Request<TRequest, TResponse>(string method, TRequest request,
	                                                          CancellationToken cancellationToken = default) where TRequest : new() where TResponse : new() {
		string requestJson = JsonSerializer.Serialize(request, JsonOptionsProvider.JsonSerializerOptions);

		string responseJson = await Request(method, requestJson, null, cancellationToken);

		return JsonSerializer.Deserialize<TResponse>(responseJson, JsonOptionsProvider.JsonSerializerOptions);
	}

	protected abstract Task<uint> CreateContext(CancellationToken cancellationToken);

	protected abstract Task RequestImpl(uint requestId, string requestJson, string method,
	                                    CancellationToken cancellationToken = default);

	protected void ResponseHandlerBase(uint requestId, string responseJson, uint responseType, bool finished) {
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

		if (!_requestsDict.TryGetValue(requestId, out (TaskCompletionSource<string>, Action<string, uint>) tuple)) {
			throw new TonClientException("Request not found") {
				Data = {
					{ "ContextId", ContextId },
					{ "RequestId", requestId }
				}
			};
		}

		if (finished) {
			_requestsDict.Remove(requestId, out _);
		}

		(TaskCompletionSource<string> tcs, Action<string, uint> callback) = tuple;

		switch ((ResponseType)responseType) {
			case ResponseType.Success:
				tcs.SetResult(responseJson);
				return;
			case ResponseType.Error:
				TonClientException exception = TonExceptionSerializer.GetTonClientExceptionByResponse(responseJson);
				tcs.SetException(exception);
				return;
			// do nothing
			case ResponseType.Nop:
				return;
			default:
				// it is callback if responseType>=3 
				_logger.LogTrace("Sending callback context:{Context} request:{Request} body:{Body}", ContextId,
				                 requestId, responseJson);
				callback?.Invoke(responseJson, responseType);
				return;
		}
	}

	private async Task<bool> WaitForDelegates() {
		// wait 30 seconds for all work to be completed
		var cts = new CancellationTokenSource(TimeSpan.FromSeconds(30));
		while (true) {
			lock (_lock) {
				if (_requestsDict.Count == 0) {
					return true;
				}
				_logger.LogWarning("Some delegates not finished: {Count} wait...", _requestsDict.Count);
			}

			try {
				await Task.Delay(TimeSpan.FromSeconds(1), cts.Token);
			} catch (TaskCanceledException) {
				return false;
			}
		}
	}

	private async Task<string> Request(string method, string requestJson,
	                                   Action<string, uint> callback = null,
	                                   CancellationToken cancellationToken = default) {
		if (!_initComplete) {
			ContextId = await CreateContext(cancellationToken);
			_initComplete = true;
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
		} catch (Exception e) {
			throw new TonClientException("Sending request error", e);
		}

		Task executeOrTimeout = await Task.WhenAny(tcs.Task, Task.Delay(CoreExecutionTimeOut, cancellationToken));
		if (tcs.Task == executeOrTimeout) {
			return await tcs.Task;
		}

		throw new TonClientException("Execution timeout expired or cancellation requested");
	}
}