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
        /// <summary>
        /// <para> Parses message boc into a JSON </para>
        /// <para> </para>
        /// <para> JSON structure is compatible with GraphQL API message object</para>
        /// </summary>
        public Task<ParseResponse> ParseMessage(ParseRequest @params, CancellationToken cancellationToken = default);

        /// <summary>
        /// <para> Parses transaction boc into a JSON </para>
        /// <para> </para>
        /// <para> JSON structure is compatible with GraphQL API transaction object</para>
        /// </summary>
        public Task<ParseResponse> ParseTransaction(ParseRequest @params, CancellationToken cancellationToken = default);

        /// <summary>
        /// <para> Parses account boc into a JSON </para>
        /// <para> </para>
        /// <para> JSON structure is compatible with GraphQL API account object</para>
        /// </summary>
        public Task<ParseResponse> ParseAccount(ParseRequest @params, CancellationToken cancellationToken = default);

        /// <summary>
        /// <para> Parses block boc into a JSON </para>
        /// <para> </para>
        /// <para> JSON structure is compatible with GraphQL API block object</para>
        /// </summary>
        public Task<ParseResponse> ParseBlock(ParseRequest @params, CancellationToken cancellationToken = default);

        /// <summary>
        /// <para> Parses shardstate boc into a JSON </para>
        /// <para> </para>
        /// <para> JSON structure is compatible with GraphQL API shardstate object</para>
        /// </summary>
        public Task<ParseResponse> ParseShardstate(ParseShardstateRequest @params, CancellationToken cancellationToken = default);

        /// <summary>
        /// Not described yet..
        /// </summary>
        public Task<GetBlockchainConfigResponse> GetBlockchainConfig(GetBlockchainConfigRequest @params, CancellationToken cancellationToken = default);

        /// <summary>
        ///  Calculates BOC root hash
        /// </summary>
        public Task<GetBocHashResponse> GetBocHash(GetBocHashRequest @params, CancellationToken cancellationToken = default);
    }
}