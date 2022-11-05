using System;
using System.Numerics;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace EverscaleNet.Client.Models
{
    /// <summary>
    /// <para>Not described yet..</para>
    /// </summary>
    public class ResultOfBatchQuery
    {
        /// <summary>
        /// <para>Result values for batched queries.</para>
        /// <para>Returns an array of values. Each value corresponds to `queries` item.</para>
        /// </summary>
        [JsonPropertyName("results")]
        public JsonElement[] Results { get; set; }
    }
}