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
    public class ParamsOfRunExecutor
    {
        /// <summary>
        /// Must be encoded as base64.
        /// </summary>
        [JsonPropertyName("message")]
        public string Message { get; set; }

        /// <summary>
        /// Not described yet..
        /// </summary>
        [JsonPropertyName("account")]
        public AccountForExecutor Account { get; set; }

        /// <summary>
        /// Not described yet..
        /// </summary>
        [JsonPropertyName("execution_options")]
        public ExecutionOptions ExecutionOptions { get; set; }

        /// <summary>
        /// Not described yet..
        /// </summary>
        [JsonPropertyName("abi")]
        public Abi Abi { get; set; }

        /// <summary>
        /// Not described yet..
        /// </summary>
        [JsonPropertyName("skip_transaction_check")]
        public bool? SkipTransactionCheck { get; set; }
    }
}