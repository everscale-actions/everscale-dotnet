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
    public class ParamsOfVerifySignature
    {
        /// <summary>
        /// Signed data that must be verified encoded in `base64`.
        /// </summary>
        [JsonPropertyName("signed")]
        public string Signed { get; set; }

        /// <summary>
        /// Signer's public key - 64 symbols hex string
        /// </summary>
        [JsonPropertyName("public")]
        public string Public { get; set; }
    }
}