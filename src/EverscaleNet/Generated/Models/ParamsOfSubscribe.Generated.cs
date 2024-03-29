using System;
using System.Numerics;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace EverscaleNet.Client.Models
{
    /// <summary>
    /// <para>Not described yet..</para>
    /// </summary>
    public class ParamsOfSubscribe
    {
        /// <summary>
        /// <para>GraphQL subscription text.</para>
        /// </summary>
        [JsonPropertyName("subscription")]
        public string Subscription { get; set; }

        /// <summary>
        /// <para>Variables used in subscription.</para>
        /// <para>Must be a map with named values that can be used in query.</para>
        /// </summary>
        [JsonPropertyName("variables")]
        public JsonElement? Variables { get; set; }
    }
}