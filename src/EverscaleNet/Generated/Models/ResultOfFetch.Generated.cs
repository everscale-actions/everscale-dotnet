using System;
using System.Numerics;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace EverscaleNet.Client.Models
{
    /// <summary>
    /// <para>[UNSTABLE](UNSTABLE.md) [DEPRECATED](DEPRECATED.md)</para>
    /// </summary>
    public class ResultOfFetch
    {
        /// <summary>
        /// <para>Debot metadata.</para>
        /// </summary>
        [JsonPropertyName("info")]
        public DebotInfo Info { get; set; }
    }
}