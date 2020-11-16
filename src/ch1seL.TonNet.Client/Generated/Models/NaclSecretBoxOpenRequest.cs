using System;
using System.Numerics;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace ch1seL.TonNet.Client.Models
{
    public class NaclSecretBoxOpenRequest
    {
        [JsonPropertyName("encrypted")]
        public string Encrypted { get; set; }
        [JsonPropertyName("nonce")]
        public string Nonce { get; set; }
        [JsonPropertyName("key")]
        public string Key { get; set; }
    }
}