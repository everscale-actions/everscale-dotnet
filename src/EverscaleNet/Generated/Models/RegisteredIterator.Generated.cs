using System;
using System.Numerics;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace EverscaleNet.Client.Models
{
    /// <summary>
    /// <para>Not described yet..</para>
    /// </summary>
    public class RegisteredIterator
    {
        /// <summary>
        /// <para>Iterator handle.</para>
        /// <para>Must be removed using `remove_iterator`</para>
        /// <para>when it is no more needed for the application.</para>
        /// </summary>
        [JsonPropertyName("handle")]
        public uint Handle { get; set; }
    }
}