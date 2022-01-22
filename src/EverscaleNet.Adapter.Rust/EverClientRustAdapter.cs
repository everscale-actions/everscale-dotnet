using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using EverscaleNet.Adapter.Base;
using EverscaleNet.Adapter.Rust.RustInterop;
using EverscaleNet.Adapter.Rust.RustInterop.Models;
using EverscaleNet.Adapter.Rust.Utils;
using EverscaleNet.Models;
using EverscaleNet.Serialization;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Abstractions;
using Microsoft.Extensions.Options;

namespace EverscaleNet.Adapter.Rust;

/// <summary>
///     Rust adapter.
/// </summary>
public class EverClientRustAdapter : EverClientAdapterBase {
	private readonly ConcurrentDictionary<uint, CallbackDelegate> _callbackDelegates = new();

	private readonly ILogger<EverClientRustAdapter> _logger;
	private readonly IOptions<EverClientOptions> _optionsAccessor;

	public EverClientRustAdapter(IOptions<EverClientOptions> optionsAccessor, ILogger<EverClientRustAdapter> logger) :
		base(logger) {
		_optionsAccessor = optionsAccessor;
		_logger = logger;
	}

	public EverClientRustAdapter(IOptions<EverClientOptions> optionsAccessor) : base(NullLogger.Instance) {
		_optionsAccessor = optionsAccessor;
		_logger = NullLogger<EverClientRustAdapter>.Instance;
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

		uint contextId = GetContextIdByJson(resultJson);
		return Task.FromResult(contextId);
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