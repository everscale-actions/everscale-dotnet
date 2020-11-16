using System;
using System.Numerics;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace ch1seL.TonNet.Client.Models
{
    public class RunExecutorRequest
    {
        [JsonPropertyName("message")]
        public string Message { get; set; }
        [JsonPropertyName("account")]
        public AccountForExecutor Account { get; set; }
        [JsonPropertyName("execution_options")]
        public ExecutionOptions ExecutionOptions { get; set; }
        [JsonPropertyName("abi")]
        public Abi Abi { get; set; }
        [JsonPropertyName("skip_transaction_check"), JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public bool? SkipTransactionCheck { get; set; }
    }
}