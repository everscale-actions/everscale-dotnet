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
    public class ParamsOfBatchQuery
    {
        /// <summary>
        /// List of query operations that must be performed per single fetch.
        /// </summary>
        [JsonPropertyName("operations")]
        public JsonElement?[] Operations { get; set; }
    }
}