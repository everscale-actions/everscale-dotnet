using EverscaleNet.Client.Models;
using System;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;

namespace EverscaleNet.Abstract.Modules
{
    /// <summary>
    /// <para>Client Module</para>
    /// </summary>
    public interface IClientModule : IEverModule
    {
        /// <summary>
        /// <para>Returns Core Library API reference</para>
        /// </summary>
        public Task<ResultOfGetApiReference> GetApiReference(CancellationToken cancellationToken = default);

        /// <summary>
        /// <para>Returns Core Library version</para>
        /// </summary>
        public Task<ResultOfVersion> Version(CancellationToken cancellationToken = default);

        /// <summary>
        /// <para>Returns Core Library API reference</para>
        /// </summary>
        public Task<ClientConfig> Config(CancellationToken cancellationToken = default);

        /// <summary>
        /// <para>Returns detailed information about this build.</para>
        /// </summary>
        public Task<ResultOfBuildInfo> BuildInfo(CancellationToken cancellationToken = default);

        /// <summary>
        /// <para>Resolves application request processing result</para>
        /// </summary>
        public Task ResolveAppRequest(ParamsOfResolveAppRequest @params, CancellationToken cancellationToken = default);
    }
}