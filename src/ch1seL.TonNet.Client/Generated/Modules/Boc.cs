using ch1seL.TonNet.Abstract;
using ch1seL.TonNet.Client.Abstract;
using ch1seL.TonNet.Client.Models;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace ch1seL.TonNet.Client
{
    public class Boc : IBoc
    {
        private readonly ITonClientAdapter _tonClientAdapter;

        public Boc(ITonClientAdapter tonClientAdapter)
        {
            _tonClientAdapter = tonClientAdapter;
        }

        public async Task<ParseResponse> ParseMessage(ParseRequest @params, CancellationToken cancellationToken = default)
        {
            return await _tonClientAdapter.Request<ParseRequest, ParseResponse>("boc.parse_message", @params, cancellationToken);
        }

        public async Task<ParseResponse> ParseTransaction(ParseRequest @params, CancellationToken cancellationToken = default)
        {
            return await _tonClientAdapter.Request<ParseRequest, ParseResponse>("boc.parse_transaction", @params, cancellationToken);
        }

        public async Task<ParseResponse> ParseAccount(ParseRequest @params, CancellationToken cancellationToken = default)
        {
            return await _tonClientAdapter.Request<ParseRequest, ParseResponse>("boc.parse_account", @params, cancellationToken);
        }

        public async Task<ParseResponse> ParseBlock(ParseRequest @params, CancellationToken cancellationToken = default)
        {
            return await _tonClientAdapter.Request<ParseRequest, ParseResponse>("boc.parse_block", @params, cancellationToken);
        }

        public async Task<ParseResponse> ParseShardstate(ParseShardstateRequest @params, CancellationToken cancellationToken = default)
        {
            return await _tonClientAdapter.Request<ParseShardstateRequest, ParseResponse>("boc.parse_shardstate", @params, cancellationToken);
        }

        public async Task<GetBlockchainConfigResponse> GetBlockchainConfig(GetBlockchainConfigRequest @params, CancellationToken cancellationToken = default)
        {
            return await _tonClientAdapter.Request<GetBlockchainConfigRequest, GetBlockchainConfigResponse>("boc.get_blockchain_config", @params, cancellationToken);
        }

        public async Task<GetBocHashResponse> GetBocHash(GetBocHashRequest @params, CancellationToken cancellationToken = default)
        {
            return await _tonClientAdapter.Request<GetBocHashRequest, GetBocHashResponse>("boc.get_boc_hash", @params, cancellationToken);
        }
    }
}