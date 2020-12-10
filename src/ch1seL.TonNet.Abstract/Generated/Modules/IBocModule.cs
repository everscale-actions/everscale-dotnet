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
        /// <para>Parses message boc into a JSON</para>
        /// <para>JSON structure is compatible with GraphQL API message object</para>
        /// </summary>
        public Task<ResultOfParse> ParseMessage(ParamsOfParse @params, CancellationToken cancellationToken = default);

        /// <summary>
        /// <para>Parses transaction boc into a JSON</para>
        /// <para>JSON structure is compatible with GraphQL API transaction object</para>
        /// </summary>
        public Task<ResultOfParse> ParseTransaction(ParamsOfParse @params, CancellationToken cancellationToken = default);

        /// <summary>
        /// <para>Parses account boc into a JSON</para>
        /// <para>JSON structure is compatible with GraphQL API account object</para>
        /// </summary>
        public Task<ResultOfParse> ParseAccount(ParamsOfParse @params, CancellationToken cancellationToken = default);

        /// <summary>
        /// <para>Parses block boc into a JSON</para>
        /// <para>JSON structure is compatible with GraphQL API block object</para>
        /// </summary>
        public Task<ResultOfParse> ParseBlock(ParamsOfParse @params, CancellationToken cancellationToken = default);

        /// <summary>
        /// <para>Parses shardstate boc into a JSON</para>
        /// <para>JSON structure is compatible with GraphQL API shardstate object</para>
        /// </summary>
        public Task<ResultOfParse> ParseShardstate(ParamsOfParseShardstate @params, CancellationToken cancellationToken = default);

        /// <summary>
        /// Not described yet..
        /// </summary>
        public Task<ResultOfGetBlockchainConfig> GetBlockchainConfig(ParamsOfGetBlockchainConfig @params, CancellationToken cancellationToken = default);

        /// <summary>
        /// Calculates BOC root hash
        /// </summary>
        public Task<ResultOfGetBocHash> GetBocHash(ParamsOfGetBocHash @params, CancellationToken cancellationToken = default);
    }
}