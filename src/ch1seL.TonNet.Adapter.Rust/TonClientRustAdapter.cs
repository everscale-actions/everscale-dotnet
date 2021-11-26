using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using ch1seL.TonNet.Adapter.Base;
using ch1seL.TonNet.Adapter.Rust.RustInterop;
using ch1seL.TonNet.Adapter.Rust.RustInterop.Models;
using ch1seL.TonNet.Adapter.Rust.Utils;
using ch1seL.TonNet.Client;
using ch1seL.TonNet.Client.Models;
using ch1seL.TonNet.Serialization;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Abstractions;
using Microsoft.Extensions.Options;

namespace ch1seL.TonNet.Adapter.Rust;

/// <summary>
///     Rust adapter.
/// </summary>
public class TonClientRustAdapter : TonClientAdapterBase {
	private readonly ConcurrentDictionary<uint, CallbackDelegate> _callbackDelegates =
		new ConcurrentDictionary<uint, CallbackDelegate>();

	private readonly ILogger<TonClientRustAdapter> _logger;
	private readonly IOptions<TonClientOptions> _optionsAccessor;

	public TonClientRustAdapter(IOptions<TonClientOptions> optionsAccessor, ILogger<TonClientRustAdapter> logger) :
		base(logger) {
		_optionsAccessor = optionsAccessor;
		_logger = logger;
	}

	public TonClientRustAdapter(IOptions<TonClientOptions> optionsAccessor) : base(NullLogger.Instance) {
		_optionsAccessor = optionsAccessor;
		_logger = NullLogger<TonClientRustAdapter>.Instance;
	}

	public override async ValueTask DisposeAsync() {
		RustInteropInterface.tc_destroy_context(ContextId);
		await base.DisposeAsync();
	}

	protected override Task<uint> CreateContext(CancellationToken cancellationToken) {
		string configJson =
			JsonSerializer.Serialize(_optionsAccessor.Value, JsonOptionsProvider.JsonSerializerOptions);
		_logger.LogTrace("Creating context with options: {Config}", configJson);
		using var optionsInteropJson = configJson.ToInteropStringDisposable();
		IntPtr resultPtr = RustInteropInterface.tc_create_context(optionsInteropJson);

		_logger.LogTrace("Reading context creation result");
		InteropString resultInterop = RustInteropInterface.tc_read_string(resultPtr);
		var resultJson = resultInterop.ToString();
		RustInteropInterface.tc_destroy_string(resultPtr);
		_logger.LogTrace("Got context creation result: {Result}", resultJson);

		var createContextResult =
			JsonSerializer.Deserialize<CreateContextResponse>(resultJson,
			                                                  JsonOptionsProvider.JsonSerializerOptions);
		if (createContextResult?.ContextId == null) {
			throw new TonClientException($"Raw result: {resultJson}",
			                             new NullReferenceException("Result of context creation or context number is null"));
		}
		ClientError error = createContextResult.Error;
		if (error != null) {
			throw TonClientException.CreateExceptionWithCodeWithData(error.Code,
			                                                         error.Data?.ToObject<Dictionary<string, object>>(),
			                                                         error.Message);
		}

		return Task.FromResult((uint)createContextResult.ContextId);
	}

	protected override Task RequestImpl(uint requestId, string requestJson, string method,
	                                    CancellationToken cancellationToken = default) {
		var callbackDelegate =
			new CallbackDelegate((id, json, type, finished) =>
				                     ResponseHandler(id, json.ToString(), type, finished));

		_callbackDelegates.AddOrUpdate(requestId, _ => callbackDelegate, (_, _) => callbackDelegate);

		using var methodInteropString = method.ToInteropStringDisposable();
		using var paramsJsonInteropString = requestJson.ToInteropStringDisposable();
		RustInteropInterface.tc_request(ContextId, methodInteropString, paramsJsonInteropString, requestId,
		                                callbackDelegate);
		return Task.CompletedTask;
	}

	private void ResponseHandler(uint requestId, string responseJson, uint responseType, bool finished) {
		if (finished) {
			_callbackDelegates.Remove(requestId, out _);
		}
		ResponseHandlerBase(requestId, responseJson, responseType, finished);
	}
}