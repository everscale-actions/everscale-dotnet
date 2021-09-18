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
            await _jsRuntime.InvokeAsync<string>("jsAdapter.sendRequest", cancellationToken,
                ContextId,
                requestId,
                method,
                requestJson);
        }

        public override async ValueTask DisposeAsync()
        {
            await _jsRuntime.InvokeAsync<string>("jsAdapter", "destroyContext", ContextId);

            await base.DisposeAsync();
        }

        protected override async Task<uint> CreateContext(CancellationToken cancellationToken)
        {
            var configJson =
                JsonSerializer.Serialize(_optionsAccessor.Value, JsonOptionsProvider.JsonSerializerOptions);
            _logger.LogTrace("Creating context with options: {Config}", configJson);
            var resultJson =
                await _jsRuntime.InvokeAsync<string>("jsAdapter.createContext", cancellationToken, configJson);

            var createContextResult =
                JsonSerializer.Deserialize<CreateContextResponse>(resultJson,
                    JsonOptionsProvider.JsonSerializerOptions);
            ClientError error = createContextResult?.Error;
            if (error != null)
                throw TonClientException.CreateExceptionWithCodeWithData(error.Code,
                    error.Data?.ToObject<Dictionary<string, object>>(),
                    error.Message);
            if (createContextResult?.ContextId == null)
                throw new TonClientException($"Raw result: {resultJson}",
                    new NullReferenceException("Result of context creation or context number is null"));
            await _jsRuntime.InvokeVoidAsync("setAdapterResponseHandler", cancellationToken,
                DotNetObjectReference.Create(this));

            return (uint)createContextResult.ContextId;
        }

        [JSInvokable]
        public void ResponseHandler(uint requestId, string responseJson, uint responseType, bool finished)
        {
            ResponseHandlerBase(requestId, responseJson, responseType, finished);
        }
    }
}