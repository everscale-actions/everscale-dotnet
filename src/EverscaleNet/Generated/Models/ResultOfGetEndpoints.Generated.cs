using System;
using System.Numerics;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace EverscaleNet.Client.Models
{
    /// <summary>
    /// <para>Not described yet..</para>
    /// </summary>
    public class ResultOfGetEndpoints
    {
        /// <summary>
        /// <para>Current query endpoint</para>
        /// </summary>
        [JsonPropertyName("query")]
        public string Query { get; set; }

        /// <summary>
        /// <para>List of all endpoints used by client</para>
        /// </summary>
        [JsonPropertyName("endpoints")]
        public string[] Endpoints { get; set; }
    }
}