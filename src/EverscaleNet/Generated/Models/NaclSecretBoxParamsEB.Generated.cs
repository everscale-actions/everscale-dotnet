using Dahomey.Json.Attributes;
using System;
using System.Numerics;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace EverscaleNet.Client.Models
{
    /// <summary>
    /// Not described yet..
    /// </summary>
    public class NaclSecretBoxParamsEB
    {
        /// <summary>
        /// Secret key - unprefixed 0-padded to 64 symbols hex string
        /// </summary>
        [JsonPropertyName("key")]
        public string Key { get; set; }

        /// <summary>
        /// Nonce in `hex`
        /// </summary>
        [JsonPropertyName("nonce")]
        public string Nonce { get; set; }
    }
}