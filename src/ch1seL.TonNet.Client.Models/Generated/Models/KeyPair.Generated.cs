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
    public class KeyPair
    {
        /// <summary>
        /// Public key - 64 symbols hex string
        /// </summary>
        [JsonPropertyName("public")]
        public string Public { get; set; }

        /// <summary>
        /// Private key - u64 symbols hex string
        /// </summary>
        [JsonPropertyName("secret")]
        public string Secret { get; set; }
    }
}