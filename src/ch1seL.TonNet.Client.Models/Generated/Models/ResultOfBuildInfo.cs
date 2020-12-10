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
    public class ResultOfBuildInfo
    {
        /// <summary>
        /// Not described yet..
        /// </summary>
        [JsonPropertyName("build_number")]
        public uint BuildNumber { get; set; }

        /// <summary>
        /// Not described yet..
        /// </summary>
        [JsonPropertyName("dependencies")]
        public JsonElement?[] Dependencies { get; set; }
    }
}