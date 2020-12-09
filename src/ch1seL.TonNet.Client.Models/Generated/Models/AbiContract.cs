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
    public class AbiContract
    {
        /// <summary>
        /// Not described yet..
        /// </summary>
        [JsonPropertyName("ABI version")]
        public uint? ABIVersion { get; set; }

        /// <summary>
        /// Not described yet..
        /// </summary>
        [JsonPropertyName("abi_version")]
        public uint? AbiVersion { get; set; }

        /// <summary>
        /// Not described yet..
        /// </summary>
        [JsonPropertyName("header")]
        public string[] Header { get; set; }

        /// <summary>
        /// Not described yet..
        /// </summary>
        [JsonPropertyName("functions")]
        public JsonElement?[] Functions { get; set; }

        /// <summary>
        /// Not described yet..
        /// </summary>
        [JsonPropertyName("events")]
        public JsonElement?[] Events { get; set; }

        /// <summary>
        /// Not described yet..
        /// </summary>
        [JsonPropertyName("data")]
        public JsonElement?[] Data { get; set; }
    }
}