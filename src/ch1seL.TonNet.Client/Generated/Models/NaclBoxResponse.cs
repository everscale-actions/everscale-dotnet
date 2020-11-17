using System;
using System.Numerics;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace ch1seL.TonNet.Client.Models
{
    /// <summary>
    /// Not described yet..
    /// </summary>
    public class NaclBoxResponse
    {
        /// <summary>
        ///  Encrypted data encoded in `base64`.
        /// </summary>
        [JsonPropertyName("encrypted")]
        public string Encrypted { get; set; }
    }
}