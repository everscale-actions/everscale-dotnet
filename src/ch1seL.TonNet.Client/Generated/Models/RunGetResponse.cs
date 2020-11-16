using System;
using System.Numerics;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace ch1seL.TonNet.Client.Models
{
    public class RunGetResponse
    {
        [JsonPropertyName("output")]
        public JsonElement Output { get; set; }
    }
}