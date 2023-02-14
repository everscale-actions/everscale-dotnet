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
    public enum SortDirection
    {
        /// <summary>
        /// <para>Not described yet..</para>
        /// </summary>
        ASC,
        /// <summary>
        /// <para>Not described yet..</para>
        /// </summary>
        DESC
    }
}