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
        [JsonPropertyName("ABI version"), JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public uint? ABIVersion { get; set; }

        /// <summary>
        /// Not described yet..
        /// </summary>
        [JsonPropertyName("abi_version"), JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public uint? AbiVersion { get; set; }

        /// <summary>
        /// Not described yet..
        /// </summary>
        [JsonPropertyName("header"), JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public string[] Header { get; set; }

        /// <summary>
        /// Not described yet..
        /// </summary>
        [JsonPropertyName("functions"), JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public JsonElement[] Functions { get; set; }

        /// <summary>
        /// Not described yet..
        /// </summary>
        [JsonPropertyName("events"), JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public JsonElement[] Events { get; set; }

        /// <summary>
        /// Not described yet..
        /// </summary>
        [JsonPropertyName("data"), JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public JsonElement[] Data { get; set; }
    }
}