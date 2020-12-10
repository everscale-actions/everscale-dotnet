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
    public class ParamsOfRunTvm
    {
        /// <summary>
        /// <para>Input message BOC.</para>
        /// <para>Must be encoded as base64.</para>
        /// </summary>
        [JsonPropertyName("message")]
        public string Message { get; set; }

        /// <summary>
        /// <para>Account BOC.</para>
        /// <para>Must be encoded as base64.</para>
        /// </summary>
        [JsonPropertyName("account")]
        public string Account { get; set; }

        /// <summary>
        /// Execution options.
        /// </summary>
        [JsonPropertyName("execution_options")]
        public ExecutionOptions ExecutionOptions { get; set; }

        /// <summary>
        /// Contract ABI for dedcoding output messages
        /// </summary>
        [JsonPropertyName("abi")]
        public Abi Abi { get; set; }
    }
}