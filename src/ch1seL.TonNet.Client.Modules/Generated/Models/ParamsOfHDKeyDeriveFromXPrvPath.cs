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
    public class ParamsOfHDKeyDeriveFromXPrvPath
    {
        /// <summary>
        ///  Serialized extended private key
        /// </summary>
        [JsonPropertyName("xprv")]
        public string Xprv { get; set; }

        /// <summary>
        ///  Derivation path, for instance "m/44'/396'/0'/0/0"
        /// </summary>
        [JsonPropertyName("path")]
        public string Path { get; set; }
    }
}