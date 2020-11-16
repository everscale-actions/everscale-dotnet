using System;
using System.Numerics;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace ch1seL.TonNet.Client.Models
{
    public class RunTvmRequest
    {
        [JsonPropertyName("message")]
        public string Message { get; set; }
        [JsonPropertyName("account")]
        public string Account { get; set; }
        [JsonPropertyName("execution_options")]
        public ExecutionOptions ExecutionOptions { get; set; }
        [JsonPropertyName("abi")]
        public Abi Abi { get; set; }
    }
}