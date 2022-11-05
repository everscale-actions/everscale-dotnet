using System;
using System.Numerics;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace EverscaleNet.Client.Models
{
    /// <summary>
    /// <para>Not described yet..</para>
    /// </summary>
    public class ParamsOfAttachSignature
    {
        /// <summary>
        /// <para>Contract ABI</para>
        /// </summary>
        [JsonPropertyName("abi")]
        public Abi Abi { get; set; }

        /// <summary>
        /// <para>Public key encoded in `hex`.</para>
        /// </summary>
        [JsonPropertyName("public_key")]
        public string PublicKey { get; set; }

        /// <summary>
        /// <para>Unsigned message BOC encoded in `base64`.</para>
        /// </summary>
        [JsonPropertyName("message")]
        public string Message { get; set; }

        /// <summary>
        /// <para>Signature encoded in `hex`.</para>
        /// </summary>
        [JsonPropertyName("signature")]
        public string Signature { get; set; }
    }
}