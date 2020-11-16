using ch1seL.TonNet.Abstract;
using ch1seL.TonNet.Client.Abstract;
using ch1seL.TonNet.Client.Models;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace ch1seL.TonNet.Client
{
    public interface INet : ITonModule
    {
        public Task<QueryCollectionResponse> QueryCollection(QueryCollectionRequest @params, CancellationToken cancellationToken = default);
        public Task<WaitForCollectionResponse> WaitForCollection(WaitForCollectionRequest @params, CancellationToken cancellationToken = default);
        public Task Unsubscribe(SubscribeCollectionResponse @params, CancellationToken cancellationToken = default);
        public Task<SubscribeCollectionResponse> SubscribeCollection(SubscribeCollectionRequest @params, Action<object> callback, CancellationToken cancellationToken = default);
    }
}