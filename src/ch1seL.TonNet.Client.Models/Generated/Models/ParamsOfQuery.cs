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
    public class ParamsOfQuery
    {
        /// <summary>
        /// Not described yet..
        /// </summary>
        [JsonPropertyName("query")]
        public string Query { get; set; }

        /// <summary>
        /// Must be a map with named values thatcan be used in query.
        /// </summary>
        [JsonPropertyName("variables")]
        public JsonElement? Variables { get; set; }
    }
}