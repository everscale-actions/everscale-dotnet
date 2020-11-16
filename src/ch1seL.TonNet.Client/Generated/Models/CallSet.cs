using System;
using System.Numerics;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace ch1seL.TonNet.Client.Models
{
    public class CallSet
    {
        [JsonPropertyName("function_name")]
        public string FunctionName { get; set; }
        [JsonPropertyName("header")]
        public FunctionHeader Header { get; set; }
        [JsonPropertyName("input")]
        public JsonElement Input { get; set; }
    }
}