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
    public class ResultOfGetEndpoints
    {
        /// <summary>
        /// Current query endpoint
        /// </summary>
        [JsonPropertyName("query")]
        public string Query { get; set; }

        /// <summary>
        /// List of all endpoints used by client
        /// </summary>
        [JsonPropertyName("endpoints")]
        public string[] Endpoints { get; set; }
    }
}