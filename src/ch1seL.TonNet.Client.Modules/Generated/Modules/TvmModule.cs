using ch1seL.TonNet.Abstract;
using ch1seL.TonNet.Abstract.Modules;
using ch1seL.TonNet.Client.Models;
using System;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;

namespace ch1seL.TonNet.Client.Modules
{
    public class TvmModule : ITvmModule
    {
        private readonly ITonClientAdapter _tonClientAdapter;

        public TvmModule(ITonClientAdapter tonClientAdapter)
        {
            _tonClientAdapter = tonClientAdapter;
        }

        /// <summary>
        /// <para>Emulates all the phases of contract execution locally</para>
        /// <para>Performs all the phases of contract execution on Transaction Executor -</para>
        /// <para>the same component that is used on Validator Nodes.</para>
        /// <para>Can be used for contract debug, to find out the reason of message unsuccessful</para>
        /// <para>delivery - as Validators just throw away failed transactions, here you can catch it.</para>
        /// <para>Another use case is to estimate fees for message execution. Set  `AccountForExecutor::Account.unlimited_balance`</para>
        /// <para>to `true` so that emulation will not depend on the actual balance.</para>
        /// <para>One more use case - you can procude the sequence of operations,</para>
        /// <para>thus emulating the multiple contract calls locally.</para>
        /// <para>And so on.</para>
        /// <para>To get the account boc (bag of cells) - use `net.query` method to download it from graphql api</para>
        /// <para>(field `boc` of `account`) or generate it with `abi.encode_account method`.</para>
        /// <para>To get the message boc - use `abi.encode_message` or prepare it any other way, for instance, with Fift script.</para>
        /// <para>If you need this emulation to be as precise as possible then specify `ParamsOfRunExecutor` parameter.</para>
        /// <para>If you need to see the aborted transaction as a result, not as an error, set `skip_transaction_check` to `true`.</para>
        /// </summary>
        public async Task<ResultOfRunExecutor> RunExecutor(ParamsOfRunExecutor @params, CancellationToken cancellationToken = default)
        {
            return await _tonClientAdapter.Request<ParamsOfRunExecutor, ResultOfRunExecutor>("tvm.run_executor", @params, cancellationToken);
        }

        /// <summary>
        /// <para>Executes get methods of ABI-compatible contracts</para>
        /// <para>Performs only a part of compute phase of transaction execution</para>
        /// <para>that is used to run get-methods of ABI-compatible contracts.</para>
        /// <para>If you try to run get methods with `run_executor` you will get an error, because it checks ACCEPT and exits</para>
        /// <para>if there is none, which is actually true for get methods.</para>
        /// <para> To get the account boc (bag of cells) - use `net.query` method to download it from graphql api</para>
        /// <para>(field `boc` of `account`) or generate it with `abi.encode_account method`.</para>
        /// <para>To get the message boc - use `abi.encode_message` or prepare it any other way, for instance, with Fift script.</para>
        /// <para>Attention! Updated account state is produces as well, but only</para>
        /// <para>`account_state.storage.state.data`  part of the boc is updated.</para>
        /// </summary>
        public async Task<ResultOfRunTvm> RunTvm(ParamsOfRunTvm @params, CancellationToken cancellationToken = default)
        {
            return await _tonClientAdapter.Request<ParamsOfRunTvm, ResultOfRunTvm>("tvm.run_tvm", @params, cancellationToken);
        }

        /// <summary>
        /// <para>Executes a getmethod of FIFT contract</para>
        /// <para>Executes a getmethod of FIFT contract that fulfills the smc-guidelines https://test.ton.org/smc-guidelines.txt</para>
        /// <para>and returns the result data from TVM's stack</para>
        /// </summary>
        public async Task<ResultOfRunGet> RunGet(ParamsOfRunGet @params, CancellationToken cancellationToken = default)
        {
            return await _tonClientAdapter.Request<ParamsOfRunGet, ResultOfRunGet>("tvm.run_get", @params, cancellationToken);
        }
    }
}