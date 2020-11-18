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
    public class ParamsOfAttachSignature
    {
        /// <summary>
        ///  Contract ABI
        /// </summary>
        [JsonPropertyName("abi")]
        public Abi Abi { get; set; }

        /// <summary>
        ///  Public key encoded in `hex`.
        /// </summary>
        [JsonPropertyName("public_key")]
        public string PublicKey { get; set; }

        /// <summary>
        ///  Unsigned message BOC encoded in `base64`.
        /// </summary>
        [JsonPropertyName("message")]
        public string Message { get; set; }

        /// <summary>
        ///  Signature encoded in `hex`.
        /// </summary>
        [JsonPropertyName("signature")]
        public string Signature { get; set; }
    }
}