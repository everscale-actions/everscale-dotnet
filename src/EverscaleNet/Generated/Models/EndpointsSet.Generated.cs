using System;
using System.Numerics;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace EverscaleNet.Client.Models
{
    /// <summary>
    /// <para>Not described yet..</para>
    /// </summary>
    public class EndpointsSet
    {
        /// <summary>
        /// <para>List of endpoints provided by server</para>
        /// </summary>
        [JsonPropertyName("endpoints")]
        public string[] Endpoints { get; set; }
    }
}