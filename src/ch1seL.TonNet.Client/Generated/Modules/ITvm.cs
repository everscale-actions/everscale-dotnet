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
        public Task<RunExecutorResponse> RunExecutor(RunExecutorRequest @params, CancellationToken cancellationToken = default);
        public Task<RunTvmResponse> RunTvm(RunTvmRequest @params, CancellationToken cancellationToken = default);
        public Task<RunGetResponse> RunGet(RunGetRequest @params, CancellationToken cancellationToken = default);
    }
}