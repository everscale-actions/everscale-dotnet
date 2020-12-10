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
        /// Not described yet..
        /// </summary>
        public async Task<ResultOfRunExecutor> RunExecutor(ParamsOfRunExecutor @params, CancellationToken cancellationToken = default)
        {
            return await _tonClientAdapter.Request<ParamsOfRunExecutor, ResultOfRunExecutor>("tvm.run_executor", @params, cancellationToken);
        }

        /// <summary>
        /// Not described yet..
        /// </summary>
        public async Task<ResultOfRunTvm> RunTvm(ParamsOfRunTvm @params, CancellationToken cancellationToken = default)
        {
            return await _tonClientAdapter.Request<ParamsOfRunTvm, ResultOfRunTvm>("tvm.run_tvm", @params, cancellationToken);
        }

        /// <summary>
        /// Executes getmethod and returns data from TVM stack
        /// </summary>
        public async Task<ResultOfRunGet> RunGet(ParamsOfRunGet @params, CancellationToken cancellationToken = default)
        {
            return await _tonClientAdapter.Request<ParamsOfRunGet, ResultOfRunGet>("tvm.run_get", @params, cancellationToken);
        }
    }
}