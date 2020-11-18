using System;
using System.Threading;
using System.Threading.Tasks;

namespace ch1seL.TonNet.Abstract
{
    /// <summary>
    ///     The most importantly interface. It provide all that needed to clients implementation
    /// </summary>
    /// There are big plans for this interface
    public interface ITonClientAdapter
    {
        Task Request<TRequest>(string method, TRequest request, CancellationToken cancellationToken = default);
        Task<TResponse> Request<TResponse>(string method, CancellationToken cancellationToken = default);
        Task<TResponse> Request<TRequest, TResponse>(string method, TRequest request, CancellationToken cancellationToken = default);
        Task<TResponse> Request<TResponse, TEvent>(string method, Action<TEvent, uint> callback, CancellationToken cancellationToken = default);

        Task<TResponse> Request<TRequest, TResponse, TEvent>(string method, TRequest request, Action<TEvent, uint> callback,
            CancellationToken cancellationToken = default);
    }
}