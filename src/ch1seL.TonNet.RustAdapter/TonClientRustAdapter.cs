using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using ch1seL.TonNet.Abstract;
using ch1seL.TonNet.Client;
using ch1seL.TonNet.Client.Models;
using ch1seL.TonNet.RustAdapter.Models;
using ch1seL.TonNet.RustAdapter.RustInterop;
using ch1seL.TonNet.RustAdapter.RustInterop.Models;
using ch1seL.TonNet.RustAdapter.Utils;
using ch1seL.TonNet.Serialization;
using Microsoft.Extensions.Logging;

// todo: fix InconsistentLogPropertyNaming inspection
// ReSharper disable InconsistentLogPropertyNaming

namespace ch1seL.TonNet.RustAdapter
{
    /// <summary>
    ///     Rust adapter. Uses RustTonClientCore to get serialized responses by serialized requests from TON SDK
    /// </summary>
    public class TonClientRustAdapter : ITonClientAdapter, ITonClientRustAdapter
    {
        private readonly uint _contextNumber;
        private readonly TimeSpan _coreExecutionTimeOut = TimeSpan.FromMinutes(1);
        private readonly ConcurrentDictionary<uint, CallbackDelegate> _dictionary = new ConcurrentDictionary<uint, CallbackDelegate>();
        private readonly object _lock = new object();
        private readonly ILogger<TonClientRustAdapter> _logger;
        private uint _requestId;

        public TonClientRustAdapter(TonClientOptions options, ILogger<TonClientRustAdapter> logger)
        {
            _logger = logger;
            var configJson = JsonSerializer.Serialize(options, JsonOptionsProvider.JsonSerializerOptions);
            _logger.LogTrace("Creating context with options: {config}", configJson);
            using var optionsInteropJson = configJson.ToInteropStringDisposable();
            IntPtr resultPtr = RustInteropInterface.tc_create_context(optionsInteropJson);

            _logger.LogTrace("Reading context creation result");
            InteropString resultInterop = RustInteropInterface.tc_read_string(resultPtr);
            var resultJson = resultInterop.ToString();
            RustInteropInterface.tc_destroy_string(resultPtr);
            _logger.LogTrace("Got context creation result: {result}", resultJson);

            var createContextResult = JsonSerializer.Deserialize<CreateContextResponse>(resultJson, JsonOptionsProvider.JsonSerializerOptions);
            if (createContextResult?.ContextNumber == null)
                throw new TonClientException($"Raw result: {resultJson}", new NullReferenceException("Result of context creation or context number is null"));
            ClientError error = createContextResult.Error;
            if (error != null)
                throw TonClientException.CreateExceptionWithCodeWithData(error.Code, error.Data?.ToObject<Dictionary<string, object>>(), error.Message);

            _contextNumber = (uint)createContextResult.ContextNumber;
        }

        public async Task<TResponse> Request<TResponse>(string method, CancellationToken cancellationToken = default)
        {
            var responseJson = await RustRequest(method, string.Empty, null, cancellationToken);

            return JsonSerializer.Deserialize<TResponse>(responseJson, JsonOptionsProvider.JsonSerializerOptions);
        }

        public async Task<TResponse> Request<TResponse, TEvent>(string method, Action<TEvent, uint> callback, CancellationToken cancellationToken = default)
        {
            var responseJson = await RustRequest(method, string.Empty, DeserializeCallback(callback), cancellationToken);

            return JsonSerializer.Deserialize<TResponse>(responseJson, JsonOptionsProvider.JsonSerializerOptions);
        }

        public async Task Request<TRequest>(string method, TRequest request, CancellationToken cancellationToken = default)
        {
            var requestJson = JsonSerializer.Serialize(request, JsonOptionsProvider.JsonSerializerOptions);

            await RustRequest(method, requestJson, null, cancellationToken);
        }

        public async Task<TResponse> Request<TRequest, TResponse>(string method, TRequest request, CancellationToken cancellationToken = default)
        {
            var requestJson = JsonSerializer.Serialize(request, JsonOptionsProvider.JsonSerializerOptions);

            var responseJson = await RustRequest(method, requestJson, null, cancellationToken);

            return JsonSerializer.Deserialize<TResponse>(responseJson, JsonOptionsProvider.JsonSerializerOptions);
        }

        public async Task<TResponse> Request<TRequest, TResponse, TEvent>(string method, TRequest request, Action<TEvent, uint> callback,
            CancellationToken cancellationToken = default)
        {
            var requestJson = JsonSerializer.Serialize(request, JsonOptionsProvider.JsonSerializerOptions);

            var responseJson = await RustRequest(method, requestJson, DeserializeCallback(callback), cancellationToken);

            return JsonSerializer.Deserialize<TResponse>(responseJson, JsonOptionsProvider.JsonSerializerOptions);
        }

        //todo: try to convert this method to IAsyncDispose
        public void Dispose()
        {
            var waitDelegatesResult = WaitForDelegates().GetAwaiter().GetResult();
            RustInteropInterface.tc_destroy_context(_contextNumber);
            if (!waitDelegatesResult)
            {
                _logger.LogError("Delegates didn't finish during the allotted time. Force clean...");
                _dictionary.Clear();
            }

            _logger.LogTrace("Context {context} disposed", _contextNumber);
        }

        public async Task<string> RustRequest(string method, string requestJson, Action<string, uint> callback = null,
            CancellationToken cancellationToken = default)
        {
            var tcs = new TaskCompletionSource<string>();
            var callbackDelegate = new CallbackDelegate((id, json, type, finished) => CallbackDelegate(id, json, type, finished, callback, tcs));
            uint request;
            lock (_lock)
            {
                _requestId = _requestId == uint.MaxValue ? 0 : _requestId + 1;
                request = _requestId;
                _dictionary.AddOrUpdate(request, _ => callbackDelegate, (_, __) => callbackDelegate);
            }

            _logger.LogTrace("Sending request: context:{context} request:{request} method:{method} body:{body}", _contextNumber, request, method,
                requestJson);
            try
            {
                using var methodInteropString = method.ToInteropStringDisposable();
                using var paramsJsonInteropString = requestJson.ToInteropStringDisposable();
                RustInteropInterface.tc_request(_contextNumber, methodInteropString, paramsJsonInteropString, request, callbackDelegate);
            }
            catch (Exception ex)
            {
                lock (_lock)
                {
                    _dictionary.Remove(request, out CallbackDelegate _);
                }

                throw new TonClientException("Sending request error", ex);
            }

            Task executeOrTimeout = await Task.WhenAny(tcs.Task, Task.Delay(_coreExecutionTimeOut, cancellationToken));
            if (tcs.Task == executeOrTimeout) return await tcs.Task;

            // log error with ids and throw TonClientException
            _logger.LogError("Request execution timeout expired or cancellation requested. Context:{context} request:{request}", _contextNumber, request);
            throw new TonClientException("Execution timeout expired or cancellation requested");
        }

        private void CallbackDelegate(uint requestId, InteropString responseInteropString, uint responseType, bool finished, Action<string, uint> callback,
            TaskCompletionSource<string> tcs)
        {
            var responseJson = responseInteropString.ToString();
            _logger.LogTrace("Got request response context:{context} request:{request} type:{responseType} finished:{finished} body:{body}", _contextNumber,
                requestId, ((ResponseType)responseType).ToString(), finished, responseJson);
            {
                if (!_dictionary.ContainsKey(requestId))
                {
                    _logger.LogWarning("Request {request} was not found in this context {context}", requestId, _contextNumber);
                    return;
                }
            }

            if (finished) _dictionary.Remove(requestId, out CallbackDelegate _);

            switch ((ResponseType)responseType)
            {
                case ResponseType.Success:
                    tcs.SetResult(responseJson);
                    break;
                case ResponseType.Error:
                    TonClientException exception = TonExceptionSerializer.GetTonClientExceptionByResponse(responseJson);
                    tcs.SetException(exception);
                    break;
                // do nothing
                case ResponseType.Nop:
                    break;
                // it is callback if responseType>=3 
                default:
                    _logger.LogTrace("Sending callback context:{context} request:{request} body:{body}", _contextNumber, requestId, responseJson);
                    callback?.Invoke(responseJson, responseType);
                    break;
            }
        }

        private async Task<bool> WaitForDelegates()
        {
            // wait 30 seconds for all work to be completed
            var cts = new CancellationTokenSource(TimeSpan.FromSeconds(30));
            while (true)
            {
                lock (_lock)
                {
                    if (_dictionary.Count == 0) return true;
                    _logger.LogWarning("Some delegates not finished: {count} wait...", _dictionary.Count);
                }

                try
                {
                    await Task.Delay(TimeSpan.FromSeconds(1), cts.Token);
                }
                catch (TaskCanceledException)
                {
                    return false;
                }
            }
        }

        private static Action<string, uint> DeserializeCallback<TEvent>(Action<TEvent, uint> callback)
        {
            return (callbackResponseJson, responseType) => { callback?.Invoke(PolymorphicSerializer.Deserialize<TEvent>(callbackResponseJson), responseType); };
        }
    }
}