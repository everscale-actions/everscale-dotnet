using System;
using System.Numerics;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace ch1seL.TonNet.Client.Models
{
    public class DeploySet
    {
        [JsonPropertyName("tvc")]
        public string Tvc { get; set; }
        [JsonPropertyName("workchain_id"), JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public int? WorkchainId { get; set; }
        [JsonPropertyName("initial_data")]
        public JsonElement InitialData { get; set; }
    }
}