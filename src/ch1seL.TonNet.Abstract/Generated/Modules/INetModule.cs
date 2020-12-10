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
        /// Not described yet..
        /// </summary>
        public Task<ResultOfQuery> Query(ParamsOfQuery @params, CancellationToken cancellationToken = default);

        /// <summary>
        /// <para>Queries data that satisfies the `filter` conditions,</para>
        /// <para>limits the number of returned records and orders them.</para>
        /// <para>The projection fields are limited to `result` fields</para>
        /// </summary>
        public Task<ResultOfQueryCollection> QueryCollection(ParamsOfQueryCollection @params, CancellationToken cancellationToken = default);

        /// <summary>
        /// <para>Triggers only once.</para>
        /// <para>If object that satisfies the `filter` conditions</para>
        /// <para>already exists - returns it immediately.</para>
        /// <para>If not - waits for insert/update of data within the specified `timeout`,</para>
        /// <para>and returns it.</para>
        /// <para>The projection fields are limited to `result` fields</para>
        /// </summary>
        public Task<ResultOfWaitForCollection> WaitForCollection(ParamsOfWaitForCollection @params, CancellationToken cancellationToken = default);

        /// <summary>
        /// Cancels a subscription specified by its handle.
        /// </summary>
        public Task Unsubscribe(ResultOfSubscribeCollection @params, CancellationToken cancellationToken = default);

        /// <summary>
        /// <para>Triggers for each insert/update of data</para>
        /// <para>that satisfies the `filter` conditions.</para>
        /// <para>The projection fields are limited to `result` fields.</para>
        /// </summary>
        public Task<ResultOfSubscribeCollection> SubscribeCollection(ParamsOfSubscribeCollection @params, Action<JsonElement,uint> callback = null, CancellationToken cancellationToken = default);

        /// <summary>
        ///  Suspends network module to stop any network activity
        /// </summary>
        public Task Suspend(CancellationToken cancellationToken = default);

        /// <summary>
        ///  Resumes network module to enable network activity
        /// </summary>
        public Task Resume(CancellationToken cancellationToken = default);
    }
}