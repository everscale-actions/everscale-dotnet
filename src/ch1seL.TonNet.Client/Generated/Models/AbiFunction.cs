using System;
using System.Numerics;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace ch1seL.TonNet.Client.Models
{
    /// <summary>
    /// Not described yet..
    /// </summary>
    public class AbiFunction
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
        public JsonElement[] Inputs { get; set; }

        /// <summary>
        /// Not described yet..
        /// </summary>
        [JsonPropertyName("outputs")]
        public JsonElement[] Outputs { get; set; }

        /// <summary>
        /// Not described yet..
        /// </summary>
        [JsonPropertyName("id"), JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public uint? Id { get; set; }
    }
}