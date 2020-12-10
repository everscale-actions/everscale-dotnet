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
    public class ParamsOfQueryCollection
    {
        /// <summary>
        /// Not described yet..
        /// </summary>
        [JsonPropertyName("collection")]
        public string Collection { get; set; }

        /// <summary>
        /// Not described yet..
        /// </summary>
        [JsonPropertyName("filter")]
        public JsonElement? Filter { get; set; }

        /// <summary>
        /// Not described yet..
        /// </summary>
        [JsonPropertyName("result")]
        public string Result { get; set; }

        /// <summary>
        /// Not described yet..
        /// </summary>
        [JsonPropertyName("order")]
        public JsonElement?[] Order { get; set; }

        /// <summary>
        /// Not described yet..
        /// </summary>
        [JsonPropertyName("limit")]
        public uint? Limit { get; set; }
    }
}