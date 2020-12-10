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
    public class ParamsOfHDKeyDeriveFromXPrv
    {
        /// <summary>
        /// Serialized extended private key
        /// </summary>
        [JsonPropertyName("xprv")]
        public string Xprv { get; set; }

        /// <summary>
        /// Child index (see BIP-0032)
        /// </summary>
        [JsonPropertyName("child_index")]
        public uint ChildIndex { get; set; }

        /// <summary>
        /// Indicates the derivation of hardened/not-hardened key (see BIP-0032)
        /// </summary>
        [JsonPropertyName("hardened")]
        public bool Hardened { get; set; }
    }
}