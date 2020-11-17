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
        ///  boc with config
        /// </summary>
        [JsonPropertyName("blockchain_config")]
        public string BlockchainConfig { get; set; }

        /// <summary>
        ///  time that is used as transaction time
        /// </summary>
        [JsonPropertyName("block_time"), JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public uint? BlockTime { get; set; }

        /// <summary>
        ///  block logical time
        /// </summary>
        [JsonPropertyName("block_lt"), JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public BigInteger? BlockLt { get; set; }

        /// <summary>
        ///  transaction logical time
        /// </summary>
        [JsonPropertyName("transaction_lt"), JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public BigInteger? TransactionLt { get; set; }
    }
}