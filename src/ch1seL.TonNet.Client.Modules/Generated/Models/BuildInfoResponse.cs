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
    public class BuildInfoResponse
    {
        /// <summary>
        ///  Build number assigned to this build by the CI.
        /// </summary>
        [JsonPropertyName("build_number")]
        public uint BuildNumber { get; set; }

        /// <summary>
        ///  Fingerprint of the most important dependencies.
        /// </summary>
        [JsonPropertyName("dependencies")]
        public JsonElement?[] Dependencies { get; set; }
    }
}