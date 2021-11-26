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
    public class ParamsOfDecodeAccountData
    {
        /// <summary>
        /// Contract ABI
        /// </summary>
        [JsonPropertyName("abi")]
        public Abi Abi { get; set; }

        /// <summary>
        /// Data BOC or BOC handle
        /// </summary>
        [JsonPropertyName("data")]
        public string Data { get; set; }
    }
}