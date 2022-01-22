using EverscaleNet.Client.Models;
using System;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;

namespace EverscaleNet.Abstract.Modules
{
    public interface IClientModule : IEverModule
    {
        /// <summary>
        /// Returns Core Library API reference
        /// </summary>
        public Task<ResultOfGetApiReference> GetApiReference(CancellationToken cancellationToken = default);

        /// <summary>
        /// Returns Core Library version
        /// </summary>
        public Task<ResultOfVersion> Version(CancellationToken cancellationToken = default);

        /// <summary>
        /// Returns detailed information about this build.
        /// </summary>
        public Task<ResultOfBuildInfo> BuildInfo(CancellationToken cancellationToken = default);

        /// <summary>
        /// Resolves application request processing result
        /// </summary>
        public Task ResolveAppRequest(ParamsOfResolveAppRequest @params, CancellationToken cancellationToken = default);
    }
}