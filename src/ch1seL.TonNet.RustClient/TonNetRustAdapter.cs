using System.Threading;
using System.Threading.Tasks;
using ch1seL.TonNet.Abstract;

namespace ch1seL.TonNet.RustClient
{
    internal class TonNetRustAdapter : ITonClientAdapter
    {
        private readonly IRustTonClientCore _rustTonClient;

        public TonNetRustAdapter(IRustTonClientCore rustTonClient)
        {
            _rustTonClient = rustTonClient;
        }

        public async Task<TResponse> Request<TRequest, TResponse>(string method, TRequest request, CancellationToken cancellationToken = default)
            // where TRequest : ITonClientRequest 
            // where TResponse : ITonClientResponse
        {
            return await _rustTonClient.Request<TRequest, TResponse>(method, request, cancellationToken);
        }

        public async Task<TResponse> Request<TResponse>(string method, CancellationToken cancellationToken = default)
        {
            return await _rustTonClient.Request<TResponse>(method, cancellationToken);
        }
    }
}