using ch1seL.TonNet.Abstract;
using ch1seL.TonNet.Client.Abstract;
using ch1seL.TonNet.Client.Models;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace ch1seL.TonNet.Client
{
    public class Client : IClient
    {
        private readonly ITonClientAdapter _tonClientAdapter;

        public Client(ITonClientAdapter tonClientAdapter)
        {
            _tonClientAdapter = tonClientAdapter;
        }

        public async Task<GetApiReferenceResponse> GetApiReference(CancellationToken cancellationToken = default)
        {
            return await _tonClientAdapter.Request<GetApiReferenceResponse>("client.get_api_reference", cancellationToken);
        }

        public async Task<VersionResponse> Version(CancellationToken cancellationToken = default)
        {
            return await _tonClientAdapter.Request<VersionResponse>("client.version", cancellationToken);
        }

        public async Task<BuildInfoResponse> BuildInfo(CancellationToken cancellationToken = default)
        {
            return await _tonClientAdapter.Request<BuildInfoResponse>("client.build_info", cancellationToken);
        }
    }
}