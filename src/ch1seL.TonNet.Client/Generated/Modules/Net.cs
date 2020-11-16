using ch1seL.TonNet.Abstract;
using ch1seL.TonNet.Client.Abstract;
using ch1seL.TonNet.Client.Models;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace ch1seL.TonNet.Client
{
    public class Net : INet
    {
        private readonly ITonClientAdapter _tonClientAdapter;

        public Net(ITonClientAdapter tonClientAdapter)
        {
            _tonClientAdapter = tonClientAdapter;
        }

        public async Task<QueryCollectionResponse> QueryCollection(QueryCollectionRequest @params, CancellationToken cancellationToken = default)
        {
            return await _tonClientAdapter.Request<QueryCollectionRequest, QueryCollectionResponse>("net.query_collection", @params, cancellationToken);
        }

        public async Task<WaitForCollectionResponse> WaitForCollection(WaitForCollectionRequest @params, CancellationToken cancellationToken = default)
        {
            return await _tonClientAdapter.Request<WaitForCollectionRequest, WaitForCollectionResponse>("net.wait_for_collection", @params, cancellationToken);
        }

        public async Task Unsubscribe(SubscribeCollectionResponse @params, CancellationToken cancellationToken = default)
        {
            await _tonClientAdapter.Request<SubscribeCollectionResponse>("net.unsubscribe", @params, cancellationToken);
        }

        public async Task<SubscribeCollectionResponse> SubscribeCollection(SubscribeCollectionRequest @params, Action<object> callback, CancellationToken cancellationToken = default)
        {
            return await _tonClientAdapter.Request<SubscribeCollectionRequest, SubscribeCollectionResponse, object>("net.subscribe_collection", @params, callback, cancellationToken);
        }
    }
}