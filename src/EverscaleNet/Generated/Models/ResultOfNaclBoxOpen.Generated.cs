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
    public class ResultOfNaclBoxOpen
    {
        /// <summary>
        /// Decrypted data encoded in `base64`.
        /// </summary>
        [JsonPropertyName("decrypted")]
        public string Decrypted { get; set; }
    }
}