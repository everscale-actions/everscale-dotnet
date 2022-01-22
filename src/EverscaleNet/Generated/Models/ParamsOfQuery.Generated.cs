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
    public class ParamsOfQuery
    {
        /// <summary>
        /// GraphQL query text.
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