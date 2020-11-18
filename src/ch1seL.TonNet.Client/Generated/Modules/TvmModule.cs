using ch1seL.TonNet.Abstract;
using ch1seL.TonNet.Client.Abstract;
using ch1seL.TonNet.Client.Models;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace ch1seL.TonNet.Client
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
        public async Task<RunExecutorResponse> RunExecutor(RunExecutorRequest @params, CancellationToken cancellationToken = default)
        {
            return await _tonClientAdapter.Request<RunExecutorRequest, RunExecutorResponse>("tvm.run_executor", @params, cancellationToken);
        }

        /// <summary>
        /// Not described yet..
        /// </summary>
        public async Task<RunTvmResponse> RunTvm(RunTvmRequest @params, CancellationToken cancellationToken = default)
        {
            return await _tonClientAdapter.Request<RunTvmRequest, RunTvmResponse>("tvm.run_tvm", @params, cancellationToken);
        }

        /// <summary>
        ///  Executes getmethod and returns data from TVM stack
        /// </summary>
        public async Task<RunGetResponse> RunGet(RunGetRequest @params, CancellationToken cancellationToken = default)
        {
            return await _tonClientAdapter.Request<RunGetRequest, RunGetResponse>("tvm.run_get", @params, cancellationToken);
        }
    }
}