using System;
using System.Collections.Generic;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using ch1seL.TonNet.Abstract;
using ch1seL.TonNet.RustClient.Models;
using ch1seL.TonNet.RustClient.RustInterop;
using ch1seL.TonNet.RustClient.RustInterop.Models;
using ch1seL.TonNet.RustClient.Utils;
using ch1seL.TonNet.Serialization;
using Microsoft.Extensions.Logging;

namespace ch1seL.TonNet.RustClient
{
    //todo: must be singleton
    internal class RustTonClientCore : IRustTonClientCore, IDisposable
    {
        private readonly uint _contextNumber;
        private readonly TimeSpan _coreExecutionTimeOut = TimeSpan.FromMinutes(1);
        private readonly IDictionary<uint, CallbackDelegate> _delegatesDict = new Dictionary<uint, CallbackDelegate>();
        private readonly object _dictLock = new object();
        private readonly ILogger<RustTonClientCore> _logger;

        private uint _requestId;


        public RustTonClientCore(string optionsJson, ILogger<RustTonClientCore> logger)
        {
            _logger = logger;
            _logger.LogTrace("Creating context with options: {options}", optionsJson);
            using var optionsInteropJson = optionsJson.ToInteropStringDisposable();
            IntPtr resultPtr = RustInteropInterface.tc_create_context(optionsInteropJson);

            _logger.LogTrace("Reading context creation result");
            InteropString resultInterop = RustInteropInterface.tc_read_string(resultPtr);
            var resultJson = resultInterop.ToString();
            RustInteropInterface.tc_destroy_string(resultPtr);
            _logger.LogTrace("Got context creation result: {result}", resultJson);

            var createContextResult = JsonSerializer.Deserialize<CreateContextResponse>(resultJson, JsonOptionsProvider.JsonSerializerOptions);
            if (createContextResult?.ContextNumber == null)
                throw new TonClientException($"Raw result: {resultJson}", new NullReferenceException("Result of context creation or context number is null"));
            RustClientError error = createContextResult.Error;
            if (error != null) throw TonClientException.CreateExceptionWithCodeWithData(error.Code, error.Data, error.Message);

            _contextNumber = (uint) createContextResult.ContextNumber;
        }

        //todo: try to convert this method to IAsyncDispose
        public void Dispose()
        {
            _logger.LogTrace("Disposing context {context}", _contextNumber);

            if (!WaitForDelegates().GetAwaiter().GetResult())
                throw new TonClientException("Delegates didn't finish during the allotted time", new TimeoutException());

            RustInteropInterface.tc_destroy_context(_contextNumber);
            _logger.LogTrace("Context {context} disposed", _contextNumber);
        }

        public async Task<string> Request<TEvent>(string method, string requestJson, Action<TEvent, uint> callback = null,
            CancellationToken cancellationToken = default)
        {
            _requestId = _requestId == uint.MaxValue ? 0 : _requestId + 1;

            var cts = new TaskCompletionSource<string>();

            var callbackDelegate = new CallbackDelegate((requestId, responseInteropString, responseType, finished) =>
            {
                var responseJson = responseInteropString.ToString();
                _logger.LogTrace("Got request response type:{responseType} body:{response}", ((ResponseType) responseType).ToString(), responseJson);

                lock (_dictLock)
                {
                    if (!_delegatesDict.ContainsKey(requestId))
                        _logger.LogError("RequestId was not found in this context");
                    // cts.SetException(new TonClientException($"Request {requestId} not found in context {_contextNumber}"));
                }

                switch ((ResponseType) responseType)
                {
                    case ResponseType.Success:
                        RemoveDelegateFromDict(requestId);
                        cts.SetResult(responseJson);
                        break;
                    case ResponseType.Nop:
                        break;
                    case ResponseType.Error:
                        RemoveDelegateFromDict(requestId);
                        var errorResponse = JsonSerializer.Deserialize<RustClientError>(responseJson);
                        TonClientException exception = errorResponse == null
                            ? new TonClientException($"Raw result: {responseJson}", new NullReferenceException("Result of error response is null"))
                            : TonClientException.CreateExceptionWithCodeWithData(errorResponse.Code, errorResponse.Data, errorResponse.Message);
                        cts.SetException(exception);
                        break;
                    default:
                        _logger.LogTrace($"Sending callback {typeof(TEvent).Name} by request:{requestId}");
                        try
                        {
                            callback?.Invoke(TonEventSerializer.DeserializeEvent<TEvent>(responseJson), responseType);
                        }
                        catch (Exception ex)
                        {
                            cts.SetException(new TonClientException("Callback invoke exception", ex));
                        }

                        break;
                }
            });


            lock (_dictLock)
            {
                _delegatesDict.Add(_requestId, callbackDelegate);
            }

            try
            {
                _logger.LogTrace("Sending request method:{method} request:{request}", method, requestJson);
                using var methodInteropString = method.ToInteropStringDisposable();
                using var paramsJsonInteropString = requestJson.ToInteropStringDisposable();
                RustInteropInterface.tc_request(_contextNumber, methodInteropString, paramsJsonInteropString, _requestId, callbackDelegate);
            }
            catch (Exception ex)
            {
                RemoveDelegateFromDict(_requestId);
                throw new TonClientException("Sending request error", ex);
            }

            Task executeOrTimeout = await Task.WhenAny(cts.Task, Task.Delay(_coreExecutionTimeOut, cancellationToken));
            if (cts.Task == executeOrTimeout) return await cts.Task;
            throw new TonClientException("Execution timeout expired or cancellation request");
        }

        private async Task<bool> WaitForDelegates()
        {
            //todo: use cts instead of Task.WaitAny  
            var cts = new CancellationTokenSource(TimeSpan.FromSeconds(30));
            while (true)
            {
                try
                {
                    await Task.Delay(TimeSpan.FromSeconds(1), cts.Token);
                }
                catch (TaskCanceledException)
                {
                    return false;
                }

                lock (_dictLock)
                {
                    if (_delegatesDict.Count == 0) return true;
                    _logger.LogWarning("Some delegates not finished: {count} wait...", _delegatesDict.Count);
                }
            }
        }

        private void RemoveDelegateFromDict(uint requestId)
        {
            lock (_dictLock)
            {
                _delegatesDict.Remove(requestId);
            }
        }
    }
}