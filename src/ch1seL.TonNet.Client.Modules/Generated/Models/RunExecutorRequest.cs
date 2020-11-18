using Dahomey.Json.Attributes;
using System;
using System.Numerics;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace ch1seL.TonNet.Client.Models
{
    /// <summary>
    /// Not described yet..
    /// </summary>
    public class RunExecutorRequest
    {
        /// <summary>
        ///  Input message BOC. Must be encoded as base64.
        /// </summary>
        [JsonPropertyName("message")]
        public string Message { get; set; }

        /// <summary>
        ///  Account to run on executor
        /// </summary>
        [JsonPropertyName("account")]
        public AccountForExecutor Account { get; set; }

        /// <summary>
        ///  Execution options.
        /// </summary>
        [JsonPropertyName("execution_options")]
        public ExecutionOptions ExecutionOptions { get; set; }

        /// <summary>
        ///  Contract ABI for decoding output messages
        /// </summary>
        [JsonPropertyName("abi")]
        public Abi Abi { get; set; }

        /// <summary>
        ///  Skip transaction check flag
        /// </summary>
        [JsonPropertyName("skip_transaction_check"), JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public bool? SkipTransactionCheck { get; set; }
    }
}