using System;
using System.Numerics;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace ch1seL.TonNet.Client.Models
{
    /// <summary>
    /// Not described yet..
    /// </summary>
    public class SignRequest
    {
        /// <summary>
        ///  Data that must be signed encoded in `base64`.
        /// </summary>
        [JsonPropertyName("unsigned")]
        public string Unsigned { get; set; }

        /// <summary>
        ///  Sign keys.
        /// </summary>
        [JsonPropertyName("keys")]
        public KeyPair Keys { get; set; }
    }
}