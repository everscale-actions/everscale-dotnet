using System;
using System.Numerics;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace EverscaleNet.Client.Models
{
    /// <summary>
    /// <para>Not described yet..</para>
    /// </summary>
    public class ResultOfFactorize
    {
        /// <summary>
        /// <para>Two factors of composite or empty if composite can't be factorized.</para>
        /// </summary>
        [JsonPropertyName("factors")]
        public string[] Factors { get; set; }
    }
}