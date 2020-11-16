using System;
using System.Numerics;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace ch1seL.TonNet.Client.Models
{
    public class ClientError
    {
        [JsonPropertyName("code")]
        public uint Code { get; set; }
        [JsonPropertyName("message")]
        public string Message { get; set; }
        [JsonPropertyName("data")]
        public JsonElement Data { get; set; }
    }
}