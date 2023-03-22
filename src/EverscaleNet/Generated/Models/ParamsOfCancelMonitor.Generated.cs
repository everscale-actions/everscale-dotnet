using System;
using System.Numerics;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace EverscaleNet.Client.Models
{
    /// <summary>
    /// <para>Not described yet..</para>
    /// </summary>
    public class ParamsOfCancelMonitor
    {
        /// <summary>
        /// <para>Name of the monitoring queue.</para>
        /// </summary>
        [JsonPropertyName("queue")]
        public string Queue { get; set; }
    }
}