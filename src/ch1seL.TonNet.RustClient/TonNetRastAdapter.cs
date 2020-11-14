using System.Threading;
using System.Threading.Tasks;
using ch1seL.TonNet.Abstract;

namespace ch1seL.TonClientDotnet
{
    internal class TonNetRastAdapter:ITonClientAdapter
    {
        private readonly IRustTonClientCore _rustTonClient;

        public TonNetRastAdapter(IRustTonClientCore rustTonClient)
        {
            _rustTonClient = rustTonClient;
        }

        public async Task<TResponse> Request<TRequest, TResponse>(string method, TRequest request, CancellationToken cancellationToken = default) where TRequest : ITonClientRequest where TResponse : ITonClientResponse
        {
            return await _rustTonClient.Request<TRequest, TResponse>(method, request, cancellationToken);
        }
    }
}