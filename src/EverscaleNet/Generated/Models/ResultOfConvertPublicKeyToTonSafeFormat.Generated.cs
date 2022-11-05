using System;
using System.Numerics;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace EverscaleNet.Client.Models
{
    /// <summary>
    /// <para>Not described yet..</para>
    /// </summary>
    public class ResultOfConvertPublicKeyToTonSafeFormat
    {
        /// <summary>
        /// <para>Public key represented in TON safe format.</para>
        /// </summary>
        [JsonPropertyName("ton_public_key")]
        public string TonPublicKey { get; set; }
    }
}