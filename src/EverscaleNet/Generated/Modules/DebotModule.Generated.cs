using EverscaleNet.Abstract;
using EverscaleNet.Abstract.Modules;
using EverscaleNet.Client.Models;
using System;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;

namespace EverscaleNet.Client.Modules
{
    /// <summary>
    /// <para>Debot Module</para>
    /// </summary>
    public class DebotModule : IDebotModule
    {
        private readonly IEverClientAdapter _everClientAdapter;

        /// <summary>
        /// <para>.ctor</para>
        /// </summary>
        public DebotModule(IEverClientAdapter everClientAdapter)
        {
            _everClientAdapter = everClientAdapter;
        }

        /// <summary>
        /// <para>[UNSTABLE](UNSTABLE.md) Creates and instance of DeBot.</para>
        /// <para>Downloads debot smart contract (code and data) from blockchain and creates</para>
        /// <para>an instance of Debot Engine for it.</para>
        /// <para># Remarks</para>
        /// <para>It does not switch debot to context 0. Browser Callbacks are not called.</para>
        /// </summary>
        public async Task<RegisteredDebot> Init(ParamsOfInit @params, Func<JsonElement, uint, Task> appObject = null, CancellationToken cancellationToken = default)
        {
            return await _everClientAdapter.Request<ParamsOfInit, RegisteredDebot, JsonElement>("debot.init", @params, appObject, cancellationToken);
        }

        /// <summary>
        /// <para>[UNSTABLE](UNSTABLE.md) Starts the DeBot.</para>
        /// <para>Downloads debot smart contract from blockchain and switches it to</para>
        /// <para>context zero.</para>
        /// <para>This function must be used by Debot Browser to start a dialog with debot.</para>
        /// <para>While the function is executing, several Browser Callbacks can be called,</para>
        /// <para>since the debot tries to display all actions from the context 0 to the user.</para>
        /// <para>When the debot starts SDK registers `BrowserCallbacks` AppObject.</para>
        /// <para>Therefore when `debote.remove` is called the debot is being deleted and the callback is called</para>
        /// <para>with `finish`=`true` which indicates that it will never be used again.</para>
        /// </summary>
        public async Task Start(ParamsOfStart @params, CancellationToken cancellationToken = default)
        {
            await _everClientAdapter.Request<ParamsOfStart>("debot.start", @params, cancellationToken);
        }

        /// <summary>
        /// <para>[UNSTABLE](UNSTABLE.md) Fetches DeBot metadata from blockchain.</para>
        /// <para>Downloads DeBot from blockchain and creates and fetches its metadata.</para>
        /// </summary>
        public async Task<ResultOfFetch> Fetch(ParamsOfFetch @params, CancellationToken cancellationToken = default)
        {
            return await _everClientAdapter.Request<ParamsOfFetch, ResultOfFetch>("debot.fetch", @params, cancellationToken);
        }

        /// <summary>
        /// <para>[UNSTABLE](UNSTABLE.md) Executes debot action.</para>
        /// <para>Calls debot engine referenced by debot handle to execute input action.</para>
        /// <para>Calls Debot Browser Callbacks if needed.</para>
        /// <para># Remarks</para>
        /// <para>Chain of actions can be executed if input action generates a list of subactions.</para>
        /// </summary>
        public async Task Execute(ParamsOfExecute @params, CancellationToken cancellationToken = default)
        {
            await _everClientAdapter.Request<ParamsOfExecute>("debot.execute", @params, cancellationToken);
        }

        /// <summary>
        /// <para>[UNSTABLE](UNSTABLE.md) Sends message to Debot.</para>
        /// <para>Used by Debot Browser to send response on Dinterface call or from other Debots.</para>
        /// </summary>
        public async Task Send(ParamsOfSend @params, CancellationToken cancellationToken = default)
        {
            await _everClientAdapter.Request<ParamsOfSend>("debot.send", @params, cancellationToken);
        }

        /// <summary>
        /// <para>[UNSTABLE](UNSTABLE.md) Destroys debot handle.</para>
        /// <para>Removes handle from Client Context and drops debot engine referenced by that handle.</para>
        /// </summary>
        public async Task Remove(ParamsOfRemove @params, CancellationToken cancellationToken = default)
        {
            await _everClientAdapter.Request<ParamsOfRemove>("debot.remove", @params, cancellationToken);
        }
    }
}