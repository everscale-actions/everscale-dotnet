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
using Microsoft.Extensions.Logging;

namespace ch1seL.TonNet.RustClient
{
    //todo: must be singleton
    internal class RustTonClientCore : IRustTonClientCore, IDisposable
    {
        public static readonly JsonSerializerOptions JsonSerializerOptions = new JsonSerializerOptions {IgnoreNullValues = true, MaxDepth = int.MaxValue};

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

            var createContextResult = JsonSerializer.Deserialize<CreateContextResult>(resultJson, JsonSerializerOptions);
            if (createContextResult?.Error != null) throw new TonClientException(createContextResult.Error);
            _contextNumber = createContextResult?.ContextNumber ?? throw new NullReferenceException("Context creation result is null");
        }

        public void Dispose()
        {
            _logger.LogTrace("Disposing context {context}", _contextNumber);

            // some questionable logic here to wait all handler finished work
            Task waitForDelegatesFinishedTask = WaitForDelegates();
            Task res = Task.WhenAny(waitForDelegatesFinishedTask, Task.Delay(TimeSpan.FromSeconds(30))).GetAwaiter().GetResult();
            if (res != waitForDelegatesFinishedTask) throw new TonClientException("Delegates didn't finish in time");

            RustInteropInterface.tc_destroy_context(_contextNumber);
            _logger.LogTrace("Context {context} disposed", _contextNumber);
        }

        public async Task<string> Request<TEvent>(string method, string requestJson, Action<TEvent> callback = null,
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
                    {
                        _logger.LogError("RequestId was not found in this context");
                        cts.SetException(new TonClientException($"Request {requestId} not found in context {_contextNumber}"));
                    }
                }

                switch ((ResponseType) responseType)
                {
                    case ResponseType.Success:
                    case ResponseType.Nop when finished:
                        RemoveDelegateFromDict<TEvent>(requestId);
                        cts.SetResult(responseJson);
                        break;
                    case ResponseType.Error:
                        RemoveDelegateFromDict<TEvent>(requestId);
                        // todo: Deserialize error response
                        cts.SetException(new TonClientException(responseJson));
                        break;
                    default:
                        _logger.LogTrace($"Sending callback {typeof(TEvent).Name} by request:{requestId}");
                        try
                        {
                            callback?.Invoke(JsonSerializer.Deserialize<TEvent>(requestJson, JsonSerializerOptions));
                        }
                        catch (Exception ex)
                        {
                            cts.SetException(new TonClientException("Callback invoke exception", ex));
                        }

                        cts.SetResult(null);
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
                RemoveDelegateFromDict<TEvent>(_requestId);
                throw new TonClientException("Sending request error", ex);
            }

            Task executeOrTimeout = await Task.WhenAny(cts.Task, Task.Delay(_coreExecutionTimeOut, cancellationToken));
            if (cts.Task == executeOrTimeout) return await cts.Task;
            throw new TonClientException("Execution timeout expired or cancellation request");
        }

        private async Task WaitForDelegates()
        {
            while (true)
            {
                await Task.Delay(TimeSpan.FromSeconds(1));

                lock (_dictLock)
                {
                    if (_delegatesDict.Count == 0) return;

                    _logger.LogWarning("Some delegates not finished: {count}", _delegatesDict.Count);
                }
            }
        }

        private void RemoveDelegateFromDict<TEvent>(uint requestId)
        {
            lock (_dictLock)
            {
                _delegatesDict.Remove(requestId);
            }
        }
    }
}