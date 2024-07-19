using EverscaleNet.Client.Models;
using System;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;

namespace EverscaleNet.Abstract.Modules
{
    /// <summary>
    /// <para>Boc Module</para>
    /// </summary>
    public interface IBocModule : IEverModule
    {
        /// <summary>
        /// <para>Decodes tvc according to the tvc spec. Read more about tvc structure here https://github.com/everx-labs/ever-struct/blob/main/src/scheme/mod.rs#L30</para>
        /// </summary>
        public Task<ResultOfDecodeTvc> DecodeTvc(ParamsOfDecodeTvc @params, CancellationToken cancellationToken = default);

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
        /// <para>Extract blockchain configuration from key block and also from zerostate.</para>
        /// </summary>
        public Task<ResultOfGetBlockchainConfig> GetBlockchainConfig(ParamsOfGetBlockchainConfig @params, CancellationToken cancellationToken = default);

        /// <summary>
        /// <para>Calculates BOC root hash</para>
        /// </summary>
        public Task<ResultOfGetBocHash> GetBocHash(ParamsOfGetBocHash @params, CancellationToken cancellationToken = default);

        /// <summary>
        /// <para>Calculates BOC depth</para>
        /// </summary>
        public Task<ResultOfGetBocDepth> GetBocDepth(ParamsOfGetBocDepth @params, CancellationToken cancellationToken = default);

        /// <summary>
        /// <para>Extracts code from TVC contract image</para>
        /// </summary>
        public Task<ResultOfGetCodeFromTvc> GetCodeFromTvc(ParamsOfGetCodeFromTvc @params, CancellationToken cancellationToken = default);

        /// <summary>
        /// <para>Get BOC from cache</para>
        /// </summary>
        public Task<ResultOfBocCacheGet> CacheGet(ParamsOfBocCacheGet @params, CancellationToken cancellationToken = default);

        /// <summary>
        /// <para>Save BOC into cache or increase pin counter for existing pinned BOC</para>
        /// </summary>
        public Task<ResultOfBocCacheSet> CacheSet(ParamsOfBocCacheSet @params, CancellationToken cancellationToken = default);

        /// <summary>
        /// <para>Unpin BOCs with specified pin defined in the `cache_set`. Decrease pin reference counter for BOCs with specified pin defined in the `cache_set`. BOCs which have only 1 pin and its reference counter become 0 will be removed from cache</para>
        /// </summary>
        public Task CacheUnpin(ParamsOfBocCacheUnpin @params, CancellationToken cancellationToken = default);

        /// <summary>
        /// <para>Encodes bag of cells (BOC) with builder operations. This method provides the same functionality as Solidity TvmBuilder. Resulting BOC of this method can be passed into Solidity and C++ contracts as TvmCell type.</para>
        /// </summary>
        public Task<ResultOfEncodeBoc> EncodeBoc(ParamsOfEncodeBoc @params, CancellationToken cancellationToken = default);

        /// <summary>
        /// <para>Returns the contract code's salt if it is present.</para>
        /// </summary>
        public Task<ResultOfGetCodeSalt> GetCodeSalt(ParamsOfGetCodeSalt @params, CancellationToken cancellationToken = default);

        /// <summary>
        /// <para>Sets new salt to contract code.</para>
        /// <para>Returns the new contract code with salt.</para>
        /// </summary>
        public Task<ResultOfSetCodeSalt> SetCodeSalt(ParamsOfSetCodeSalt @params, CancellationToken cancellationToken = default);

        /// <summary>
        /// <para>Decodes contract's initial state into code, data, libraries and special options.</para>
        /// </summary>
        public Task<ResultOfDecodeStateInit> DecodeStateInit(ParamsOfDecodeStateInit @params, CancellationToken cancellationToken = default);

        /// <summary>
        /// <para>Encodes initial contract state from code, data, libraries ans special options (see input params)</para>
        /// </summary>
        public Task<ResultOfEncodeStateInit> EncodeStateInit(ParamsOfEncodeStateInit @params, CancellationToken cancellationToken = default);

        /// <summary>
        /// <para>Encodes a message</para>
        /// <para>Allows to encode any external inbound message.</para>
        /// </summary>
        public Task<ResultOfEncodeExternalInMessage> EncodeExternalInMessage(ParamsOfEncodeExternalInMessage @params, CancellationToken cancellationToken = default);

        /// <summary>
        /// <para>Returns the compiler version used to compile the code.</para>
        /// </summary>
        public Task<ResultOfGetCompilerVersion> GetCompilerVersion(ParamsOfGetCompilerVersion @params, CancellationToken cancellationToken = default);
    }
}