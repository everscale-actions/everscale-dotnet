using Dahomey.Json.Attributes;
using System;
using System.Numerics;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace ch1seL.TonNet.Client.Models
{
    /// <summary>
    /// [UNSTABLE](UNSTABLE.md)
    /// </summary>
    public class ResultOfFetch
    {
        /// <summary>
        /// Debot metadata.
        /// </summary>
        [JsonPropertyName("info")]
        public DebotInfo Info { get; set; }
    }
}