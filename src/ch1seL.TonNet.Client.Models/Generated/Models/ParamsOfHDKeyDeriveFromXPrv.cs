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
        /// Not described yet..
        /// </summary>
        [JsonPropertyName("xprv")]
        public string Xprv { get; set; }

        /// <summary>
        /// Not described yet..
        /// </summary>
        [JsonPropertyName("child_index")]
        public uint ChildIndex { get; set; }

        /// <summary>
        /// Not described yet..
        /// </summary>
        [JsonPropertyName("hardened")]
        public bool Hardened { get; set; }
    }
}