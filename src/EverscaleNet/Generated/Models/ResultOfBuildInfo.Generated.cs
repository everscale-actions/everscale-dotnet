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
    public class ResultOfBuildInfo
    {
        /// <summary>
        /// Build number assigned to this build by the CI.
        /// </summary>
        [JsonPropertyName("build_number")]
        public uint BuildNumber { get; set; }

        /// <summary>
        /// Fingerprint of the most important dependencies.
        /// </summary>
        [JsonPropertyName("dependencies")]
        public BuildInfoDependency[] Dependencies { get; set; }
    }
}