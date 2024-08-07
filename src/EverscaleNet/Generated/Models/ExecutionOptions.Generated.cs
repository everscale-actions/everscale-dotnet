using System;
using System.Numerics;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace EverscaleNet.Client.Models
{
    /// <summary>
    /// <para>Not described yet..</para>
    /// </summary>
    public class ExecutionOptions
    {
        /// <summary>
        /// <para>boc with config</para>
        /// </summary>
        [JsonPropertyName("blockchain_config")]
        public string BlockchainConfig { get; set; }

        /// <summary>
        /// <para>time that is used as transaction time</para>
        /// </summary>
        [JsonPropertyName("block_time")]
        public uint? BlockTime { get; set; }

        /// <summary>
        /// <para>block logical time</para>
        /// </summary>
        [JsonPropertyName("block_lt")]
        public BigInteger? BlockLt { get; set; }

        /// <summary>
        /// <para>transaction logical time</para>
        /// </summary>
        [JsonPropertyName("transaction_lt")]
        public BigInteger? TransactionLt { get; set; }

        /// <summary>
        /// <para>Overrides standard TVM behaviour. If set to `true` then CHKSIG always will return `true`.</para>
        /// </summary>
        [JsonPropertyName("chksig_always_succeed")]
        public bool? ChksigAlwaysSucceed { get; set; }

        /// <summary>
        /// <para>Signature ID to be used in signature verifying instructions when CapSignatureWithId capability is enabled</para>
        /// </summary>
        [JsonPropertyName("signature_id")]
        public int? SignatureId { get; set; }
    }
}