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
        /// Performs multiple queries per single fetch.
        /// </summary>
        public Task<ResultOfBatchQuery> BatchQuery(ParamsOfBatchQuery @params, CancellationToken cancellationToken = default);

        /// <summary>
        /// <para>Queries collection data</para>
        /// <para>Queries data that satisfies the `filter` conditions,</para>
        /// <para>limits the number of returned records and orders them.</para>
        /// <para>The projection fields are limited to `result` fields</para>
        /// </summary>
        public Task<ResultOfQueryCollection> QueryCollection(ParamsOfQueryCollection @params, CancellationToken cancellationToken = default);

        /// <summary>
        /// <para>Aggregates collection data.</para>
        /// <para>Aggregates values from the specified `fields` for records</para>
        /// <para>that satisfies the `filter` conditions,</para>
        /// </summary>
        public Task<ResultOfAggregateCollection> AggregateCollection(ParamsOfAggregateCollection @params, CancellationToken cancellationToken = default);

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
        /// <para>Triggers for each insert/update of data that satisfies</para>
        /// <para>the `filter` conditions.</para>
        /// <para>The projection fields are limited to `result` fields.</para>
        /// <para>The subscription is a persistent communication channel between</para>
        /// <para>client and Free TON Network.</para>
        /// <para>All changes in the blockchain will be reflected in realtime.</para>
        /// <para>Changes means inserts and updates of the blockchain entities.</para>
        /// <para>### Important Notes on Subscriptions</para>
        /// <para>Unfortunately sometimes the connection with the network brakes down.</para>
        /// <para>In this situation the library attempts to reconnect to the network.</para>
        /// <para>This reconnection sequence can take significant time.</para>
        /// <para>All of this time the client is disconnected from the network.</para>
        /// <para>Bad news is that all blockchain changes that happened while</para>
        /// <para>the client was disconnected are lost.</para>
        /// <para>Good news is that the client report errors to the callback when</para>
        /// <para>it loses and resumes connection.</para>
        /// <para>So, if the lost changes are important to the application then</para>
        /// <para>the application must handle these error reports.</para>
        /// <para>Library reports errors with `responseType` == 101</para>
        /// <para>and the error object passed via `params`.</para>
        /// <para>When the library has successfully reconnected</para>
        /// <para>the application receives callback with</para>
        /// <para>`responseType` == 101 and `params.code` == 614 (NetworkModuleResumed).</para>
        /// <para>Application can use several ways to handle this situation:</para>
        /// <para>- If application monitors changes for the single blockchain</para>
        /// <para>object (for example specific account):  application</para>
        /// <para>can perform a query for this object and handle actual data as a</para>
        /// <para>regular data from the subscription.</para>
        /// <para>- If application monitors sequence of some blockchain objects</para>
        /// <para>(for example transactions of the specific account): application must</para>
        /// <para>refresh all cached (or visible to user) lists where this sequences presents.</para>
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

        /// <summary>
        /// <para>Allows to query and paginate through the list of accounts that the specified account has interacted with, sorted by the time of the last internal message between accounts</para>
        /// <para>*Attention* this query retrieves data from 'Counterparties' service which is not supported in</para>
        /// <para>the opensource version of DApp Server (and will not be supported) as well as in TON OS SE (will be supported in SE in future),</para>
        /// <para>but is always accessible via [TON OS Devnet/Mainnet Clouds](https://docs.ton.dev/86757ecb2/p/85c869-networks)</para>
        /// </summary>
        public Task<ResultOfQueryCollection> QueryCounterparties(ParamsOfQueryCounterparties @params, CancellationToken cancellationToken = default);
    }
}