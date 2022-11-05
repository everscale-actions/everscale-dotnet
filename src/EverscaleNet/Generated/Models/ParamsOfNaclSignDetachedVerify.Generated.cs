using System;
using System.Numerics;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace EverscaleNet.Client.Models
{
    /// <summary>
    /// <para>Not described yet..</para>
    /// </summary>
    public class ParamsOfNaclSignDetachedVerify
    {
        /// <summary>
        /// <para>Unsigned data that must be verified.</para>
        /// <para>Encoded with `base64`.</para>
        /// </summary>
        [JsonPropertyName("unsigned")]
        public string Unsigned { get; set; }

        /// <summary>
        /// <para>Signature that must be verified.</para>
        /// <para>Encoded with `hex`.</para>
        /// </summary>
        [JsonPropertyName("signature")]
        public string Signature { get; set; }

        /// <summary>
        /// <para>Signer's public key - unprefixed 0-padded to 64 symbols hex string.</para>
        /// </summary>
        [JsonPropertyName("public")]
        public string Public { get; set; }
    }
}