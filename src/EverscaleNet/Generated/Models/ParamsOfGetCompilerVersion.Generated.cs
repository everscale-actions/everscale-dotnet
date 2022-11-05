using System;
using System.Numerics;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace EverscaleNet.Client.Models
{
    /// <summary>
    /// <para>Not described yet..</para>
    /// </summary>
    public class ParamsOfGetCompilerVersion
    {
        /// <summary>
        /// <para>Contract code BOC encoded as base64 or code BOC handle</para>
        /// </summary>
        [JsonPropertyName("code")]
        public string Code { get; set; }
    }
}