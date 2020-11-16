using System;
using System.Numerics;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace ch1seL.TonNet.Client.Models
{
    public class RunGetRequest
    {
        [JsonPropertyName("account")]
        public string Account { get; set; }
        [JsonPropertyName("function_name")]
        public string FunctionName { get; set; }
        [JsonPropertyName("input")]
        public JsonElement Input { get; set; }
        [JsonPropertyName("execution_options")]
        public ExecutionOptions ExecutionOptions { get; set; }
    }
}