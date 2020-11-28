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
    public class ParamsOfAttachSignatureToMessageBody
    {
        /// <summary>
        ///  Contract ABI
        /// </summary>
        [JsonPropertyName("abi")]
        public Abi Abi { get; set; }

        /// <summary>
        ///  Public key. Must be encoded with `hex`.
        /// </summary>
        [JsonPropertyName("public_key")]
        public string PublicKey { get; set; }

        /// <summary>
        ///  Unsigned message body BOC. Must be encoded with `base64`.
        /// </summary>
        [JsonPropertyName("message")]
        public string Message { get; set; }

        /// <summary>
        ///  Signature. Must be encoded with `hex`.
        /// </summary>
        [JsonPropertyName("signature")]
        public string Signature { get; set; }
    }
}