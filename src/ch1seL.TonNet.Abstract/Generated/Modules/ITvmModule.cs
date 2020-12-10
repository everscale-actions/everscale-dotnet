using ch1seL.TonNet.Client.Models;
using System;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;

namespace ch1seL.TonNet.Abstract.Modules
{
    public interface ITvmModule : ITonModule
    {
        /// <summary>
        /// Not described yet..
        /// </summary>
        public Task<ResultOfRunExecutor> RunExecutor(ParamsOfRunExecutor @params, CancellationToken cancellationToken = default);

        /// <summary>
        /// Not described yet..
        /// </summary>
        public Task<ResultOfRunTvm> RunTvm(ParamsOfRunTvm @params, CancellationToken cancellationToken = default);

        /// <summary>
        /// Executes getmethod and returns data from TVM stack
        /// </summary>
        public Task<ResultOfRunGet> RunGet(ParamsOfRunGet @params, CancellationToken cancellationToken = default);
    }
}