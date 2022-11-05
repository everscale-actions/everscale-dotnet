using System;
using System.Numerics;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace EverscaleNet.Client.Models
{
    /// <summary>
    /// <para>Not described yet..</para>
    /// </summary>
    public class ResultOfSubscribeCollection
    {
        /// <summary>
        /// <para>Subscription handle.</para>
        /// <para>Must be closed with `unsubscribe`</para>
        /// </summary>
        [JsonPropertyName("handle")]
        public uint Handle { get; set; }
    }
}