using System;
using System.Numerics;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace EverscaleNet.Client.Models
{
    /// <summary>
    /// <para>Not described yet..</para>
    /// </summary>
    public class MonitoringQueueInfo
    {
        /// <summary>
        /// <para>Count of the unresolved messages.</para>
        /// </summary>
        [JsonPropertyName("unresolved")]
        public uint Unresolved { get; set; }

        /// <summary>
        /// <para>Count of resolved results.</para>
        /// </summary>
        [JsonPropertyName("resolved")]
        public uint Resolved { get; set; }
    }
}