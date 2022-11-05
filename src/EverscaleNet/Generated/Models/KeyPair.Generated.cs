using System;
using System.Numerics;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace EverscaleNet.Client.Models
{
    /// <summary>
    /// <para>Not described yet..</para>
    /// </summary>
    public class KeyPair
    {
        /// <summary>
        /// <para>Public key - 64 symbols hex string</para>
        /// </summary>
        [JsonPropertyName("public")]
        public string Public { get; set; }

        /// <summary>
        /// <para>Private key - u64 symbols hex string</para>
        /// </summary>
        [JsonPropertyName("secret")]
        public string Secret { get; set; }
    }
}