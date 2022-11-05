using System;
using System.Numerics;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace EverscaleNet.Client.Models
{
    /// <summary>
    /// <para>Not described yet..</para>
    /// </summary>
    public class ResultOfDecodeAccountData
    {
        /// <summary>
        /// <para>Decoded data as a JSON structure.</para>
        /// </summary>
        [JsonPropertyName("data")]
        public JsonElement? Data { get; set; }
    }
}