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
    public class ResultOfGetCodeSalt
    {
        /// <summary>
        /// <para>Contract code salt if present.</para>
        /// <para>BOC encoded as base64 or BOC handle</para>
        /// </summary>
        [JsonPropertyName("salt")]
        public string Salt { get; set; }
    }
}