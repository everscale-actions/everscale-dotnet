using System;
using System.Numerics;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace EverscaleNet.Client.Models
{
    /// <summary>
    /// <para>Network protocol used to perform GraphQL queries.</para>
    /// </summary>
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public enum NetworkQueriesProtocol
    {
        /// <summary>
        /// <para>Not described yet..</para>
        /// </summary>
        HTTP,
        /// <summary>
        /// <para>Not described yet..</para>
        /// </summary>
        WS
    }
}