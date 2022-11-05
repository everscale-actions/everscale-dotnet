using System;
using System.Numerics;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace EverscaleNet.Client.Models
{
    /// <summary>
    /// <para>Not described yet..</para>
    /// </summary>
    public class ParamsOfHDKeyDeriveFromXPrvPath
    {
        /// <summary>
        /// <para>Serialized extended private key</para>
        /// </summary>
        [JsonPropertyName("xprv")]
        public string Xprv { get; set; }

        /// <summary>
        /// <para>Derivation path, for instance "m/44'/396'/0'/0/0"</para>
        /// </summary>
        [JsonPropertyName("path")]
        public string Path { get; set; }
    }
}