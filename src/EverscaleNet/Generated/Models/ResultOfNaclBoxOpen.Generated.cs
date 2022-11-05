using System;
using System.Numerics;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace EverscaleNet.Client.Models
{
    /// <summary>
    /// <para>Not described yet..</para>
    /// </summary>
    public class ResultOfNaclBoxOpen
    {
        /// <summary>
        /// <para>Decrypted data encoded in `base64`.</para>
        /// </summary>
        [JsonPropertyName("decrypted")]
        public string Decrypted { get; set; }
    }
}