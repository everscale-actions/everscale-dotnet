using System;
using System.Numerics;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace EverscaleNet.Client.Models
{
    /// <summary>
    /// <para>Not described yet..</para>
    /// </summary>
    public class ResultOfQueryCollection
    {
        /// <summary>
        /// <para>Objects that match the provided criteria</para>
        /// </summary>
        [JsonPropertyName("result")]
        public JsonElement[] Result { get; set; }
    }
}