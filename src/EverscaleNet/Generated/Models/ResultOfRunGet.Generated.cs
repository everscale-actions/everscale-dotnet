using System;
using System.Numerics;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace EverscaleNet.Client.Models
{
    /// <summary>
    /// <para>Not described yet..</para>
    /// </summary>
    public class ResultOfRunGet
    {
        /// <summary>
        /// <para>Values returned by get-method on stack</para>
        /// </summary>
        [JsonPropertyName("output")]
        public JsonElement? Output { get; set; }
    }
}