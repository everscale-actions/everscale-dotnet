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
    public enum MonitorFetchWaitMode
    {
        /// <summary>
        /// <para>Not described yet..</para>
        /// </summary>
        AtLeastOne,
        /// <summary>
        /// <para>Not described yet..</para>
        /// </summary>
        All,
        /// <summary>
        /// <para>Not described yet..</para>
        /// </summary>
        NoWait
    }
}