using System;
using System.Numerics;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace EverscaleNet.Client.Models
{
    /// <summary>
    /// <para>Not described yet..</para>
    /// </summary>
    public class ResultOfGetCodeFromTvc
    {
        /// <summary>
        /// <para>Contract code encoded as base64</para>
        /// </summary>
        [JsonPropertyName("code")]
        public string Code { get; set; }
    }
}