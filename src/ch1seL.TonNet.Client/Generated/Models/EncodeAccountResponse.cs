using System;
using System.Numerics;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace ch1seL.TonNet.Client.Models
{
    public class EncodeAccountResponse
    {
        [JsonPropertyName("account")]
        public string Account { get; set; }
        [JsonPropertyName("id")]
        public string Id { get; set; }
    }
}