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
        /// <para>Input message BOC.</para>
        /// <para>Must be encoded as base64.</para>
        /// </summary>
        [JsonPropertyName("message")]
        public string Message { get; set; }

        /// <summary>
        /// Account to run on executor
        /// </summary>
        [JsonPropertyName("account")]
        public AccountForExecutor Account { get; set; }

        /// <summary>
        /// Execution options.
        /// </summary>
        [JsonPropertyName("execution_options")]
        public ExecutionOptions ExecutionOptions { get; set; }

        /// <summary>
        /// Contract ABI for decoding output messages
        /// </summary>
        [JsonPropertyName("abi")]
        public Abi Abi { get; set; }

        /// <summary>
        /// Skip transaction check flag
        /// </summary>
        [JsonPropertyName("skip_transaction_check")]
        public bool? SkipTransactionCheck { get; set; }

        /// <summary>
        /// <para>Cache type to put the result.</para>
        /// <para>The BOC itself returned if no cache type provided</para>
        /// </summary>
        [JsonPropertyName("boc_cache")]
        public BocCacheType BocCache { get; set; }

        /// <summary>
        /// <para>Return updated account flag.</para>
        /// <para>Empty string is returned if the flag is `false`</para>
        /// </summary>
        [JsonPropertyName("return_updated_account")]
        public bool? ReturnUpdatedAccount { get; set; }
    }
}