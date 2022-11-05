using System;
using System.Numerics;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace EverscaleNet.Client.Models
{
    /// <summary>
    /// <para>Not described yet..</para>
    /// </summary>
    public class ParamsOfVerifySignature
    {
        /// <summary>
        /// <para>Signed data that must be verified encoded in `base64`.</para>
        /// </summary>
        [JsonPropertyName("signed")]
        public string Signed { get; set; }

        /// <summary>
        /// <para>Signer's public key - 64 symbols hex string</para>
        /// </summary>
        [JsonPropertyName("public")]
        public string Public { get; set; }
    }
}