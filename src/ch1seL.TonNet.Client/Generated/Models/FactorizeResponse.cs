using System;
using System.Numerics;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace ch1seL.TonNet.Client.Models
{
    /// <summary>
    /// Not described yet..
    /// </summary>
    public class FactorizeResponse
    {
        /// <summary>
        ///  Two factors of composite or empty if composite can't be factorized.
        /// </summary>
        [JsonPropertyName("factors")]
        public string[] Factors { get; set; }
    }
}