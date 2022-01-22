using Dahomey.Json.Attributes;
using System;
using System.Numerics;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace EverscaleNet.Client.Models
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
        [JsonPropertyName("version")]
        public string Version { get; set; }

        /// <summary>
        /// Not described yet..
        /// </summary>
        [JsonPropertyName("header")]
        public string[] Header { get; set; }

        /// <summary>
        /// Not described yet..
        /// </summary>
        [JsonPropertyName("functions")]
        public AbiFunction[] Functions { get; set; }

        /// <summary>
        /// Not described yet..
        /// </summary>
        [JsonPropertyName("events")]
        public AbiEvent[] Events { get; set; }

        /// <summary>
        /// Not described yet..
        /// </summary>
        [JsonPropertyName("data")]
        public AbiData[] Data { get; set; }

        /// <summary>
        /// Not described yet..
        /// </summary>
        [JsonPropertyName("fields")]
        public AbiParam[] Fields { get; set; }
    }
}