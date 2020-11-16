using System;
using System.Numerics;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace ch1seL.TonNet.Client.Models
{
    public class WaitForCollectionRequest
    {
        [JsonPropertyName("collection")]
        public string Collection { get; set; }
        [JsonPropertyName("filter")]
        public JsonElement Filter { get; set; }
        [JsonPropertyName("result")]
        public string Result { get; set; }
        [JsonPropertyName("timeout"), JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public uint? Timeout { get; set; }
    }
}