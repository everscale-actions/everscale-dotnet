using ch1seL.TonNet.Abstract;
using ch1seL.TonNet.Client.Abstract;
using ch1seL.TonNet.Client.Models;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace ch1seL.TonNet.Client
{
    public interface IBoc : ITonModule
    {
        public Task<ParseResponse> ParseMessage(ParseRequest @params, CancellationToken cancellationToken = default);
        public Task<ParseResponse> ParseTransaction(ParseRequest @params, CancellationToken cancellationToken = default);
        public Task<ParseResponse> ParseAccount(ParseRequest @params, CancellationToken cancellationToken = default);
        public Task<ParseResponse> ParseBlock(ParseRequest @params, CancellationToken cancellationToken = default);
        public Task<ParseResponse> ParseShardstate(ParseShardstateRequest @params, CancellationToken cancellationToken = default);
        public Task<GetBlockchainConfigResponse> GetBlockchainConfig(GetBlockchainConfigRequest @params, CancellationToken cancellationToken = default);
        public Task<GetBocHashResponse> GetBocHash(GetBocHashRequest @params, CancellationToken cancellationToken = default);
    }
}