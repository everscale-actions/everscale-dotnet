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
        ///  Input message BOC. Must be encoded as base64.
        /// </summary>
        [JsonPropertyName("message")]
        public string Message { get; set; }

        /// <summary>
        ///  Account BOC. Must be encoded as base64.
        /// </summary>
        [JsonPropertyName("account")]
        public string Account { get; set; }

        /// <summary>
        ///  Execution options.
        /// </summary>
        [JsonPropertyName("execution_options")]
        public ExecutionOptions ExecutionOptions { get; set; }

        /// <summary>
        ///  Contract ABI for dedcoding output messages
        /// </summary>
        [JsonPropertyName("abi")]
        public Abi Abi { get; set; }
    }
}