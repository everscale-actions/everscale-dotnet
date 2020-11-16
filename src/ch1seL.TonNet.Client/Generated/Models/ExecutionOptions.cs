using System;
using System.Numerics;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace ch1seL.TonNet.Client.Models
{
    public class ExecutionOptions
    {
        [JsonPropertyName("blockchain_config")]
        public string BlockchainConfig { get; set; }
        [JsonPropertyName("block_time"), JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public uint? BlockTime { get; set; }
        [JsonPropertyName("block_lt"), JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public BigInteger? BlockLt { get; set; }
        [JsonPropertyName("transaction_lt"), JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public BigInteger? TransactionLt { get; set; }
    }
}