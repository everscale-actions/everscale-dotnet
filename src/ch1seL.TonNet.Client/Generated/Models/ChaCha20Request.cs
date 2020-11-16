using System;
using System.Numerics;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace ch1seL.TonNet.Client.Models
{
    public class ChaCha20Request
    {
        [JsonPropertyName("data")]
        public string Data { get; set; }
        [JsonPropertyName("key")]
        public string Key { get; set; }
        [JsonPropertyName("nonce")]
        public string Nonce { get; set; }
    }
}