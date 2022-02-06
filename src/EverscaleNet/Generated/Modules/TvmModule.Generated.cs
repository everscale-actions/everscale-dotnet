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
    /// Tvm Module
    /// </summary>
    public class TvmModule : ITvmModule
    {
        private readonly IEverClientAdapter _everClientAdapter;

        /// <summary>
        /// .ctor
        /// </summary>
        public TvmModule(IEverClientAdapter everClientAdapter)
        {
            _everClientAdapter = everClientAdapter;
        }

        /// <summary>
        /// <para>Emulates all the phases of contract execution locally</para>
        /// <para>Performs all the phases of contract execution on Transaction Executor -</para>
        /// <para>the same component that is used on Validator Nodes.</para>
        /// <para>Can be used for contract debugging, to find out the reason why a message was not delivered successfully.</para>
        /// <para>Validators throw away the failed external inbound messages (if they failed bedore `ACCEPT`) in the real network.</para>
        /// <para>This is why these messages are impossible to debug in the real network.</para>
        /// <para>With the help of run_executor you can do that. In fact, `process_message` function</para>
        /// <para>performs local check with `run_executor` if there was no transaction as a result of processing</para>
        /// <para>and returns the error, if there is one.</para>
        /// <para>Another use case to use `run_executor` is to estimate fees for message execution.</para>
        /// <para>Set  `AccountForExecutor::Account.unlimited_balance`</para>
        /// <para>to `true` so that emulation will not depend on the actual balance.</para>
        /// <para>This may be needed to calculate deploy fees for an account that does not exist yet.</para>
        /// <para>JSON with fees is in `fees` field of the result.</para>
        /// <para>One more use case - you can produce the sequence of operations,</para>
        /// <para>thus emulating the sequential contract calls locally.</para>
        /// <para>And so on.</para>
        /// <para>Transaction executor requires account BOC (bag of cells) as a parameter.</para>
        /// <para>To get the account BOC - use `net.query` method to download it from GraphQL API</para>
        /// <para>(field `boc` of `account`) or generate it with `abi.encode_account` method.</para>
        /// <para>Also it requires message BOC. To get the message BOC - use `abi.encode_message` or `abi.encode_internal_message`.</para>
        /// <para>If you need this emulation to be as precise as possible (for instance - emulate transaction</para>
        /// <para>with particular lt in particular block or use particular blockchain config,</para>
        /// <para>downloaded from a particular key block - then specify `execution_options` parameter.</para>
        /// <para>If you need to see the aborted transaction as a result, not as an error, set `skip_transaction_check` to `true`.</para>
        /// </summary>
        public async Task<ResultOfRunExecutor> RunExecutor(ParamsOfRunExecutor @params, CancellationToken cancellationToken = default)
        {
            return await _everClientAdapter.Request<ParamsOfRunExecutor, ResultOfRunExecutor>("tvm.run_executor", @params, cancellationToken);
        }

        /// <summary>
        /// <para>Executes get-methods of ABI-compatible contracts</para>
        /// <para>Performs only a part of compute phase of transaction execution</para>
        /// <para>that is used to run get-methods of ABI-compatible contracts.</para>
        /// <para>If you try to run get-methods with `run_executor` you will get an error, because it checks ACCEPT and exits</para>
        /// <para>if there is none, which is actually true for get-methods.</para>
        /// <para> To get the account BOC (bag of cells) - use `net.query` method to download it from GraphQL API</para>
        /// <para>(field `boc` of `account`) or generate it with `abi.encode_account method`.</para>
        /// <para>To get the message BOC - use `abi.encode_message` or prepare it any other way, for instance, with FIFT script.</para>
        /// <para>Attention! Updated account state is produces as well, but only</para>
        /// <para>`account_state.storage.state.data`  part of the BOC is updated.</para>
        /// </summary>
        public async Task<ResultOfRunTvm> RunTvm(ParamsOfRunTvm @params, CancellationToken cancellationToken = default)
        {
            return await _everClientAdapter.Request<ParamsOfRunTvm, ResultOfRunTvm>("tvm.run_tvm", @params, cancellationToken);
        }

        /// <summary>
        /// <para>Executes a get-method of FIFT contract</para>
        /// <para>Executes a get-method of FIFT contract that fulfills the smc-guidelines https://test.ton.org/smc-guidelines.txt</para>
        /// <para>and returns the result data from TVM's stack</para>
        /// </summary>
        public async Task<ResultOfRunGet> RunGet(ParamsOfRunGet @params, CancellationToken cancellationToken = default)
        {
            return await _everClientAdapter.Request<ParamsOfRunGet, ResultOfRunGet>("tvm.run_get", @params, cancellationToken);
        }
    }
}