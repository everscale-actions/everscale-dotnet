using System;
using System.Numerics;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace EverscaleNet.Client.Models
{
    /// <summary>
    /// <para>Not described yet..</para>
    /// </summary>
    public class ResultOfNaclSignDetachedVerify
    {
        /// <summary>
        /// <para>`true` if verification succeeded or `false` if it failed</para>
        /// </summary>
        [JsonPropertyName("succeeded")]
        public bool Succeeded { get; set; }
    }
}