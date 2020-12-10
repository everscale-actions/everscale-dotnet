using ch1seL.TonNet.Client.Models;
using System;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;

namespace ch1seL.TonNet.Abstract.Modules
{
    public interface IBocModule : ITonModule
    {
        /// <summary>
        /// JSON structure is compatible with GraphQL API message object
        /// </summary>
        public Task<ResultOfParse> ParseMessage(ParamsOfParse @params, CancellationToken cancellationToken = default);

        /// <summary>
        /// JSON structure is compatible with GraphQL API transaction object
        /// </summary>
        public Task<ResultOfParse> ParseTransaction(ParamsOfParse @params, CancellationToken cancellationToken = default);

        /// <summary>
        /// JSON structure is compatible with GraphQL API account object
        /// </summary>
        public Task<ResultOfParse> ParseAccount(ParamsOfParse @params, CancellationToken cancellationToken = default);

        /// <summary>
        /// JSON structure is compatible with GraphQL API block object
        /// </summary>
        public Task<ResultOfParse> ParseBlock(ParamsOfParse @params, CancellationToken cancellationToken = default);

        /// <summary>
        /// JSON structure is compatible with GraphQL API shardstate object
        /// </summary>
        public Task<ResultOfParse> ParseShardstate(ParamsOfParseShardstate @params, CancellationToken cancellationToken = default);

        /// <summary>
        /// Not described yet..
        /// </summary>
        public Task<ResultOfGetBlockchainConfig> GetBlockchainConfig(ParamsOfGetBlockchainConfig @params, CancellationToken cancellationToken = default);

        /// <summary>
        /// Not described yet..
        /// </summary>
        public Task<ResultOfGetBocHash> GetBocHash(ParamsOfGetBocHash @params, CancellationToken cancellationToken = default);
    }
}