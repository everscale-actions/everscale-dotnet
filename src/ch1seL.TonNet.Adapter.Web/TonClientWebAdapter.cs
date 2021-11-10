using System;
using System.Collections.Generic;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using ch1seL.TonNet.Adapter.Base;
using ch1seL.TonNet.Client;
using ch1seL.TonNet.Client.Models;
using ch1seL.TonNet.Serialization;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.JSInterop;

namespace ch1seL.TonNet.Adapter.Web
{
    public class TonClientWebAdapter : TonClientAdapterBase
    {
        private readonly IJSRuntime _jsRuntime;
        private readonly ILogger<TonClientWebAdapter> _logger;
        private readonly IOptions<TonClientOptions> _optionsAccessor;
        private IJSObjectReference? _libWeb;

        public TonClientWebAdapter(IJSRuntime jsRuntime, IOptions<TonClientOptions> optionsAccessor,
            ILogger<TonClientWebAdapter> logger) : base(logger)
        {
            _jsRuntime = jsRuntime;
            _optionsAccessor = optionsAccessor;
            _logger = logger;
        }

        protected override async Task RequestImpl(uint requestId, string requestJson,
            string method,
            CancellationToken cancellationToken = default)
        {
            await (_libWeb ?? throw new InvalidOperationException()).InvokeVoidAsync("sendRequest", cancellationToken,
                ContextId,
                requestId,
                method,
                requestJson);
        }

        public override async ValueTask DisposeAsync()
        {
            if (_libWeb is not null)
            {
                await _libWeb.InvokeAsync<string>("destroyContext", ContextId);
                await _libWeb.DisposeAsync();
            }
        }

        protected override async Task<uint> CreateContext(CancellationToken cancellationToken)
        {
            var module = await _jsRuntime.InvokeAsync<IJSObjectReference>("import", cancellationToken,
                "/_content/ch1seL.TonNet.Adapter.Web/js/tonclient-adapter.js");
            _libWeb = await module.InvokeAsync<IJSObjectReference>("init", cancellationToken, DotNetObjectReference.Create(this));

            var configJson =
                JsonSerializer.Serialize(_optionsAccessor.Value, JsonOptionsProvider.JsonSerializerOptions);
            _logger.LogTrace("Creating context with options: {Config}", configJson);
            var resultJson =
                await _libWeb.InvokeAsync<string>("createContext", cancellationToken, configJson);

            var createContextResult =
                JsonSerializer.Deserialize<CreateContextResponse>(resultJson,
                    JsonOptionsProvider.JsonSerializerOptions);
            ClientError? error = createContextResult?.Error;
            if (error != null)
                throw TonClientException.CreateExceptionWithCodeWithData(error.Code,
                    error.Data?.ToObject<Dictionary<string, object>>(),
                    error.Message);
            if (createContextResult?.ContextId == null)
                throw new TonClientException($"Raw result: {resultJson}",
                    new NullReferenceException("Result of context creation or context number is null"));

            return (uint)createContextResult.ContextId;
        }

        [JSInvokable]
        public void ResponseHandler(uint requestId, string responseJson, uint responseType, bool finished)
        {
            ResponseHandlerBase(requestId, responseJson, responseType, finished);
        }
    }
}