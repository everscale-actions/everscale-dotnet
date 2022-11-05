using System;
using System.Numerics;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace EverscaleNet.Client.Models
{
    /// <summary>
    /// <para>Not described yet..</para>
    /// </summary>
    public class ResultOfNaclBox
    {
        /// <summary>
        /// <para>Encrypted data encoded in `base64`.</para>
        /// </summary>
        [JsonPropertyName("encrypted")]
        public string Encrypted { get; set; }
    }
}