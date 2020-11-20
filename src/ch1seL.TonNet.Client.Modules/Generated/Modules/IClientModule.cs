using ch1seL.TonNet.Abstract;
using ch1seL.TonNet.Client.Models;
using System;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;

namespace ch1seL.TonNet.Client
{
    public interface IClientModule : ITonModule
    {
        /// <summary>
        ///  Returns Core Library API reference
        /// </summary>
        public Task<ResultOfGetApiReference> GetApiReference(CancellationToken cancellationToken = default);

        /// <summary>
        ///  Returns Core Library version
        /// </summary>
        public Task<ResultOfVersion> Version(CancellationToken cancellationToken = default);

        /// <summary>
        ///  Returns detailed information about this build.
        /// </summary>
        public Task<ResultOfBuildInfo> BuildInfo(CancellationToken cancellationToken = default);
    }
}