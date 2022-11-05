using System;
using System.Numerics;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace EverscaleNet.Client.Models
{
    /// <summary>
    /// <para>Not described yet..</para>
    /// </summary>
    public class ParamsOfQuery
    {
        /// <summary>
        /// <para>GraphQL query text.</para>
        /// </summary>
        [JsonPropertyName("query")]
        public string Query { get; set; }

        /// <summary>
        /// <para>Variables used in query.</para>
        /// <para>Must be a map with named values that can be used in query.</para>
        /// </summary>
        [JsonPropertyName("variables")]
        public JsonElement? Variables { get; set; }
    }
}