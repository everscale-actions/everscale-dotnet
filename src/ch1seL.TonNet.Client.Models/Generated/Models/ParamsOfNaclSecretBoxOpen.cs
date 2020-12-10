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
    public class ParamsOfNaclSecretBoxOpen
    {
        /// <summary>
        /// Encoded with `base64`.
        /// </summary>
        [JsonPropertyName("encrypted")]
        public string Encrypted { get; set; }

        /// <summary>
        /// Not described yet..
        /// </summary>
        [JsonPropertyName("nonce")]
        public string Nonce { get; set; }

        /// <summary>
        /// Not described yet..
        /// </summary>
        [JsonPropertyName("key")]
        public string Key { get; set; }
    }
}