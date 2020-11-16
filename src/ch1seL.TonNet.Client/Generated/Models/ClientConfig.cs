using System;
using System.Numerics;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace ch1seL.TonNet.Client.Models
{
    public class ClientConfig
    {
        [JsonPropertyName("network")]
        public NetworkConfig Network { get; set; }
        [JsonPropertyName("crypto")]
        public CryptoConfig Crypto { get; set; }
        [JsonPropertyName("abi")]
        public AbiConfig Abi { get; set; }
    }
}