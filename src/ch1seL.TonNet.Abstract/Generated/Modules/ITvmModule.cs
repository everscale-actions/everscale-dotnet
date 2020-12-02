using System.Threading;
using System.Threading.Tasks;
using ch1seL.TonNet.Client.Models;

namespace ch1seL.TonNet.Abstract.Modules
{
    public interface ITvmModule : ITonModule
    {
        /// <summary>
        ///     Not described yet..
        /// </summary>
        public Task<ResultOfRunExecutor> RunExecutor(ParamsOfRunExecutor @params, CancellationToken cancellationToken = default);

        /// <summary>
        ///     Not described yet..
        /// </summary>
        public Task<ResultOfRunTvm> RunTvm(ParamsOfRunTvm @params, CancellationToken cancellationToken = default);

        /// <summary>
        ///     Executes getmethod and returns data from TVM stack
        /// </summary>
        public Task<ResultOfRunGet> RunGet(ParamsOfRunGet @params, CancellationToken cancellationToken = default);
    }
}