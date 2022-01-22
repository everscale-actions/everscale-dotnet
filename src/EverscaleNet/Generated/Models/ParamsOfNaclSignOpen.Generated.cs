using Dahomey.Json.Attributes;
using System;
using System.Numerics;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace EverscaleNet.Client.Models
{
    /// <summary>
    /// Not described yet..
    /// </summary>
    public class ParamsOfNaclSignOpen
    {
        /// <summary>
        /// <para>Signed data that must be unsigned.</para>
        /// <para>Encoded with `base64`.</para>
        /// </summary>
        [JsonPropertyName("signed")]
        public string Signed { get; set; }

        /// <summary>
        /// Signer's public key - unprefixed 0-padded to 64 symbols hex string
        /// </summary>
        [JsonPropertyName("public")]
        public string Public { get; set; }
    }
}