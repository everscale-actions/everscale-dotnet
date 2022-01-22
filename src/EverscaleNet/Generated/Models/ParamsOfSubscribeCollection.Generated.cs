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
    public class ParamsOfSubscribeCollection
    {
        /// <summary>
        /// Collection name (accounts, blocks, transactions, messages, block_signatures)
        /// </summary>
        [JsonPropertyName("collection")]
        public string Collection { get; set; }

        /// <summary>
        /// Collection filter
        /// </summary>
        [JsonPropertyName("filter")]
        public JsonElement? Filter { get; set; }

        /// <summary>
        /// Projection (result) string
        /// </summary>
        [JsonPropertyName("result")]
        public string Result { get; set; }
    }
}