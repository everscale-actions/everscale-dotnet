using Dahomey.Json.Attributes;
using System;
using System.Numerics;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace ch1seL.TonNet.Client.Models
{
    /// <summary>
    /// Not described yet..
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