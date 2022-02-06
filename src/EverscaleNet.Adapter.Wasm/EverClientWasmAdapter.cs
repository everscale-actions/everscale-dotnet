using System;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using EverscaleNet.Adapter.Base;
using EverscaleNet.Models;
using EverscaleNet.Serialization;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.JSInterop;

namespace EverscaleNet.Adapter.Wasm;

/// <inheritdoc />
public class EverClientWasmAdapter : EverClientAdapterBase {
	private readonly IJSRuntime _jsRuntime;
	private readonly ILogger<EverClientWasmAdapter> _logger;
	private readonly IOptions<EverClientOptions> _optionsAccessor;
	private IJSObjectReference? _libWeb;

	/// <inheritdoc />
	public EverClientWasmAdapter(IJSRuntime jsRuntime, IOptions<EverClientOptions> optionsAccessor,
	                             ILogger<EverClientWasmAdapter> logger) : base(logger) {
		_jsRuntime = jsRuntime;
		_optionsAccessor = optionsAccessor;
		_logger = logger;
	}

	/// <inheritdoc />
	public override async ValueTask DisposeAsync() {
		if (_libWeb is not null) {
			await _libWeb.InvokeAsync<string>("destroyContext", ContextId);
			await _libWeb.DisposeAsync();
		}
	}

	/// <summary>
	///     Method called by js client
	/// </summary>
	/// <param name="requestId"></param>
	/// <param name="responseJson"></param>
	/// <param name="responseType"></param>
	/// <param name="finished"></param>
	[JSInvokable]
	public void ResponseHandler(uint requestId, string responseJson, uint responseType, bool finished) {
		ResponseHandlerBase(requestId, responseJson, responseType, finished);
	}

	/// <inheritdoc />
	protected override async Task RequestImpl(uint requestId, string requestJson,
	                                          string method,
	                                          CancellationToken cancellationToken = default) {
		await (_libWeb ?? throw new InvalidOperationException()).InvokeVoidAsync("sendRequest", cancellationToken,
		                                                                         ContextId,
		                                                                         requestId,
		                                                                         method,
		                                                                         requestJson);
	}

	/// <inheritdoc />
	protected override async Task<uint> CreateContext(CancellationToken cancellationToken) {
		var module = await _jsRuntime.InvokeAsync<IJSObjectReference>("import", cancellationToken,
		                                                              "/_content/EverscaleNet.Adapter.Wasm/js/tonclient-adapter.js");
		_libWeb = await module.InvokeAsync<IJSObjectReference>("init", cancellationToken, DotNetObjectReference.Create(this));

		string configJson =
			JsonSerializer.Serialize(_optionsAccessor.Value, JsonOptionsProvider.JsonSerializerOptions);
		_logger.LogTrace("Creating context with options: {Config}", configJson);
		var resultJson =
			await _libWeb.InvokeAsync<string>("createContext", cancellationToken, configJson);

		return GetContextIdByCreatedContextJson(resultJson);
	}
}
