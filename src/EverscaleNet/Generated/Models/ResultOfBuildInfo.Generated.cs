using System;
using System.Numerics;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace EverscaleNet.Client.Models
{
    /// <summary>
    /// <para>Not described yet..</para>
    /// </summary>
    public class ResultOfBuildInfo
    {
        /// <summary>
        /// <para>Build number assigned to this build by the CI.</para>
        /// </summary>
        [JsonPropertyName("build_number")]
        public uint BuildNumber { get; set; }

        /// <summary>
        /// <para>Fingerprint of the most important dependencies.</para>
        /// </summary>
        [JsonPropertyName("dependencies")]
        public BuildInfoDependency[] Dependencies { get; set; }
    }
}