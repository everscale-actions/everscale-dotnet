using System;
using System.Numerics;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace EverscaleNet.Client.Models
{
    /// <summary>
    /// <para>Not described yet..</para>
    /// </summary>
    public class ResultOfGetCompilerVersion
    {
        /// <summary>
        /// <para>Compiler version, for example 'sol 0.49.0'</para>
        /// </summary>
        [JsonPropertyName("version")]
        public string Version { get; set; }
    }
}