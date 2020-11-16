using System;
using System.Numerics;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace ch1seL.TonNet.Client.Models
{
    public class QueryCollectionRequest
    {
        [JsonPropertyName("collection")]
        public string Collection { get; set; }
        [JsonPropertyName("filter")]
        public JsonElement Filter { get; set; }
        [JsonPropertyName("result")]
        public string Result { get; set; }
        [JsonPropertyName("order"), JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public JsonElement[] Order { get; set; }
        [JsonPropertyName("limit"), JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public uint? Limit { get; set; }
    }
}