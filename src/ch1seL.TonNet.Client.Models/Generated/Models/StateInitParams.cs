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
    public class StateInitParams
    {
        /// <summary>
        /// Not described yet..
        /// </summary>
        [JsonPropertyName("abi")]
        public Abi Abi { get; set; }

        /// <summary>
        /// Not described yet..
        /// </summary>
        [JsonPropertyName("value")]
        public JsonElement? Value { get; set; }
    }
}