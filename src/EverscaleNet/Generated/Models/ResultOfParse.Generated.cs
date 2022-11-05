using System;
using System.Numerics;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace EverscaleNet.Client.Models
{
    /// <summary>
    /// <para>Not described yet..</para>
    /// </summary>
    public class ResultOfParse
    {
        /// <summary>
        /// <para>JSON containing parsed BOC</para>
        /// </summary>
        [JsonPropertyName("parsed")]
        public JsonElement? Parsed { get; set; }
    }
}