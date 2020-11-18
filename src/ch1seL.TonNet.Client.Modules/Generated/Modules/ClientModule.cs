using ch1seL.TonNet.Abstract;
using ch1seL.TonNet.Client.Models;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace ch1seL.TonNet.Client
{
    public class ClientModule : IClientModule
    {
        private readonly ITonClientAdapter _tonClientAdapter;

        public ClientModule(ITonClientAdapter tonClientAdapter)
        {
            _tonClientAdapter = tonClientAdapter;
        }

        /// <summary>
        ///  Returns Core Library API reference
        /// </summary>
        public async Task<ResultOfGetApiReference> GetApiReference(CancellationToken cancellationToken = default)
        {
            return await _tonClientAdapter.Request<ResultOfGetApiReference>("client.get_api_reference", cancellationToken);
        }

        /// <summary>
        ///  Returns Core Library version
        /// </summary>
        public async Task<ResultOfVersion> Version(CancellationToken cancellationToken = default)
        {
            return await _tonClientAdapter.Request<ResultOfVersion>("client.version", cancellationToken);
        }

        /// <summary>
        ///  Returns detailed information about this build.
        /// </summary>
        public async Task<ResultOfBuildInfo> BuildInfo(CancellationToken cancellationToken = default)
        {
            return await _tonClientAdapter.Request<ResultOfBuildInfo>("client.build_info", cancellationToken);
        }
    }
}