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
    public enum MessageBodyType
    {
        /// <summary>
        /// <para>Not described yet..</para>
        /// </summary>
        Input,
        /// <summary>
        /// <para>Not described yet..</para>
        /// </summary>
        Output,
        /// <summary>
        /// <para>Not described yet..</para>
        /// </summary>
        InternalOutput,
        /// <summary>
        /// <para>Not described yet..</para>
        /// </summary>
        Event
    }
}