using ch1seL.TonNet.Abstract;
using ch1seL.TonNet.Client.Abstract;
using ch1seL.TonNet.Client.Models;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace ch1seL.TonNet.Client
{
    public class NetModule : INetModule
    {
        private readonly ITonClientAdapter _tonClientAdapter;

        public NetModule(ITonClientAdapter tonClientAdapter)
        {
            _tonClientAdapter = tonClientAdapter;
        }

        /// <summary>
        /// <para> Queries collection data</para>
        /// <para> Queries data that satisfies the `filter` conditions,</para>
        /// <para> limits the number of returned records and orders them.</para>
        /// <para> The projection fields are limited to `result` fields</para>
        /// </summary>
        public async Task<QueryCollectionResponse> QueryCollection(QueryCollectionRequest @params, CancellationToken cancellationToken = default)
        {
            return await _tonClientAdapter.Request<QueryCollectionRequest, QueryCollectionResponse>("net.query_collection", @params, cancellationToken);
        }

        /// <summary>
        /// <para> Returns an object that fulfills the conditions or waits for its appearance</para>
        /// <para> Triggers only once.</para>
        /// <para> If object that satisfies the `filter` conditions</para>
        /// <para> already exists - returns it immediately.</para>
        /// <para> If not - waits for insert/update of data within the specified `timeout`,</para>
        /// <para> and returns it.</para>
        /// <para> The projection fields are limited to `result` fields</para>
        /// </summary>
        public async Task<WaitForCollectionResponse> WaitForCollection(WaitForCollectionRequest @params, CancellationToken cancellationToken = default)
        {
            return await _tonClientAdapter.Request<WaitForCollectionRequest, WaitForCollectionResponse>("net.wait_for_collection", @params, cancellationToken);
        }

        /// <summary>
        /// <para> Cancels a subscription</para>
        /// <para> Cancels a subscription specified by its handle.</para>
        /// </summary>
        public async Task Unsubscribe(SubscribeCollectionResponse @params, CancellationToken cancellationToken = default)
        {
            await _tonClientAdapter.Request<SubscribeCollectionResponse>("net.unsubscribe", @params, cancellationToken);
        }

        /// <summary>
        /// <para> Creates a subscription</para>
        /// <para> </para>
        /// <para> Triggers for each insert/update of data</para>
        /// <para> that satisfies the `filter` conditions.</para>
        /// <para> The projection fields are limited to `result` fields.</para>
        /// </summary>
        public async Task<SubscribeCollectionResponse> SubscribeCollection(SubscribeCollectionRequest @params, Action<object> callback, CancellationToken cancellationToken = default)
        {
            return await _tonClientAdapter.Request<SubscribeCollectionRequest, SubscribeCollectionResponse, object>("net.subscribe_collection", @params, callback, cancellationToken);
        }
    }
}