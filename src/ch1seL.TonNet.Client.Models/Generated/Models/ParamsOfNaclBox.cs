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
    public class ParamsOfNaclBox
    {
        /// <summary>
        /// Not described yet..
        /// </summary>
        [JsonPropertyName("decrypted")]
        public string Decrypted { get; set; }

        /// <summary>
        /// Not described yet..
        /// </summary>
        [JsonPropertyName("nonce")]
        public string Nonce { get; set; }

        /// <summary>
        /// Not described yet..
        /// </summary>
        [JsonPropertyName("their_public")]
        public string TheirPublic { get; set; }

        /// <summary>
        /// Not described yet..
        /// </summary>
        [JsonPropertyName("secret")]
        public string Secret { get; set; }
    }
}