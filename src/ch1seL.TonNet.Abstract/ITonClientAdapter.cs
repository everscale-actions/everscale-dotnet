using System.Threading;
using System.Threading.Tasks;

namespace ch1seL.TonNet.Abstract
{
    public interface ITonClientAdapter
    {
        Task<TResponse> Request<TRequest, TResponse>(string method, TRequest request, CancellationToken cancellationToken = default)
            where TRequest : ITonClientRequest
            where TResponse : ITonClientResponse;
    }
}