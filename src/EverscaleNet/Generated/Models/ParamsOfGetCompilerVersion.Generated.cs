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
    public class ParamsOfGetCompilerVersion
    {
        /// <summary>
        /// Contract code BOC encoded as base64 or code BOC handle
        /// </summary>
        [JsonPropertyName("code")]
        public string Code { get; set; }
    }
}