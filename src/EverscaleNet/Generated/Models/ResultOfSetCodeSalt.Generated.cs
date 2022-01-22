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
    public class ResultOfSetCodeSalt
    {
        /// <summary>
        /// <para>Contract code with salt set.</para>
        /// <para>BOC encoded as base64 or BOC handle</para>
        /// </summary>
        [JsonPropertyName("code")]
        public string Code { get; set; }
    }
}