using System;
using System.Numerics;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace EverscaleNet.Client.Models
{
    /// <summary>
    /// <para>Not described yet..</para>
    /// </summary>
    public class ResultOfSigningBoxGetPublicKey
    {
        /// <summary>
        /// <para>Public key of signing box.</para>
        /// <para>Encoded with hex</para>
        /// </summary>
        [JsonPropertyName("pubkey")]
        public string Pubkey { get; set; }
    }
}