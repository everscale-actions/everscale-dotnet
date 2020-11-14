using System;
using System.Text.Json;
using System.Threading.Tasks;
using ch1seL.TonClientDotnet.Models;
using ch1seL.TonClientDotnet.RustInterop;
using ch1seL.TonClientDotnet.RustInterop.Models;
using ch1seL.TonClientDotnet.Utils;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace ch1seL.TonClientDotnet
{
    internal class RustTonClientCore : IRustTonClientCore, IDisposable
    {
        public static readonly JsonSerializerOptions JsonSerializerOptions = new JsonSerializerOptions
            {IgnoreNullValues = true, MaxDepth = int.MaxValue};

        private readonly uint _contextNumber;
        private readonly TimeSpan _coreExecutionTimeOut = TimeSpan.FromMinutes(1);
        private CallbackDelegate _callbackDelegate;
        private readonly ILogger<RustTonClientCore> _logger;

        private uint _requestId;

        public RustTonClientCore(IOptions<TonClientOptions> options, ILogger<RustTonClientCore> logger)
        {
            _logger = logger;
            var optionsJson = JsonSerializer.Serialize(options, JsonSerializerOptions);
            
            _logger.LogTrace("Creating context: {options}", optionsJson);
            using var optionsInteropJson = optionsJson.ToInteropStringDisposable();
            IntPtr resultPtr = RustInteropInterface.tc_create_context(optionsInteropJson);
            _logger.LogTrace("Reading context creation result");
            InteropString resultInterop = RustInteropInterface.tc_read_string(resultPtr);
            RustInteropInterface.tc_destroy_string(resultPtr);
            var resultJson = resultInterop.ToString();
            var createContextResult = JsonSerializer.Deserialize<CreateContextResult>(resultJson, JsonSerializerOptions);
            _logger.LogTrace("Context creation result: {result}", createContextResult);
            if (createContextResult.Error != null) throw new TonClientException(createContextResult.Error);
            _contextNumber = createContextResult.ContextNumber;
        }

        public void Dispose()
        {
            _logger.LogTrace("Disposing context {context}");
            RustInteropInterface.tc_destroy_context(_contextNumber);
        }

        public async Task<string> Request(string method, string paramsJson)
        {
            _requestId = _requestId == uint.MaxValue ? 0 : _requestId++;
            _logger.LogTrace("Init request {context}", _contextNumber);
            
            var cts = new TaskCompletionSource<string>();
            _callbackDelegate = (requestId, responseInteropString, responseType, finished) =>
            {
                var responseJson = responseInteropString.ToString();
                
                _logger.LogTrace("Got request response {context} {response}", _contextNumber, responseJson);
                
                switch ((ResponseType) responseType)
                {
                    case ResponseType.Success:
                        cts.SetResult(responseJson);
                        break;
                    case ResponseType.Error:
                        // todo: Deserialize error response
                        cts.SetException(new TonClientException(responseJson));
                        break;
                    default:
                        cts.SetException(new ArgumentOutOfRangeException(nameof(responseType), responseType, null));
                        break;
                }
            };

            _logger.LogTrace("Sending request {context} {method} {request}", _contextNumber, method, paramsJson);
            
            using var methodInteropString = method.ToInteropStringDisposable();
            using var paramsJsonInteropString = paramsJson.ToInteropStringDisposable();
            RustInteropInterface.tc_request(_contextNumber, methodInteropString, paramsJsonInteropString, _requestId, _callbackDelegate);

            Task executeOrTimeout = await Task.WhenAny(cts.Task, Task.Delay(_coreExecutionTimeOut));
            if (cts.Task == executeOrTimeout) return await cts.Task;
            throw new TonClientException("Execution timeout expired");
        }
    }
}