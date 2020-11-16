using ch1seL.TonNet.Abstract;
using ch1seL.TonNet.Client.Abstract;
using ch1seL.TonNet.Client.Models;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace ch1seL.TonNet.Client
{
    public interface IClient : ITonModule
    {
        public Task<GetApiReferenceResponse> GetApiReference(CancellationToken cancellationToken = default);
        public Task<VersionResponse> Version(CancellationToken cancellationToken = default);
        public Task<BuildInfoResponse> BuildInfo(CancellationToken cancellationToken = default);
    }
}