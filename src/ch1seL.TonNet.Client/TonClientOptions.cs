using System.Text.Json.Serialization;
using ch1seL.TonNet.Client.Models;

namespace ch1seL.TonNet.Client
{
    public class TonClientOptions
    {
        private const string DefaultServerAddress = "main.ton.dev";

        [JsonPropertyName("network")] public NetworkConfig Network { get; set; } = new NetworkConfig {ServerAddress = DefaultServerAddress};

        [JsonPropertyName("abi")] public AbiConfig Abi { get; set; } = new AbiConfig();

        [JsonPropertyName("crypto")] public CryptoConfig Crypto { get; set; } = new CryptoConfig();
    }
}