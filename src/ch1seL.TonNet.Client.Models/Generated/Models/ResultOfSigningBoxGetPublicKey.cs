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