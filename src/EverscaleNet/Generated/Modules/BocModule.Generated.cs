using EverscaleNet.Abstract;
using EverscaleNet.Abstract.Modules;
using EverscaleNet.Client.Models;
using System;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;

namespace EverscaleNet.Client.Modules
{
    /// <summary>
    /// Boc Module
    /// </summary>
    public class BocModule : IBocModule
    {
        private readonly IEverClientAdapter _everClientAdapter;

        /// <summary>
        /// .ctor
        /// </summary>
        public BocModule(IEverClientAdapter everClientAdapter)
        {
            _everClientAdapter = everClientAdapter;
        }

        /// <summary>
        /// <para>Parses message boc into a JSON</para>
        /// <para>JSON structure is compatible with GraphQL API message object</para>
        /// </summary>
        public async Task<ResultOfParse> ParseMessage(ParamsOfParse @params, CancellationToken cancellationToken = default)
        {
            return await _everClientAdapter.Request<ParamsOfParse, ResultOfParse>("boc.parse_message", @params, cancellationToken);
        }

        /// <summary>
        /// <para>Parses transaction boc into a JSON</para>
        /// <para>JSON structure is compatible with GraphQL API transaction object</para>
        /// </summary>
        public async Task<ResultOfParse> ParseTransaction(ParamsOfParse @params, CancellationToken cancellationToken = default)
        {
            return await _everClientAdapter.Request<ParamsOfParse, ResultOfParse>("boc.parse_transaction", @params, cancellationToken);
        }

        /// <summary>
        /// <para>Parses account boc into a JSON</para>
        /// <para>JSON structure is compatible with GraphQL API account object</para>
        /// </summary>
        public async Task<ResultOfParse> ParseAccount(ParamsOfParse @params, CancellationToken cancellationToken = default)
        {
            return await _everClientAdapter.Request<ParamsOfParse, ResultOfParse>("boc.parse_account", @params, cancellationToken);
        }

        /// <summary>
        /// <para>Parses block boc into a JSON</para>
        /// <para>JSON structure is compatible with GraphQL API block object</para>
        /// </summary>
        public async Task<ResultOfParse> ParseBlock(ParamsOfParse @params, CancellationToken cancellationToken = default)
        {
            return await _everClientAdapter.Request<ParamsOfParse, ResultOfParse>("boc.parse_block", @params, cancellationToken);
        }

        /// <summary>
        /// <para>Parses shardstate boc into a JSON</para>
        /// <para>JSON structure is compatible with GraphQL API shardstate object</para>
        /// </summary>
        public async Task<ResultOfParse> ParseShardstate(ParamsOfParseShardstate @params, CancellationToken cancellationToken = default)
        {
            return await _everClientAdapter.Request<ParamsOfParseShardstate, ResultOfParse>("boc.parse_shardstate", @params, cancellationToken);
        }

        /// <summary>
        /// Extract blockchain configuration from key block and also from zerostate.
        /// </summary>
        public async Task<ResultOfGetBlockchainConfig> GetBlockchainConfig(ParamsOfGetBlockchainConfig @params, CancellationToken cancellationToken = default)
        {
            return await _everClientAdapter.Request<ParamsOfGetBlockchainConfig, ResultOfGetBlockchainConfig>("boc.get_blockchain_config", @params, cancellationToken);
        }

        /// <summary>
        /// Calculates BOC root hash
        /// </summary>
        public async Task<ResultOfGetBocHash> GetBocHash(ParamsOfGetBocHash @params, CancellationToken cancellationToken = default)
        {
            return await _everClientAdapter.Request<ParamsOfGetBocHash, ResultOfGetBocHash>("boc.get_boc_hash", @params, cancellationToken);
        }

        /// <summary>
        /// Calculates BOC depth
        /// </summary>
        public async Task<ResultOfGetBocDepth> GetBocDepth(ParamsOfGetBocDepth @params, CancellationToken cancellationToken = default)
        {
            return await _everClientAdapter.Request<ParamsOfGetBocDepth, ResultOfGetBocDepth>("boc.get_boc_depth", @params, cancellationToken);
        }

        /// <summary>
        /// Extracts code from TVC contract image
        /// </summary>
        public async Task<ResultOfGetCodeFromTvc> GetCodeFromTvc(ParamsOfGetCodeFromTvc @params, CancellationToken cancellationToken = default)
        {
            return await _everClientAdapter.Request<ParamsOfGetCodeFromTvc, ResultOfGetCodeFromTvc>("boc.get_code_from_tvc", @params, cancellationToken);
        }

        /// <summary>
        /// Get BOC from cache
        /// </summary>
        public async Task<ResultOfBocCacheGet> CacheGet(ParamsOfBocCacheGet @params, CancellationToken cancellationToken = default)
        {
            return await _everClientAdapter.Request<ParamsOfBocCacheGet, ResultOfBocCacheGet>("boc.cache_get", @params, cancellationToken);
        }

        /// <summary>
        /// Save BOC into cache or increase pin counter for existing pinned BOC
        /// </summary>
        public async Task<ResultOfBocCacheSet> CacheSet(ParamsOfBocCacheSet @params, CancellationToken cancellationToken = default)
        {
            return await _everClientAdapter.Request<ParamsOfBocCacheSet, ResultOfBocCacheSet>("boc.cache_set", @params, cancellationToken);
        }

        /// <summary>
        /// Unpin BOCs with specified pin defined in the `cache_set`. Decrease pin reference counter for BOCs with specified pin defined in the `cache_set`. BOCs which have only 1 pin and its reference counter become 0 will be removed from cache
        /// </summary>
        public async Task CacheUnpin(ParamsOfBocCacheUnpin @params, CancellationToken cancellationToken = default)
        {
            await _everClientAdapter.Request<ParamsOfBocCacheUnpin>("boc.cache_unpin", @params, cancellationToken);
        }

        /// <summary>
        /// Encodes bag of cells (BOC) with builder operations. This method provides the same functionality as Solidity TvmBuilder. Resulting BOC of this method can be passed into Solidity and C++ contracts as TvmCell type.
        /// </summary>
        public async Task<ResultOfEncodeBoc> EncodeBoc(ParamsOfEncodeBoc @params, CancellationToken cancellationToken = default)
        {
            return await _everClientAdapter.Request<ParamsOfEncodeBoc, ResultOfEncodeBoc>("boc.encode_boc", @params, cancellationToken);
        }

        /// <summary>
        /// Returns the contract code's salt if it is present.
        /// </summary>
        public async Task<ResultOfGetCodeSalt> GetCodeSalt(ParamsOfGetCodeSalt @params, CancellationToken cancellationToken = default)
        {
            return await _everClientAdapter.Request<ParamsOfGetCodeSalt, ResultOfGetCodeSalt>("boc.get_code_salt", @params, cancellationToken);
        }

        /// <summary>
        /// <para>Sets new salt to contract code.</para>
        /// <para>Returns the new contract code with salt.</para>
        /// </summary>
        public async Task<ResultOfSetCodeSalt> SetCodeSalt(ParamsOfSetCodeSalt @params, CancellationToken cancellationToken = default)
        {
            return await _everClientAdapter.Request<ParamsOfSetCodeSalt, ResultOfSetCodeSalt>("boc.set_code_salt", @params, cancellationToken);
        }

        /// <summary>
        /// Decodes tvc into code, data, libraries and special options.
        /// </summary>
        public async Task<ResultOfDecodeTvc> DecodeTvc(ParamsOfDecodeTvc @params, CancellationToken cancellationToken = default)
        {
            return await _everClientAdapter.Request<ParamsOfDecodeTvc, ResultOfDecodeTvc>("boc.decode_tvc", @params, cancellationToken);
        }

        /// <summary>
        /// Encodes tvc from code, data, libraries ans special options (see input params)
        /// </summary>
        public async Task<ResultOfEncodeTvc> EncodeTvc(ParamsOfEncodeTvc @params, CancellationToken cancellationToken = default)
        {
            return await _everClientAdapter.Request<ParamsOfEncodeTvc, ResultOfEncodeTvc>("boc.encode_tvc", @params, cancellationToken);
        }

        /// <summary>
        /// <para>Encodes a message</para>
        /// <para>Allows to encode any external inbound message.</para>
        /// </summary>
        public async Task<ResultOfEncodeExternalInMessage> EncodeExternalInMessage(ParamsOfEncodeExternalInMessage @params, CancellationToken cancellationToken = default)
        {
            return await _everClientAdapter.Request<ParamsOfEncodeExternalInMessage, ResultOfEncodeExternalInMessage>("boc.encode_external_in_message", @params, cancellationToken);
        }

        /// <summary>
        /// Returns the compiler version used to compile the code.
        /// </summary>
        public async Task<ResultOfGetCompilerVersion> GetCompilerVersion(ParamsOfGetCompilerVersion @params, CancellationToken cancellationToken = default)
        {
            return await _everClientAdapter.Request<ParamsOfGetCompilerVersion, ResultOfGetCompilerVersion>("boc.get_compiler_version", @params, cancellationToken);
        }
    }
}