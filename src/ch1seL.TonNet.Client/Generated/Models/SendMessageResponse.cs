using System;
using System.Numerics;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace ch1seL.TonNet.Client.Models
{
    public class SendMessageResponse
    {
        [JsonPropertyName("shard_block_id")]
        public string ShardBlockId { get; set; }
    }
}