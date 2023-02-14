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
    public enum AggregationFn
    {
        /// <summary>
        /// <para>Not described yet..</para>
        /// </summary>
        COUNT,
        /// <summary>
        /// <para>Not described yet..</para>
        /// </summary>
        MIN,
        /// <summary>
        /// <para>Not described yet..</para>
        /// </summary>
        MAX,
        /// <summary>
        /// <para>Not described yet..</para>
        /// </summary>
        SUM,
        /// <summary>
        /// <para>Not described yet..</para>
        /// </summary>
        AVERAGE
    }
}