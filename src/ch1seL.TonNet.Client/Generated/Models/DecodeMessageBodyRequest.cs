using System;
using System.Numerics;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace ch1seL.TonNet.Client.Models
{
    public class DecodeMessageBodyRequest
    {
        [JsonPropertyName("abi")]
        public Abi Abi { get; set; }
        [JsonPropertyName("body")]
        public string Body { get; set; }
        [JsonPropertyName("is_internal")]
        public bool IsInternal { get; set; }
    }
}