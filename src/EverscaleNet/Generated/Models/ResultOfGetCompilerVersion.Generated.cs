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
    public class ResultOfGetCompilerVersion
    {
        /// <summary>
        /// Compiler version, for example 'sol 0.49.0'
        /// </summary>
        [JsonPropertyName("version")]
        public string Version { get; set; }
    }
}