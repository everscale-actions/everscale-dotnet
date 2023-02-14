using System;
using System.Numerics;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace EverscaleNet.Client.Models
{
    /// <summary>
    /// <para>Not described yet..</para>
    /// </summary>
    public class FieldAggregation
    {
        /// <summary>
        /// <para>Dot separated path to the field</para>
        /// </summary>
        [JsonPropertyName("field")]
        public string Field { get; set; }

        /// <summary>
        /// <para>Aggregation function that must be applied to field values</para>
        /// </summary>
        [JsonPropertyName("fn")]
        public AggregationFn? Fn { get; set; }
    }
}