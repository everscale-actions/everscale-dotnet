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
    public class AbiData
    {
        /// <summary>
        /// Not described yet..
        /// </summary>
        [JsonPropertyName("key")]
        public uint Key { get; set; }

        /// <summary>
        /// Not described yet..
        /// </summary>
        [JsonPropertyName("name")]
        public string Name { get; set; }

        /// <summary>
        /// Not described yet..
        /// </summary>
        [JsonPropertyName("type")]
        public string Type { get; set; }

        /// <summary>
        /// Not described yet..
        /// </summary>
        [JsonPropertyName("components")]
        public AbiParam[] Components { get; set; }
    }
}