using System;
using System.Numerics;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace EverscaleNet.Client.Models
{
    /// <summary>
    /// <para>Not described yet..</para>
    /// </summary>
    public class ParamsOfMonitorMessages
    {
        /// <summary>
        /// <para>Name of the monitoring queue.</para>
        /// </summary>
        [JsonPropertyName("queue")]
        public string Queue { get; set; }

        /// <summary>
        /// <para>Messages to start monitoring for.</para>
        /// </summary>
        [JsonPropertyName("messages")]
        public MessageMonitoringParams[] Messages { get; set; }
    }
}