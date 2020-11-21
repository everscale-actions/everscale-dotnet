using System;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using ch1seL.TonNet.Abstract;
using ch1seL.TonNet.Serialization;

namespace ch1seL.TonNet.RustClient
{
    /// <summary>
    ///     Rust adapter. Uses extensions method of RustTonClientCore
    /// </summary>
    public class TonNetRustAdapter : ITonClientAdapter
    {
        private readonly IRustTonClientCore _rustTonClient;

        public TonNetRustAdapter(IRustTonClientCore rustTonClient)
        {
            _rustTonClient = rustTonClient;
        }

        public async Task<TResponse> Request<TResponse>(string method, CancellationToken cancellationToken = default)
        {
            var responseJson = await _rustTonClient.Request<string>(method, string.Empty, null, cancellationToken);

            return JsonSerializer.Deserialize<TResponse>(responseJson, JsonOptionsProvider.JsonSerializerOptions);
        }

        public async Task<TResponse> Request<TResponse, TEvent>(string method, Action<TEvent, uint> callback, CancellationToken cancellationToken = default)
        {
            var responseJson = await _rustTonClient.Request(method, string.Empty, callback, cancellationToken);

            return JsonSerializer.Deserialize<TResponse>(responseJson, JsonOptionsProvider.JsonSerializerOptions);
        }

        public async Task Request<TRequest>(string method, TRequest request, CancellationToken cancellationToken = default)
        {
            var requestJson = JsonSerializer.Serialize(request, JsonOptionsProvider.JsonSerializerOptions);

            await _rustTonClient.Request<string>(method, requestJson, null, cancellationToken);
        }

        public async Task<TResponse> Request<TRequest, TResponse>(string method, TRequest request, CancellationToken cancellationToken = default)
        {
            var requestJson = JsonSerializer.Serialize(request, JsonOptionsProvider.JsonSerializerOptions);

            var responseJson = await _rustTonClient.Request<string>(method, requestJson, null, cancellationToken);

            return JsonSerializer.Deserialize<TResponse>(responseJson, JsonOptionsProvider.JsonSerializerOptions);
        }

        public async Task<TResponse> Request<TRequest, TResponse, TEvent>(string method, TRequest request, Action<TEvent, uint> callback,
            CancellationToken cancellationToken = default)
        {
            var requestJson = JsonSerializer.Serialize(request, JsonOptionsProvider.JsonSerializerOptions);

            var responseJson = await _rustTonClient.Request(method, requestJson, callback, cancellationToken);

            return JsonSerializer.Deserialize<TResponse>(responseJson, JsonOptionsProvider.JsonSerializerOptions);
        }
    }
}