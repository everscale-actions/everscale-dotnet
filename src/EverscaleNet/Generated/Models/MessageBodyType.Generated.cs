using Dahomey.Json.Attributes;
using System;
using System.Numerics;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace EverscaleNet.Client.Models
{
    /// <summary>
    /// Not described yet..
    /// </summary>
    public enum MessageBodyType
    {
        /// <summary>
        /// Not described yet..
        /// </summary>
        Input,
        /// <summary>
        /// Not described yet..
        /// </summary>
        Output,
        /// <summary>
        /// Not described yet..
        /// </summary>
        InternalOutput,
        /// <summary>
        /// Not described yet..
        /// </summary>
        Event
    }
}