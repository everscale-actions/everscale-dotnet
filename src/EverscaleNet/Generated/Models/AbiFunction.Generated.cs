using Dahomey.Json.Attributes;
using System;
using System.Numerics;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace EverscaleNet.Client.Models
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
        public AbiParam[] Inputs { get; set; }

        /// <summary>
        /// Not described yet..
        /// </summary>
        [JsonPropertyName("outputs")]
        public AbiParam[] Outputs { get; set; }

        /// <summary>
        /// Not described yet..
        /// </summary>
        [JsonPropertyName("id")]
        public string Id { get; set; }
    }
}