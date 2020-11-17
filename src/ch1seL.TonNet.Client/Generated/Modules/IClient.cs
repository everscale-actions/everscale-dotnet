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
        /// <summary>
        ///  Returns Core Library API reference
        /// </summary>
        public Task<GetApiReferenceResponse> GetApiReference(CancellationToken cancellationToken = default);

        /// <summary>
        ///  Returns Core Library version
        /// </summary>
        public Task<VersionResponse> Version(CancellationToken cancellationToken = default);

        /// <summary>
        ///  Returns detailed information about this build.
        /// </summary>
        public Task<BuildInfoResponse> BuildInfo(CancellationToken cancellationToken = default);
    }
}