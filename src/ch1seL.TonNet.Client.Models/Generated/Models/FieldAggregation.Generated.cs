using Dahomey.Json.Attributes;
using System;
using System.Numerics;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace ch1seL.TonNet.Client.Models
{
    /// <summary>
    /// Not described yet..
    /// </summary>
    public class FieldAggregation
    {
        /// <summary>
        /// Dot separated path to the field
        /// </summary>
        [JsonPropertyName("field")]
        public string Field { get; set; }

        /// <summary>
        /// Aggregation function that must be applied to field values
        /// </summary>
        [JsonPropertyName("fn")]
        public AggregationFn Fn { get; set; }
    }
}