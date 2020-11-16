using System;
using System.Numerics;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace ch1seL.TonNet.Client.Models
{
    public class WaitForTransactionRequest
    {
        [JsonPropertyName("abi")]
        public Abi Abi { get; set; }
        [JsonPropertyName("message")]
        public string Message { get; set; }
        [JsonPropertyName("shard_block_id")]
        public string ShardBlockId { get; set; }
        [JsonPropertyName("send_events")]
        public bool SendEvents { get; set; }
    }
}