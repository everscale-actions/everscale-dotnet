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
    public class ParamsOfDecodeMessage
    {
        /// <summary>
        /// contract ABI
        /// </summary>
        [JsonPropertyName("abi")]
        public Abi Abi { get; set; }

        /// <summary>
        /// Message BOC
        /// </summary>
        [JsonPropertyName("message")]
        public string Message { get; set; }
    }
}