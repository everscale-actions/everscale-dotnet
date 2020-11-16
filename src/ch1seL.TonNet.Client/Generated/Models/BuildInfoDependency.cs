using System;
using System.Numerics;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace ch1seL.TonNet.Client.Models
{
    public class BuildInfoDependency
    {
        [JsonPropertyName("name")]
        public string Name { get; set; }
        [JsonPropertyName("git_commit")]
        public string GitCommit { get; set; }
    }
}