using System;
using System.Numerics;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace ch1seL.TonNet.Client.Models
{
    public class HashResponse
    {
        [JsonPropertyName("hash")]
        public string Hash { get; set; }
    }
}