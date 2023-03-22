using System;
using System.Numerics;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace EverscaleNet.Client.Models
{
    /// <summary>
    /// <para>Not described yet..</para>
    /// </summary>
    public class ParamsOfFetchNextMonitorResults
    {
        /// <summary>
        /// <para>Name of the monitoring queue.</para>
        /// </summary>
        [JsonPropertyName("queue")]
        public string Queue { get; set; }

        /// <summary>
        /// <para>Wait mode.</para>
        /// <para>Default is `NO_WAIT`.</para>
        /// </summary>
        [JsonPropertyName("wait_mode")]
        public MonitorFetchWaitMode? WaitMode { get; set; }
    }
}