using System;
using System.Numerics;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace EverscaleNet.Client.Models
{
    /// <summary>
    /// <para>Not described yet..</para>
    /// </summary>
    public class BuildInfoDependency
    {
        /// <summary>
        /// <para>Dependency name.</para>
        /// <para>Usually it is a crate name.</para>
        /// </summary>
        [JsonPropertyName("name")]
        public string Name { get; set; }

        /// <summary>
        /// <para>Git commit hash of the related repository.</para>
        /// </summary>
        [JsonPropertyName("git_commit")]
        public string GitCommit { get; set; }
    }
}