using System;
using System.Numerics;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace ch1seL.TonNet.Client.Models
{
    public class AbiContract
    {
        [JsonPropertyName("ABI version"), JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public uint? ABIVersion { get; set; }
        [JsonPropertyName("abi_version"), JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public uint? AbiVersion { get; set; }
        [JsonPropertyName("header"), JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public string[] Header { get; set; }
        [JsonPropertyName("functions"), JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public JsonElement[] Functions { get; set; }
        [JsonPropertyName("events"), JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public JsonElement[] Events { get; set; }
        [JsonPropertyName("data"), JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public JsonElement[] Data { get; set; }
    }
}