using System;
using System.Numerics;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace ch1seL.TonNet.Client.Models
{
    public class ParseShardstateRequest
    {
        [JsonPropertyName("boc")]
        public string Boc { get; set; }
        [JsonPropertyName("id")]
        public string Id { get; set; }
        [JsonPropertyName("workchain_id")]
        public int WorkchainId { get; set; }
    }
}