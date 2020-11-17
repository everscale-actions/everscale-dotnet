using ch1seL.TonNet.Abstract;
using ch1seL.TonNet.Client.Abstract;
using ch1seL.TonNet.Client.Models;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace ch1seL.TonNet.Client
{
    public interface ITvm : ITonModule
    {
        /// <summary>
        /// Not described yet..
        /// </summary>
        public Task<RunExecutorResponse> RunExecutor(RunExecutorRequest @params, CancellationToken cancellationToken = default);

        /// <summary>
        /// Not described yet..
        /// </summary>
        public Task<RunTvmResponse> RunTvm(RunTvmRequest @params, CancellationToken cancellationToken = default);

        /// <summary>
        ///  Executes getmethod and returns data from TVM stack
        /// </summary>
        public Task<RunGetResponse> RunGet(RunGetRequest @params, CancellationToken cancellationToken = default);
    }
}