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
    public class AbiEvent
    {
        /// <summary>
        /// Not described yet..
        /// </summary>
        [JsonPropertyName("name")]
        public string Name { get; set; }

        /// <summary>
        /// Not described yet..
        /// </summary>
        [JsonPropertyName("inputs")]
        public JsonElement?[] Inputs { get; set; }

        /// <summary>
        /// Not described yet..
        /// </summary>
        [JsonPropertyName("id")]
        public string Id { get; set; }
    }
}