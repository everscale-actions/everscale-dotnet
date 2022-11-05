using EverscaleNet.Abstract;
using EverscaleNet.Abstract.Modules;
using EverscaleNet.Client.Models;
using System;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;

namespace EverscaleNet.Client.Modules
{
    /// <summary>
    /// <para>Client Module</para>
    /// </summary>
    public class ClientModule : IClientModule
    {
        private readonly IEverClientAdapter _everClientAdapter;

        /// <summary>
        /// <para>.ctor</para>
        /// </summary>
        public ClientModule(IEverClientAdapter everClientAdapter)
        {
            _everClientAdapter = everClientAdapter;
        }

        /// <summary>
        /// <para>Returns Core Library API reference</para>
        /// </summary>
        public async Task<ResultOfGetApiReference> GetApiReference(CancellationToken cancellationToken = default)
        {
            return await _everClientAdapter.Request<ResultOfGetApiReference>("client.get_api_reference", cancellationToken);
        }

        /// <summary>
        /// <para>Returns Core Library version</para>
        /// </summary>
        public async Task<ResultOfVersion> Version(CancellationToken cancellationToken = default)
        {
            return await _everClientAdapter.Request<ResultOfVersion>("client.version", cancellationToken);
        }

        /// <summary>
        /// <para>Returns Core Library API reference</para>
        /// </summary>
        public async Task<ClientConfig> Config(CancellationToken cancellationToken = default)
        {
            return await _everClientAdapter.Request<ClientConfig>("client.config", cancellationToken);
        }

        /// <summary>
        /// <para>Returns detailed information about this build.</para>
        /// </summary>
        public async Task<ResultOfBuildInfo> BuildInfo(CancellationToken cancellationToken = default)
        {
            return await _everClientAdapter.Request<ResultOfBuildInfo>("client.build_info", cancellationToken);
        }

        /// <summary>
        /// <para>Resolves application request processing result</para>
        /// </summary>
        public async Task ResolveAppRequest(ParamsOfResolveAppRequest @params, CancellationToken cancellationToken = default)
        {
            await _everClientAdapter.Request<ParamsOfResolveAppRequest>("client.resolve_app_request", @params, cancellationToken);
        }
    }
}