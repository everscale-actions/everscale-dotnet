using ch1seL.TonNet.Client.Models;
using System;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;

namespace ch1seL.TonNet.Abstract.Modules
{
    public interface INetModule : ITonModule
    {
        /// <summary>
        /// Performs DAppServer GraphQL query.
        /// </summary>
        public Task<ResultOfQuery> Query(ParamsOfQuery @params, CancellationToken cancellationToken = default);

        /// <summary>
        /// <para>Queries collection data</para>
        /// <para>Queries data that satisfies the `filter` conditions,</para>
        /// <para>limits the number of returned records and orders them.</para>
        /// <para>The projection fields are limited to `result` fields</para>
        /// </summary>
        public Task<ResultOfQueryCollection> QueryCollection(ParamsOfQueryCollection @params, CancellationToken cancellationToken = default);

        /// <summary>
        /// <para>Returns an object that fulfills the conditions or waits for its appearance</para>
        /// <para>Triggers only once.</para>
        /// <para>If object that satisfies the `filter` conditions</para>
        /// <para>already exists - returns it immediately.</para>
        /// <para>If not - waits for insert/update of data within the specified `timeout`,</para>
        /// <para>and returns it.</para>
        /// <para>The projection fields are limited to `result` fields</para>
        /// </summary>
        public Task<ResultOfWaitForCollection> WaitForCollection(ParamsOfWaitForCollection @params, CancellationToken cancellationToken = default);

        /// <summary>
        /// <para>Cancels a subscription</para>
        /// <para>Cancels a subscription specified by its handle.</para>
        /// </summary>
        public Task Unsubscribe(ResultOfSubscribeCollection @params, CancellationToken cancellationToken = default);

        /// <summary>
        /// <para>Creates a subscription</para>
        /// <para>Triggers for each insert/update of data</para>
        /// <para>that satisfies the `filter` conditions.</para>
        /// <para>The projection fields are limited to `result` fields.</para>
        /// </summary>
        public Task<ResultOfSubscribeCollection> SubscribeCollection(ParamsOfSubscribeCollection @params, Action<JsonElement,uint> callback = null, CancellationToken cancellationToken = default);

        /// <summary>
        /// Suspends network module to stop any network activity
        /// </summary>
        public Task Suspend(CancellationToken cancellationToken = default);

        /// <summary>
        /// Resumes network module to enable network activity
        /// </summary>
        public Task Resume(CancellationToken cancellationToken = default);

        /// <summary>
        /// Returns ID of the last block in a specified account shard
        /// </summary>
        public Task<ResultOfFindLastShardBlock> FindLastShardBlock(ParamsOfFindLastShardBlock @params, CancellationToken cancellationToken = default);

        /// <summary>
        /// Requests the list of alternative endpoints from server
        /// </summary>
        public Task<EndpointsSet> FetchEndpoints(CancellationToken cancellationToken = default);

        /// <summary>
        /// Sets the list of endpoints to use on reinit
        /// </summary>
        public Task SetEndpoints(EndpointsSet @params, CancellationToken cancellationToken = default);
    }
}