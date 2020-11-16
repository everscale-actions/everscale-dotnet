﻿using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using ch1seL.TonNet.RustClient.Models;
using ch1seL.TonNet.RustClient.RustInterop;
using ch1seL.TonNet.RustClient.RustInterop.Models;
using ch1seL.TonNet.RustClient.Utils;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace ch1seL.TonNet.RustClient
{
    //todo: must be singleton
    internal class RustTonClientCore : IRustTonClientCore, IDisposable
    {
        public static readonly JsonSerializerOptions JsonSerializerOptions = new JsonSerializerOptions
            {IgnoreNullValues = true, MaxDepth = int.MaxValue};

        private readonly uint _contextNumber;
        private readonly TimeSpan _coreExecutionTimeOut = TimeSpan.FromMinutes(1);
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

        private readonly IDictionary<uint, CallbackDelegate> _delegates = new ConcurrentDictionary<uint, CallbackDelegate>(); 
        
        public async Task<string> Request<TEvent>(string method, string paramsJson, Action<TEvent> callback=null, CancellationToken cancellationToken = default)
        {
            _requestId = _requestId == uint.MaxValue ? 0 : _requestId++;
            _logger.LogTrace("Init request {context}", _contextNumber);

            var cts = new TaskCompletionSource<string>();

            void CallbackDelegate(uint requestId, InteropString responseInteropString, uint responseType, bool finished)
            {
                var responseJson = responseInteropString.ToString();
                _logger.LogTrace("Got request response {context} {requestId} {responseType} {response}", _contextNumber, requestId, responseType, responseJson);

                if (_delegates.ContainsKey(requestId)) return;
                if (finished) _delegates.Remove(requestId);

                switch ((ResponseType) responseType)
                {
                    case ResponseType.Success:
                    case ResponseType.Nop when finished:
                        cts.SetResult(responseJson);
                        break;
                    case ResponseType.Error:
                        // todo: Deserialize error response
                        cts.SetException(new TonClientException(responseJson));
                        break;
                    default:
                        callback?.Invoke(JsonSerializer.Deserialize<TEvent>(paramsJson, JsonSerializerOptions));
                        break;
                }
            }
            _delegates.Add(_requestId, CallbackDelegate);
            _logger.LogTrace("Sending request {context} {method} {request}", _contextNumber, method, paramsJson);
            try
            {
                using var methodInteropString = method.ToInteropStringDisposable();
                using var paramsJsonInteropString = paramsJson.ToInteropStringDisposable();
                RustInteropInterface.tc_request(_contextNumber, methodInteropString, paramsJsonInteropString, _requestId, CallbackDelegate);
            }
            catch
            {
                _delegates.Remove(_requestId);
            }

            Task executeOrTimeout = await Task.WhenAny(cts.Task, Task.Delay(_coreExecutionTimeOut, cancellationToken));
            if (cts.Task == executeOrTimeout) return await cts.Task;
            throw new TonClientException("Execution timeout expired or cancellation request");
        }
    }
}