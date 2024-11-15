using System;
using System.Numerics;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace EverscaleNet.Client.Models
{
    /// <summary>
    /// <para>Not described yet..</para>
    /// </summary>
    public class ParamsOfQueryCollection
    {
        /// <summary>
        /// <para>Collection name (accounts, blocks, transactions, messages, block_signatures)</para>
        /// </summary>
        [JsonPropertyName("collection")]
        public string Collection { get; set; }

        /// <summary>
        /// <para>Collection filter</para>
        /// </summary>
        [JsonPropertyName("filter")]
        public JsonElement? Filter { get; set; }

        /// <summary>
        /// <para>Projection (result) string</para>
        /// </summary>
        [JsonPropertyName("result")]
        public string Result { get; set; }

        /// <summary>
        /// <para>Sorting order</para>
        /// </summary>
        [JsonPropertyName("order")]
        public OrderBy[] Order { get; set; }

        /// <summary>
        /// <para>Number of documents to return</para>
        /// </summary>
        [JsonPropertyName("limit")]
        public uint? Limit { get; set; }
    }
}