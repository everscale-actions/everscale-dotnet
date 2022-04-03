using Dahomey.Json.Attributes;
using System;
using System.Numerics;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace EverscaleNet.Client.Models
{
    /// <summary>
    /// Network protocol used to perform GraphQL queries.

    /// </summary>
    public enum NetworkQueriesProtocol
    {
        /// <summary>
        /// Not described yet..
        /// </summary>
        HTTP,
        /// <summary>
        /// Not described yet..
        /// </summary>
        WS
    }
}