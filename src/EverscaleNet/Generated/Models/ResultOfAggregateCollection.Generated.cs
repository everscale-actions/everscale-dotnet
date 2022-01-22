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
    public class ResultOfAggregateCollection
    {
        /// <summary>
        /// <para>Values for requested fields.</para>
        /// <para>Returns an array of strings. Each string refers to the corresponding `fields` item.</para>
        /// <para>Numeric value is returned as a decimal string representations.</para>
        /// </summary>
        [JsonPropertyName("values")]
        public JsonElement? Values { get; set; }
    }
}