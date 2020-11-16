using System;
using System.Numerics;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace ch1seL.TonNet.Client.Models
{
    public class ScryptRequest
    {
        [JsonPropertyName("password")]
        public string Password { get; set; }
        [JsonPropertyName("salt")]
        public string Salt { get; set; }
        [JsonPropertyName("log_n")]
        public byte LogN { get; set; }
        [JsonPropertyName("r")]
        public uint R { get; set; }
        [JsonPropertyName("p")]
        public uint P { get; set; }
        [JsonPropertyName("dk_len")]
        public uint DkLen { get; set; }
    }
}