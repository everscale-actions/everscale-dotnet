using Dahomey.Json.Attributes;
using System;
using System.Numerics;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace ch1seL.TonNet.Client.Models
{
    /// <summary>
    /// Not described yet..
    /// </summary>
    public class EndpointsSet
    {
        /// <summary>
        /// List of endpoints provided by server
        /// </summary>
        [JsonPropertyName("endpoints")]
        public string[] Endpoints { get; set; }
    }
}