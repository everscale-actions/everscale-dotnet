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
    public class ParamsOfSubscribe
    {
        /// <summary>
        /// GraphQL subscription text.
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