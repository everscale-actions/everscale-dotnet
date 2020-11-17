using System;
using System.Numerics;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace ch1seL.TonNet.Client.Models
{
    /// <summary>
    /// Not described yet..
    /// </summary>
    public class QueryCollectionRequest
    {
        /// <summary>
        ///  Collection name (accounts, blocks, transactions, messages, block_signatures)
        /// </summary>
        [JsonPropertyName("collection")]
        public string Collection { get; set; }

        /// <summary>
        ///  Collection filter
        /// </summary>
        [JsonPropertyName("filter")]
        public JsonElement Filter { get; set; }

        /// <summary>
        ///  Projection (result) string
        /// </summary>
        [JsonPropertyName("result")]
        public string Result { get; set; }

        /// <summary>
        ///  Sorting order
        /// </summary>
        [JsonPropertyName("order"), JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public JsonElement[] Order { get; set; }

        /// <summary>
        ///  Number of documents to return
        /// </summary>
        [JsonPropertyName("limit"), JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public uint? Limit { get; set; }
    }
}