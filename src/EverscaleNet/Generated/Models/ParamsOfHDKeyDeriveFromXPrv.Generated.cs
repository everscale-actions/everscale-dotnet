using System;
using System.Numerics;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace EverscaleNet.Client.Models
{
    /// <summary>
    /// <para>Not described yet..</para>
    /// </summary>
    public class ParamsOfHDKeyDeriveFromXPrv
    {
        /// <summary>
        /// <para>Serialized extended private key</para>
        /// </summary>
        [JsonPropertyName("xprv")]
        public string Xprv { get; set; }

        /// <summary>
        /// <para>Child index (see BIP-0032)</para>
        /// </summary>
        [JsonPropertyName("child_index")]
        public uint ChildIndex { get; set; }

        /// <summary>
        /// <para>Indicates the derivation of hardened/not-hardened key (see BIP-0032)</para>
        /// </summary>
        [JsonPropertyName("hardened")]
        public bool Hardened { get; set; }
    }
}