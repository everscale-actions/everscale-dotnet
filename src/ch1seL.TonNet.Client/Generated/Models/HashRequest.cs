using System;
using System.Numerics;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace ch1seL.TonNet.Client.Models
{
    public class HashRequest
    {
        [JsonPropertyName("data")]
        public string Data { get; set; }
    }
}