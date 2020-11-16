using System;
using System.Numerics;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace ch1seL.TonNet.Client.Models
{
    public class SignRequest
    {
        [JsonPropertyName("unsigned")]
        public string Unsigned { get; set; }
        [JsonPropertyName("keys")]
        public KeyPair Keys { get; set; }
    }
}