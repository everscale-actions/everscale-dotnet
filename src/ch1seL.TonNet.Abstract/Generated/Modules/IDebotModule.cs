using ch1seL.TonNet.Client.Models;
using System;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;

namespace ch1seL.TonNet.Abstract.Modules
{
    public interface IDebotModule : ITonModule
    {
        /// <summary>
        /// <para>[UNSTABLE](UNSTABLE.md) Starts an instance of debot.</para>
        /// <para>Downloads debot smart contract from blockchain and switches it to</para>
        /// <para>context zero.</para>
        /// <para>Returns a debot handle which can be used later in `execute` function.</para>
        /// <para>This function must be used by Debot Browser to start a dialog with debot.</para>
        /// <para>While the function is executing, several Browser Callbacks can be called,</para>
        /// <para>since the debot tries to display all actions from the context 0 to the user.</para>
        /// <para># Remarks</para>
        /// <para>`start` is equivalent to `fetch` + switch to context 0.</para>
        /// </summary>
        public Task<RegisteredDebot> Start(ParamsOfStart @params, Action<JsonElement,uint> appObject = null, CancellationToken cancellationToken = default);

        /// <summary>
        /// <para>[UNSTABLE](UNSTABLE.md) Fetches debot from blockchain.</para>
        /// <para>Downloads debot smart contract (code and data) from blockchain and creates</para>
        /// <para>an instance of Debot Engine for it.</para>
        /// <para># Remarks</para>
        /// <para>It does not switch debot to context 0. Browser Callbacks are not called.</para>
        /// </summary>
        public Task<RegisteredDebot> Fetch(ParamsOfFetch @params, Action<JsonElement,uint> appObject = null, CancellationToken cancellationToken = default);

        /// <summary>
        /// <para>[UNSTABLE](UNSTABLE.md) Executes debot action.</para>
        /// <para>Calls debot engine referenced by debot handle to execute input action.</para>
        /// <para>Calls Debot Browser Callbacks if needed.</para>
        /// <para># Remarks</para>
        /// <para>Chain of actions can be executed if input action generates a list of subactions.</para>
        /// </summary>
        public Task Execute(ParamsOfExecute @params, CancellationToken cancellationToken = default);

        /// <summary>
        /// <para>[UNSTABLE](UNSTABLE.md) Destroys debot handle.</para>
        /// <para>Removes handle from Client Context and drops debot engine referenced by that handle.</para>
        /// </summary>
        public Task Remove(RegisteredDebot @params, CancellationToken cancellationToken = default);
    }
}