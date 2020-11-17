using System;
using System.Numerics;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace ch1seL.TonNet.Client.Models
{
    /// <summary>
    /// Not described yet..
    /// </summary>
    public class BuildInfoDependency
    {
        /// <summary>
        ///  Dependency name. Usually it is a crate name.
        /// </summary>
        [JsonPropertyName("name")]
        public string Name { get; set; }

        /// <summary>
        ///  Git commit hash of the related repository.
        /// </summary>
        [JsonPropertyName("git_commit")]
        public string GitCommit { get; set; }
    }
}