using ch1seL.TonNet.Abstract;
using ch1seL.TonNet.Abstract.Modules;
using ch1seL.TonNet.Client.Models;
using System;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;

namespace ch1seL.TonNet.Client.Modules
{
    public class BocModule : IBocModule
    {
        private readonly ITonClientAdapter _tonClientAdapter;

        public BocModule(ITonClientAdapter tonClientAdapter)
        {
            _tonClientAdapter = tonClientAdapter;
        }

        /// <summary>
        /// JSON structure is compatible with GraphQL API message object
        /// </summary>
        public async Task<ResultOfParse> ParseMessage(ParamsOfParse @params, CancellationToken cancellationToken = default)
        {
            return await _tonClientAdapter.Request<ParamsOfParse, ResultOfParse>("boc.parse_message", @params, cancellationToken);
        }

        /// <summary>
        /// JSON structure is compatible with GraphQL API transaction object
        /// </summary>
        public async Task<ResultOfParse> ParseTransaction(ParamsOfParse @params, CancellationToken cancellationToken = default)
        {
            return await _tonClientAdapter.Request<ParamsOfParse, ResultOfParse>("boc.parse_transaction", @params, cancellationToken);
        }

        /// <summary>
        /// JSON structure is compatible with GraphQL API account object
        /// </summary>
        public async Task<ResultOfParse> ParseAccount(ParamsOfParse @params, CancellationToken cancellationToken = default)
        {
            return await _tonClientAdapter.Request<ParamsOfParse, ResultOfParse>("boc.parse_account", @params, cancellationToken);
        }

        /// <summary>
        /// JSON structure is compatible with GraphQL API block object
        /// </summary>
        public async Task<ResultOfParse> ParseBlock(ParamsOfParse @params, CancellationToken cancellationToken = default)
        {
            return await _tonClientAdapter.Request<ParamsOfParse, ResultOfParse>("boc.parse_block", @params, cancellationToken);
        }

        /// <summary>
        /// JSON structure is compatible with GraphQL API shardstate object
        /// </summary>
        public async Task<ResultOfParse> ParseShardstate(ParamsOfParseShardstate @params, CancellationToken cancellationToken = default)
        {
            return await _tonClientAdapter.Request<ParamsOfParseShardstate, ResultOfParse>("boc.parse_shardstate", @params, cancellationToken);
        }

        /// <summary>
        /// Not described yet..
        /// </summary>
        public async Task<ResultOfGetBlockchainConfig> GetBlockchainConfig(ParamsOfGetBlockchainConfig @params, CancellationToken cancellationToken = default)
        {
            return await _tonClientAdapter.Request<ParamsOfGetBlockchainConfig, ResultOfGetBlockchainConfig>("boc.get_blockchain_config", @params, cancellationToken);
        }

        /// <summary>
        /// Not described yet..
        /// </summary>
        public async Task<ResultOfGetBocHash> GetBocHash(ParamsOfGetBocHash @params, CancellationToken cancellationToken = default)
        {
            return await _tonClientAdapter.Request<ParamsOfGetBocHash, ResultOfGetBocHash>("boc.get_boc_hash", @params, cancellationToken);
        }
    }
}