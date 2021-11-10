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

        /// <summary>
        /// Not described yet..
        /// </summary>
        [JsonPropertyName("boc")]
        public BocConfig Boc { get; set; }

        /// <summary>
        /// Not described yet..
        /// </summary>
        [JsonPropertyName("proofs")]
        public ProofsConfig Proofs { get; set; }

        /// <summary>
        /// For file based storage is a folder name where SDK will store its data. For browser based is a browser async storage key prefix. Default (recommended) value is "~/.tonclient" for native environments and ".tonclient" for web-browser.
        /// </summary>
        [JsonPropertyName("local_storage_path")]
        public string LocalStoragePath { get; set; }
    }
}