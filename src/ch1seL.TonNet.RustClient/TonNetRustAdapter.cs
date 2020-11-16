using System;
using System.Threading;
using System.Threading.Tasks;
using ch1seL.TonNet.Abstract;

namespace ch1seL.TonNet.RustClient
{
    /// <summary>
    ///     must be a Singleton
    /// </summary>
    internal class TonNetRustAdapter : ITonClientAdapter
    {
        private readonly IRustTonClientCore _rustTonClient;

        public TonNetRustAdapter(IRustTonClientCore rustTonClient)
        {
            _rustTonClient = rustTonClient;
        }

        public async Task Request<TRequest>(string method, TRequest request, CancellationToken cancellationToken = default)
        {
            await _rustTonClient.Request(method, request, cancellationToken);
        }

        public async Task<TResponse> Request<TResponse>(string method, CancellationToken cancellationToken = default)
        {
            return await _rustTonClient.Request<TResponse>(method, cancellationToken);
        }

        public async Task<TResponse> Request<TRequest, TResponse>(string method, TRequest request, CancellationToken cancellationToken = default)
        {
            return await _rustTonClient.Request<TRequest, TResponse>(method, request, cancellationToken);
        }

        public async Task<TResponse> Request<TResponse, TEvent>(string method, Action<TEvent> callback, CancellationToken cancellationToken = default)
        {
            return await _rustTonClient.Request<TResponse, TEvent>(method, callback, cancellationToken);
        }

        public async Task<TResponse> Request<TRequest, TResponse, TEvent>(string method, TRequest request, Action<TEvent> callback,
            CancellationToken cancellationToken = default)
        {
            return await _rustTonClient.Request<TRequest, TResponse, TEvent>(method, request, callback, cancellationToken);
        }
    }
}