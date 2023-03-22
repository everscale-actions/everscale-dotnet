using System;
using System.Numerics;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace EverscaleNet.Client.Models
{
    /// <summary>
    /// <para>Not described yet..</para>
    /// </summary>
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public enum MessageMonitoringStatus
    {
        /// <summary>
        /// <para>Not described yet..</para>
        /// </summary>
        Finalized,
        /// <summary>
        /// <para>Not described yet..</para>
        /// </summary>
        Timeout,
        /// <summary>
        /// <para>Not described yet..</para>
        /// </summary>
        Reserved
    }
}