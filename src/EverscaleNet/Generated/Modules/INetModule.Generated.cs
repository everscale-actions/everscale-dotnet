using EverscaleNet.Client.Models;
using System;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;

namespace EverscaleNet.Abstract.Modules
{
    /// <summary>
    /// <para>Net Module</para>
    /// </summary>
    public interface INetModule : IEverModule
    {
        /// <summary>
        /// <para>Performs DAppServer GraphQL query.</para>
        /// </summary>
        public Task<ResultOfQuery> Query(ParamsOfQuery @params, CancellationToken cancellationToken = default);

        /// <summary>
        /// <para>Performs multiple queries per single fetch.</para>
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
        /// <para>Creates a collection subscription</para>
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
        /// <para>Creates a subscription</para>
        /// <para>The subscription is a persistent communication channel between</para>
        /// <para>client and Everscale Network.</para>
        /// <para>### Important Notes on Subscriptions</para>
        /// <para>Unfortunately sometimes the connection with the network breaks down.</para>
        /// <para>In this situation the library attempts to reconnect to the network.</para>
        /// <para>This reconnection sequence can take significant time.</para>
        /// <para>All of this time the client is disconnected from the network.</para>
        /// <para>Bad news is that all changes that happened while</para>
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
        /// <para>- If application monitors changes for the single</para>
        /// <para>object (for example specific account):  application</para>
        /// <para>can perform a query for this object and handle actual data as a</para>
        /// <para>regular data from the subscription.</para>
        /// <para>- If application monitors sequence of some objects</para>
        /// <para>(for example transactions of the specific account): application must</para>
        /// <para>refresh all cached (or visible to user) lists where this sequences presents.</para>
        /// </summary>
        public Task<ResultOfSubscribeCollection> Subscribe(ParamsOfSubscribe @params, Action<JsonElement,uint> callback = null, CancellationToken cancellationToken = default);

        /// <summary>
        /// <para>Suspends network module to stop any network activity</para>
        /// </summary>
        public Task Suspend(CancellationToken cancellationToken = default);

        /// <summary>
        /// <para>Resumes network module to enable network activity</para>
        /// </summary>
        public Task Resume(CancellationToken cancellationToken = default);

        /// <summary>
        /// <para>Returns ID of the last block in a specified account shard</para>
        /// </summary>
        public Task<ResultOfFindLastShardBlock> FindLastShardBlock(ParamsOfFindLastShardBlock @params, CancellationToken cancellationToken = default);

        /// <summary>
        /// <para>Requests the list of alternative endpoints from server</para>
        /// </summary>
        public Task<EndpointsSet> FetchEndpoints(CancellationToken cancellationToken = default);

        /// <summary>
        /// <para>Sets the list of endpoints to use on reinit</para>
        /// </summary>
        public Task SetEndpoints(EndpointsSet @params, CancellationToken cancellationToken = default);

        /// <summary>
        /// <para>Requests the list of alternative endpoints from server</para>
        /// </summary>
        public Task<ResultOfGetEndpoints> GetEndpoints(CancellationToken cancellationToken = default);

        /// <summary>
        /// <para>Allows to query and paginate through the list of accounts that the specified account has interacted with, sorted by the time of the last internal message between accounts</para>
        /// <para>*Attention* this query retrieves data from 'Counterparties' service which is not supported in</para>
        /// <para>the opensource version of DApp Server (and will not be supported) as well as in Evernode SE (will be supported in SE in future),</para>
        /// <para>but is always accessible via [EVER OS Clouds](../ton-os-api/networks.md)</para>
        /// </summary>
        public Task<ResultOfQueryCollection> QueryCounterparties(ParamsOfQueryCounterparties @params, CancellationToken cancellationToken = default);

        /// <summary>
        /// <para>Returns a tree of transactions triggered by a specific message.</para>
        /// <para>Performs recursive retrieval of a transactions tree produced by a specific message:</para>
        /// <para>in_msg -&gt; dst_transaction -&gt; out_messages -&gt; dst_transaction -&gt; ...</para>
        /// <para>If the chain of transactions execution is in progress while the function is running,</para>
        /// <para>it will wait for the next transactions to appear until the full tree or more than 50 transactions</para>
        /// <para>are received.</para>
        /// <para>All the retrieved messages and transactions are included</para>
        /// <para>into `result.messages` and `result.transactions` respectively.</para>
        /// <para>Function reads transactions layer by layer, by pages of 20 transactions.</para>
        /// <para>The retrieval process goes like this:</para>
        /// <para>Let's assume we have an infinite chain of transactions and each transaction generates 5 messages.</para>
        /// <para>1. Retrieve 1st message (input parameter) and corresponding transaction - put it into result.</para>
        /// <para>It is the first level of the tree of transactions - its root.</para>
        /// <para>Retrieve 5 out message ids from the transaction for next steps.</para>
        /// <para>2. Retrieve 5 messages and corresponding transactions on the 2nd layer. Put them into result.</para>
        /// <para>Retrieve 5*5 out message ids from these transactions for next steps</para>
        /// <para>3. Retrieve 20 (size of the page) messages and transactions (3rd layer) and 20*5=100 message ids (4th layer).</para>
        /// <para>4. Retrieve the last 5 messages and 5 transactions on the 3rd layer + 15 messages and transactions (of 100) from the 4th layer</para>
        /// <para>+ 25 message ids of the 4th layer + 75 message ids of the 5th layer.</para>
        /// <para>5. Retrieve 20 more messages and 20 more transactions of the 4th layer + 100 more message ids of the 5th layer.</para>
        /// <para>6. Now we have 1+5+20+20+20 = 66 transactions, which is more than 50. Function exits with the tree of</para>
        /// <para>1m-&gt;1t-&gt;5m-&gt;5t-&gt;25m-&gt;25t-&gt;35m-&gt;35t. If we see any message ids in the last transactions out_msgs, which don't have</para>
        /// <para>corresponding messages in the function result, it means that the full tree was not received and we need to continue iteration.</para>
        /// <para>To summarize, it is guaranteed that each message in `result.messages` has the corresponding transaction</para>
        /// <para>in the `result.transactions`.</para>
        /// <para>But there is no guarantee that all messages from transactions `out_msgs` are</para>
        /// <para>presented in `result.messages`.</para>
        /// <para>So the application has to continue retrieval for missing messages if it requires.</para>
        /// </summary>
        public Task<ResultOfQueryTransactionTree> QueryTransactionTree(ParamsOfQueryTransactionTree @params, CancellationToken cancellationToken = default);

        /// <summary>
        /// <para>Creates block iterator.</para>
        /// <para>Block iterator uses robust iteration methods that guaranties that every</para>
        /// <para>block in the specified range isn't missed or iterated twice.</para>
        /// <para>Iterated range can be reduced with some filters:</para>
        /// <para>- `start_time` – the bottom time range. Only blocks with `gen_utime`</para>
        /// <para>more or equal to this value is iterated. If this parameter is omitted then there is</para>
        /// <para>no bottom time edge, so all blocks since zero state is iterated.</para>
        /// <para>- `end_time` – the upper time range. Only blocks with `gen_utime`</para>
        /// <para>less then this value is iterated. If this parameter is omitted then there is</para>
        /// <para>no upper time edge, so iterator never finishes.</para>
        /// <para>- `shard_filter` – workchains and shard prefixes that reduce the set of interesting</para>
        /// <para>blocks. Block conforms to the shard filter if it belongs to the filter workchain</para>
        /// <para>and the first bits of block's `shard` fields matches to the shard prefix.</para>
        /// <para>Only blocks with suitable shard are iterated.</para>
        /// <para>Items iterated is a JSON objects with block data. The minimal set of returned</para>
        /// <para>fields is:</para>
        /// <para>```text</para>
        /// <para>id</para>
        /// <para>gen_utime</para>
        /// <para>workchain_id</para>
        /// <para>shard</para>
        /// <para>after_split</para>
        /// <para>after_merge</para>
        /// <para>prev_ref {</para>
        /// <para>    root_hash</para>
        /// <para>}</para>
        /// <para>prev_alt_ref {</para>
        /// <para>    root_hash</para>
        /// <para>}</para>
        /// <para>```</para>
        /// <para>Application can request additional fields in the `result` parameter.</para>
        /// <para>Application should call the `remove_iterator` when iterator is no longer required.</para>
        /// </summary>
        public Task<RegisteredIterator> CreateBlockIterator(ParamsOfCreateBlockIterator @params, CancellationToken cancellationToken = default);

        /// <summary>
        /// <para>Resumes block iterator.</para>
        /// <para>The iterator stays exactly at the same position where the `resume_state` was caught.</para>
        /// <para>Application should call the `remove_iterator` when iterator is no longer required.</para>
        /// </summary>
        public Task<RegisteredIterator> ResumeBlockIterator(ParamsOfResumeBlockIterator @params, CancellationToken cancellationToken = default);

        /// <summary>
        /// <para>Creates transaction iterator.</para>
        /// <para>Transaction iterator uses robust iteration methods that guaranty that every</para>
        /// <para>transaction in the specified range isn't missed or iterated twice.</para>
        /// <para>Iterated range can be reduced with some filters:</para>
        /// <para>- `start_time` – the bottom time range. Only transactions with `now`</para>
        /// <para>more or equal to this value are iterated. If this parameter is omitted then there is</para>
        /// <para>no bottom time edge, so all the transactions since zero state are iterated.</para>
        /// <para>- `end_time` – the upper time range. Only transactions with `now`</para>
        /// <para>less then this value are iterated. If this parameter is omitted then there is</para>
        /// <para>no upper time edge, so iterator never finishes.</para>
        /// <para>- `shard_filter` – workchains and shard prefixes that reduce the set of interesting</para>
        /// <para>accounts. Account address conforms to the shard filter if</para>
        /// <para>it belongs to the filter workchain and the first bits of address match to</para>
        /// <para>the shard prefix. Only transactions with suitable account addresses are iterated.</para>
        /// <para>- `accounts_filter` – set of account addresses whose transactions must be iterated.</para>
        /// <para>Note that accounts filter can conflict with shard filter so application must combine</para>
        /// <para>these filters carefully.</para>
        /// <para>Iterated item is a JSON objects with transaction data. The minimal set of returned</para>
        /// <para>fields is:</para>
        /// <para>```text</para>
        /// <para>id</para>
        /// <para>account_addr</para>
        /// <para>now</para>
        /// <para>balance_delta(format:DEC)</para>
        /// <para>bounce { bounce_type }</para>
        /// <para>in_message {</para>
        /// <para>    id</para>
        /// <para>    value(format:DEC)</para>
        /// <para>    msg_type</para>
        /// <para>    src</para>
        /// <para>}</para>
        /// <para>out_messages {</para>
        /// <para>    id</para>
        /// <para>    value(format:DEC)</para>
        /// <para>    msg_type</para>
        /// <para>    dst</para>
        /// <para>}</para>
        /// <para>```</para>
        /// <para>Application can request an additional fields in the `result` parameter.</para>
        /// <para>Another parameter that affects on the returned fields is the `include_transfers`.</para>
        /// <para>When this parameter is `true` the iterator computes and adds `transfer` field containing</para>
        /// <para>list of the useful `TransactionTransfer` objects.</para>
        /// <para>Each transfer is calculated from the particular message related to the transaction</para>
        /// <para>and has the following structure:</para>
        /// <para>- message – source message identifier.</para>
        /// <para>- isBounced – indicates that the transaction is bounced, which means the value will be returned back to the sender.</para>
        /// <para>- isDeposit – indicates that this transfer is the deposit (true) or withdraw (false).</para>
        /// <para>- counterparty – account address of the transfer source or destination depending on `isDeposit`.</para>
        /// <para>- value – amount of nano tokens transferred. The value is represented as a decimal string</para>
        /// <para>because the actual value can be more precise than the JSON number can represent. Application</para>
        /// <para>must use this string carefully – conversion to number can follow to loose of precision.</para>
        /// <para>Application should call the `remove_iterator` when iterator is no longer required.</para>
        /// </summary>
        public Task<RegisteredIterator> CreateTransactionIterator(ParamsOfCreateTransactionIterator @params, CancellationToken cancellationToken = default);

        /// <summary>
        /// <para>Resumes transaction iterator.</para>
        /// <para>The iterator stays exactly at the same position where the `resume_state` was caught.</para>
        /// <para>Note that `resume_state` doesn't store the account filter. If the application requires</para>
        /// <para>to use the same account filter as it was when the iterator was created then the application</para>
        /// <para>must pass the account filter again in `accounts_filter` parameter.</para>
        /// <para>Application should call the `remove_iterator` when iterator is no longer required.</para>
        /// </summary>
        public Task<RegisteredIterator> ResumeTransactionIterator(ParamsOfResumeTransactionIterator @params, CancellationToken cancellationToken = default);

        /// <summary>
        /// <para>Returns next available items.</para>
        /// <para>In addition to available items this function returns the `has_more` flag</para>
        /// <para>indicating that the iterator isn't reach the end of the iterated range yet.</para>
        /// <para>This function can return the empty list of available items but</para>
        /// <para>indicates that there are more items is available.</para>
        /// <para>This situation appears when the iterator doesn't reach iterated range</para>
        /// <para>but database doesn't contains available items yet.</para>
        /// <para>If application requests resume state in `return_resume_state` parameter</para>
        /// <para>then this function returns `resume_state` that can be used later to</para>
        /// <para>resume the iteration from the position after returned items.</para>
        /// <para>The structure of the items returned depends on the iterator used.</para>
        /// <para>See the description to the appropriated iterator creation function.</para>
        /// </summary>
        public Task<ResultOfIteratorNext> IteratorNext(ParamsOfIteratorNext @params, CancellationToken cancellationToken = default);

        /// <summary>
        /// <para>Removes an iterator</para>
        /// <para>Frees all resources allocated in library to serve iterator.</para>
        /// <para>Application always should call the `remove_iterator` when iterator</para>
        /// <para>is no longer required.</para>
        /// </summary>
        public Task RemoveIterator(RegisteredIterator @params, CancellationToken cancellationToken = default);

        /// <summary>
        /// <para>Returns signature ID for configured network if it should be used in messages signature</para>
        /// </summary>
        public Task<ResultOfGetSignatureId> GetSignatureId(CancellationToken cancellationToken = default);
    }
}