using System;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using ch1seL.TonNet.Abstract;
using ch1seL.TonNet.Serialization;

namespace ch1seL.TonNet.RustAdapter
{
    /// <summary>
    ///     Rust adapter. Uses RustTonClientCore to get serialized responses by serialized requests from TON SDK
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
            var responseJson = await _rustTonClient.Request(method, string.Empty, null, cancellationToken);

            return JsonSerializer.Deserialize<TResponse>(responseJson, JsonOptionsProvider.JsonSerializerOptions);
        }

        public async Task<TResponse> Request<TResponse, TEvent>(string method, Action<TEvent, uint> callback, CancellationToken cancellationToken = default)
        {
            var responseJson = await _rustTonClient.Request(method, string.Empty, DeserializeCallback(callback), cancellationToken);

            return JsonSerializer.Deserialize<TResponse>(responseJson, JsonOptionsProvider.JsonSerializerOptions);
        }

        public async Task Request<TRequest>(string method, TRequest request, CancellationToken cancellationToken = default)
        {
            var requestJson = JsonSerializer.Serialize(request, JsonOptionsProvider.JsonSerializerOptions);

            await _rustTonClient.Request(method, requestJson, null, cancellationToken);
        }

        public async Task<TResponse> Request<TRequest, TResponse>(string method, TRequest request, CancellationToken cancellationToken = default)
        {
            var requestJson = JsonSerializer.Serialize(request, JsonOptionsProvider.JsonSerializerOptions);

            var responseJson = await _rustTonClient.Request(method, requestJson, null, cancellationToken);

            return JsonSerializer.Deserialize<TResponse>(responseJson, JsonOptionsProvider.JsonSerializerOptions);
        }

        public async Task<TResponse> Request<TRequest, TResponse, TEvent>(string method, TRequest request, Action<TEvent, uint> callback,
            CancellationToken cancellationToken = default)
        {
            var requestJson = JsonSerializer.Serialize(request, JsonOptionsProvider.JsonSerializerOptions);

            var responseJson = await _rustTonClient.Request(method, requestJson, DeserializeCallback(callback), cancellationToken);

            return JsonSerializer.Deserialize<TResponse>(responseJson, JsonOptionsProvider.JsonSerializerOptions);
        }

        private static Action<string, uint> DeserializeCallback<TEvent>(Action<TEvent, uint> callback)
        {
            return (callbackResponseJson, responseType) => { callback?.Invoke(PolymorphicSerializer.Deserialize<TEvent>(callbackResponseJson), responseType); };
        }
    }
}