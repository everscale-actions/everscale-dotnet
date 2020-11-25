using Dahomey.Json.Attributes;
using System;
using System.Numerics;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace ch1seL.TonNet.Client.Models
{
    /// <summary>
    /// Not described yet..
    /// </summary>
    public class ClientConfig
    {
        /// <summary>
        /// Not described yet..
        /// </summary>
        [JsonPropertyName("network")]
        public NetworkConfig Network { get; set; }

        /// <summary>
        /// Not described yet..
        /// </summary>
        [JsonPropertyName("crypto")]
        public CryptoConfig Crypto { get; set; }

        /// <summary>
        /// Not described yet..
        /// </summary>
        [JsonPropertyName("abi")]
        public AbiConfig Abi { get; set; }
    }
}