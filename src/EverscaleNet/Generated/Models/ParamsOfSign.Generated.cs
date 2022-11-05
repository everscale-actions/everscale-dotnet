using System;
using System.Numerics;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace EverscaleNet.Client.Models
{
    /// <summary>
    /// <para>Not described yet..</para>
    /// </summary>
    public class ParamsOfSign
    {
        /// <summary>
        /// <para>Data that must be signed encoded in `base64`.</para>
        /// </summary>
        [JsonPropertyName("unsigned")]
        public string Unsigned { get; set; }

        /// <summary>
        /// <para>Sign keys.</para>
        /// </summary>
        [JsonPropertyName("keys")]
        public KeyPair Keys { get; set; }
    }
}