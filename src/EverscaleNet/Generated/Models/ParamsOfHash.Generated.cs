using System;
using System.Numerics;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace EverscaleNet.Client.Models
{
    /// <summary>
    /// <para>Not described yet..</para>
    /// </summary>
    public class ParamsOfHash
    {
        /// <summary>
        /// <para>Input data for hash calculation.</para>
        /// <para>Encoded with `base64`.</para>
        /// </summary>
        [JsonPropertyName("data")]
        public string Data { get; set; }
    }
}