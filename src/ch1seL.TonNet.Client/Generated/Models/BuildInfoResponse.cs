using System;
using System.Numerics;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace ch1seL.TonNet.Client.Models
{
    public class BuildInfoResponse
    {
        [JsonPropertyName("build_number")]
        public uint BuildNumber { get; set; }
        [JsonPropertyName("dependencies")]
        public JsonElement[] Dependencies { get; set; }
    }
}