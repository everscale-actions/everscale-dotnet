using System.Collections.Concurrent;
using System.Text.Json;
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
	private readonly ConcurrentDictionary<uint, (CallbackDelegate, CancellationToken)> _callbackDelegates = new();

	private readonly ILogger<EverClientRustAdapter> _logger;
	private readonly IOptions<EverClientOptions> _optionsAccessor;

	/// <inheritdoc />
	public EverClientRustAdapter(IOptions<EverClientOptions> optionsAccessor, ILogger<EverClientRustAdapter> logger) :
		base(logger) {
		_optionsAccessor = optionsAccessor;
		_logger = logger;
	}

	/// <inheritdoc />
	public EverClientRustAdapter(IOptions<EverClientOptions> optionsAccessor) : base(NullLogger.Instance) {
		_optionsAccessor = optionsAccessor;
		_logger = NullLogger<EverClientRustAdapter>.Instance;
	}

	/// <inheritdoc />
	protected override async ValueTask DisposeAsyncCore() {
		await base.DisposeAsyncCore();
		RustInteropInterface.DestroyContext(ContextId);
	}

	/// <inheritdoc />
	protected override Task<uint> CreateContext(CancellationToken cancellationToken) {
		string configJson =
			JsonSerializer.Serialize(_optionsAccessor.Value, JsonOptionsProvider.JsonSerializerOptions);
		_logger.LogTrace("Creating context with options: {Config}", configJson);
		using var optionsInteropJson = configJson.ToInteropStringDisposable();
		IntPtr resultPtr = RustInteropInterface.CreateContext(optionsInteropJson);

		_logger.LogTrace("Reading context creation result");
		InteropString resultInterop = RustInteropInterface.ReadString(resultPtr);
		var resultJson = resultInterop.ToString();
		RustInteropInterface.DestroyString(resultPtr);
		_logger.LogTrace("Got context creation result: {Result}", resultJson);

		uint contextId = GetContextIdByCreatedContextJson(resultJson);
		return Task.FromResult(contextId);
	}

	/// <inheritdoc />
	protected override Task RequestImpl(uint requestId, string requestJson, string method,
	                                    CancellationToken cancellationToken = default) {
		var callbackDelegate =
			new CallbackDelegate((id, json, type, finished) => ResponseHandler(id, json.ToString(), type, finished));

		_callbackDelegates.AddOrUpdate(requestId, _ => (callbackDelegate, cancellationToken), (_, _) => (callbackDelegate, cancellationToken));

		using var methodInteropString = method.ToInteropStringDisposable();
		using var paramsJsonInteropString = requestJson.ToInteropStringDisposable();
		RustInteropInterface.Request(ContextId, methodInteropString, paramsJsonInteropString, requestId,
		                             callbackDelegate);
		return Task.CompletedTask;
	}

	private async void ResponseHandler(uint requestId, string responseJson, uint responseType, bool finished) {
		CancellationToken cancellationToken;

		if (finished) {
			_callbackDelegates.Remove(requestId, out (CallbackDelegate _, CancellationToken cancellationToken) t);
			cancellationToken = t.cancellationToken;
		} else {
			(_, cancellationToken) = _callbackDelegates.GetValueOrDefault(requestId);
		}

		await ResponseHandlerBase(requestId, responseJson, responseType, finished, cancellationToken);
	}
}
