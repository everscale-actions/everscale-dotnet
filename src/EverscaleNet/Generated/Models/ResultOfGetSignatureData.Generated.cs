using System;
using System.Numerics;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace EverscaleNet.Client.Models
{
    /// <summary>
    /// <para>Not described yet..</para>
    /// </summary>
    public class ResultOfGetSignatureData
    {
        /// <summary>
        /// <para>Signature from the message in `hex`.</para>
        /// </summary>
        [JsonPropertyName("signature")]
        public string Signature { get; set; }

        /// <summary>
        /// <para>Hash to verify the signature in `base64`.</para>
        /// </summary>
        [JsonPropertyName("hash")]
        public string Hash { get; set; }
    }
}