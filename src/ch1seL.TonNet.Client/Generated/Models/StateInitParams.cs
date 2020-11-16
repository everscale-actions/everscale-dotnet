using System;
using System.Numerics;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace ch1seL.TonNet.Client.Models
{
    public class StateInitParams
    {
        [JsonPropertyName("abi")]
        public Abi Abi { get; set; }
        [JsonPropertyName("value")]
        public JsonElement Value { get; set; }
    }
}