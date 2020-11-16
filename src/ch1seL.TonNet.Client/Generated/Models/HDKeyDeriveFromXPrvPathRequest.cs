using System;
using System.Numerics;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace ch1seL.TonNet.Client.Models
{
    public class HDKeyDeriveFromXPrvPathRequest
    {
        [JsonPropertyName("xprv")]
        public string Xprv { get; set; }
        [JsonPropertyName("path")]
        public string Path { get; set; }
    }
}