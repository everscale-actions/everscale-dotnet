using System;
using System.Numerics;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace ch1seL.TonNet.Client.Models
{
    public class HDKeyDeriveFromXPrvRequest
    {
        [JsonPropertyName("xprv")]
        public string Xprv { get; set; }
        [JsonPropertyName("child_index")]
        public uint ChildIndex { get; set; }
        [JsonPropertyName("hardened")]
        public bool Hardened { get; set; }
    }
}