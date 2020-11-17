using System;
using System.Numerics;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace ch1seL.TonNet.Client.Models
{
    /// <summary>
    /// Not described yet..
    /// </summary>
    public class NaclBoxOpenResponse
    {
        /// <summary>
        ///  Decrypted data encoded in `base64`.
        /// </summary>
        [JsonPropertyName("decrypted")]
        public string Decrypted { get; set; }
    }
}