using System.Text.Json.Serialization;
using ch1seL.TonNet.Client.Models;

namespace ch1seL.TonNet.Client
{
    public class TonClientOptions
    {
        private static readonly string[] DefaultEndpoints = { "https://main2.ton.dev/", "https://main3.ton.dev/", "https://main4.ton.dev/" };

        [JsonPropertyName("network")] public NetworkConfig Network { get; set; } = new NetworkConfig { Endpoints = DefaultEndpoints };

        [JsonPropertyName("abi")] public AbiConfig Abi { get; set; } = new AbiConfig();

        [JsonPropertyName("crypto")] public CryptoConfig Crypto { get; set; } = new CryptoConfig();
    }
}