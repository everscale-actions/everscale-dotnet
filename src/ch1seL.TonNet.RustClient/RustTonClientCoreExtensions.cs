﻿using System;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;

namespace ch1seL.TonNet.RustClient
{
    public static class RustTonClientCoreExtensions
    {
        internal static async Task<string> Request(this IRustTonClientCore rustTonClientCore,string method, string paramsJson, CancellationToken cancellationToken = default)
        {
            return await rustTonClientCore.Request<object>(method, paramsJson, null, cancellationToken);
        }
        
        internal static async Task Request<TRequest>(this IRustTonClientCore rustTonClientCore, string method, TRequest request, CancellationToken cancellationToken = default)
        {
            var paramsJson = JsonSerializer.Serialize(request, RustTonClientCore.JsonSerializerOptions);

            await rustTonClientCore.Request<object>(method, paramsJson, null, cancellationToken);
        }
        
        internal static async Task<TResponse> Request<TResponse>(this IRustTonClientCore rustTonClientCore, string method, CancellationToken cancellationToken = default)
        {
            var responseJson = await rustTonClientCore.Request<object>(method, string.Empty,null, cancellationToken);

            return JsonSerializer.Deserialize<TResponse>(responseJson, RustTonClientCore.JsonSerializerOptions);
        }
        
        internal static async Task<TResponse> Request<TRequest, TResponse>(this IRustTonClientCore rustTonClientCore, string method, TRequest request, CancellationToken cancellationToken = default)
        {
            var paramsJson = JsonSerializer.Serialize(request, RustTonClientCore.JsonSerializerOptions);

            var responseJson = await rustTonClientCore.Request<object>(method, paramsJson, null, cancellationToken);

            return JsonSerializer.Deserialize<TResponse>(responseJson, RustTonClientCore.JsonSerializerOptions);
        }
        
        internal static async Task<TResponse> Request<TRequest, TResponse, TEvent>(this IRustTonClientCore rustTonClientCore, string method,TRequest request, Action<TEvent> callback, CancellationToken cancellationToken = default)
        {
            var paramsJson = JsonSerializer.Serialize(request, RustTonClientCore.JsonSerializerOptions);
            
            var responseJson = await rustTonClientCore.Request(method, paramsJson, callback, cancellationToken);

            return JsonSerializer.Deserialize<TResponse>(responseJson, RustTonClientCore.JsonSerializerOptions);
        }

        internal static async Task<TResponse> Request<TResponse, TEvent>(this IRustTonClientCore rustTonClientCore, string method, Action<TEvent> callback, CancellationToken cancellationToken = default)
        {
            var responseJson = await rustTonClientCore.Request<TEvent>(method, string.Empty, callback, cancellationToken);

            return JsonSerializer.Deserialize<TResponse>(responseJson, RustTonClientCore.JsonSerializerOptions);
        }
        
        internal static async Task Request<TRequest, TEvent>(this IRustTonClientCore rustTonClientCore, string method, TRequest request, Action<TEvent> callback,
            CancellationToken cancellationToken = default)
        {
            var paramsJson = JsonSerializer.Serialize(request, RustTonClientCore.JsonSerializerOptions);

            await rustTonClientCore.Request(method, paramsJson, callback, cancellationToken);
        }
    }
}