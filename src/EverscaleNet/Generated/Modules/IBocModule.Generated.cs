using EverscaleNet.Client.Models;
using System;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;

namespace EverscaleNet.Abstract.Modules
{
    public interface IBocModule : IEverModule
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
        /// Extract blockchain configuration from key block and also from zerostate.
        /// </summary>
        public Task<ResultOfGetBlockchainConfig> GetBlockchainConfig(ParamsOfGetBlockchainConfig @params, CancellationToken cancellationToken = default);

        /// <summary>
        /// Calculates BOC root hash
        /// </summary>
        public Task<ResultOfGetBocHash> GetBocHash(ParamsOfGetBocHash @params, CancellationToken cancellationToken = default);

        /// <summary>
        /// Calculates BOC depth
        /// </summary>
        public Task<ResultOfGetBocDepth> GetBocDepth(ParamsOfGetBocDepth @params, CancellationToken cancellationToken = default);

        /// <summary>
        /// Extracts code from TVC contract image
        /// </summary>
        public Task<ResultOfGetCodeFromTvc> GetCodeFromTvc(ParamsOfGetCodeFromTvc @params, CancellationToken cancellationToken = default);

        /// <summary>
        /// Get BOC from cache
        /// </summary>
        public Task<ResultOfBocCacheGet> CacheGet(ParamsOfBocCacheGet @params, CancellationToken cancellationToken = default);

        /// <summary>
        /// Save BOC into cache
        /// </summary>
        public Task<ResultOfBocCacheSet> CacheSet(ParamsOfBocCacheSet @params, CancellationToken cancellationToken = default);

        /// <summary>
        /// <para>Unpin BOCs with specified pin.</para>
        /// <para>BOCs which don't have another pins will be removed from cache</para>
        /// </summary>
        public Task CacheUnpin(ParamsOfBocCacheUnpin @params, CancellationToken cancellationToken = default);

        /// <summary>
        /// Encodes bag of cells (BOC) with builder operations. This method provides the same functionality as Solidity TvmBuilder. Resulting BOC of this method can be passed into Solidity and C++ contracts as TvmCell type
        /// </summary>
        public Task<ResultOfEncodeBoc> EncodeBoc(ParamsOfEncodeBoc @params, CancellationToken cancellationToken = default);

        /// <summary>
        /// Returns the contract code's salt if it is present.
        /// </summary>
        public Task<ResultOfGetCodeSalt> GetCodeSalt(ParamsOfGetCodeSalt @params, CancellationToken cancellationToken = default);

        /// <summary>
        /// <para>Sets new salt to contract code.</para>
        /// <para>Returns the new contract code with salt.</para>
        /// </summary>
        public Task<ResultOfSetCodeSalt> SetCodeSalt(ParamsOfSetCodeSalt @params, CancellationToken cancellationToken = default);

        /// <summary>
        /// Decodes tvc into code, data, libraries and special options.
        /// </summary>
        public Task<ResultOfDecodeTvc> DecodeTvc(ParamsOfDecodeTvc @params, CancellationToken cancellationToken = default);

        /// <summary>
        /// Encodes tvc from code, data, libraries ans special options (see input params)
        /// </summary>
        public Task<ResultOfEncodeTvc> EncodeTvc(ParamsOfEncodeTvc @params, CancellationToken cancellationToken = default);

        /// <summary>
        /// Returns the compiler version used to compile the code.
        /// </summary>
        public Task<ResultOfGetCompilerVersion> GetCompilerVersion(ParamsOfGetCompilerVersion @params, CancellationToken cancellationToken = default);
    }
}