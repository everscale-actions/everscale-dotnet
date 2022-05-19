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
    /// Client Module
    /// </summary>
    public class ClientModule : IClientModule
    {
        private readonly IEverClientAdapter _everClientAdapter;

        /// <summary>
        /// .ctor
        /// </summary>
        public ClientModule(IEverClientAdapter everClientAdapter)
        {
            _everClientAdapter = everClientAdapter;
        }

        /// <summary>
        /// Returns Core Library API reference
        /// </summary>
        public async Task<ResultOfGetApiReference> GetApiReference(CancellationToken cancellationToken = default)
        {
            return await _everClientAdapter.Request<ResultOfGetApiReference>("client.get_api_reference", cancellationToken);
        }

        /// <summary>
        /// Returns Core Library version
        /// </summary>
        public async Task<ResultOfVersion> Version(CancellationToken cancellationToken = default)
        {
            return await _everClientAdapter.Request<ResultOfVersion>("client.version", cancellationToken);
        }

        /// <summary>
        /// Returns Core Library API reference
        /// </summary>
        public async Task<ClientConfig> Config(CancellationToken cancellationToken = default)
        {
            return await _everClientAdapter.Request<ClientConfig>("client.config", cancellationToken);
        }

        /// <summary>
        /// Returns detailed information about this build.
        /// </summary>
        public async Task<ResultOfBuildInfo> BuildInfo(CancellationToken cancellationToken = default)
        {
            return await _everClientAdapter.Request<ResultOfBuildInfo>("client.build_info", cancellationToken);
        }

        /// <summary>
        /// Resolves application request processing result
        /// </summary>
        public async Task ResolveAppRequest(ParamsOfResolveAppRequest @params, CancellationToken cancellationToken = default)
        {
            await _everClientAdapter.Request<ParamsOfResolveAppRequest>("client.resolve_app_request", @params, cancellationToken);
        }
    }
}