using System;
using System.Numerics;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace EverscaleNet.Client.Models
{
    /// <summary>
    /// <para>Not described yet..</para>
    /// </summary>
    public class ResultOfQuery
    {
        /// <summary>
        /// <para>Result provided by DAppServer.</para>
        /// </summary>
        [JsonPropertyName("result")]
        public JsonElement? Result { get; set; }
    }
}