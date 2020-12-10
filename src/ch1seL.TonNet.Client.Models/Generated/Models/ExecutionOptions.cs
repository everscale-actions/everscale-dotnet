using Dahomey.Json.Attributes;
using System;
using System.Numerics;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace ch1seL.TonNet.Client.Models
{
    /// <summary>
    /// Not described yet..
    /// </summary>
    public class ExecutionOptions
    {
        /// <summary>
        /// Not described yet..
        /// </summary>
        [JsonPropertyName("blockchain_config")]
        public string BlockchainConfig { get; set; }

        /// <summary>
        /// Not described yet..
        /// </summary>
        [JsonPropertyName("block_time")]
        public uint? BlockTime { get; set; }

        /// <summary>
        /// Not described yet..
        /// </summary>
        [JsonPropertyName("block_lt")]
        public ulong? BlockLt { get; set; }

        /// <summary>
        /// Not described yet..
        /// </summary>
        [JsonPropertyName("transaction_lt")]
        public ulong? TransactionLt { get; set; }
    }
}