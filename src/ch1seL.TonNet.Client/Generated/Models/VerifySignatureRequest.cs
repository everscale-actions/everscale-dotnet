using System;
using System.Numerics;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace ch1seL.TonNet.Client.Models
{
    public class VerifySignatureRequest
    {
        [JsonPropertyName("signed")]
        public string Signed { get; set; }
        [JsonPropertyName("public")]
        public string Public { get; set; }
    }
}