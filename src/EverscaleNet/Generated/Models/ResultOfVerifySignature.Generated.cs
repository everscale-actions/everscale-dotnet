using System;
using System.Numerics;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace EverscaleNet.Client.Models
{
    /// <summary>
    /// <para>Not described yet..</para>
    /// </summary>
    public class ResultOfVerifySignature
    {
        /// <summary>
        /// <para>Unsigned data encoded in `base64`.</para>
        /// </summary>
        [JsonPropertyName("unsigned")]
        public string Unsigned { get; set; }
    }
}