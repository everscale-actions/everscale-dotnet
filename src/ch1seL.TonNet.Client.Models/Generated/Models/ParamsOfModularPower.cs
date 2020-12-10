using Dahomey.Json.Attributes;
using System;
using System.Numerics;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace ch1seL.TonNet.Client.Models
{
    /// <summary>
    /// Not described yet..
    /// </summary>
    public class ParamsOfModularPower
    {
        /// <summary>
        /// Not described yet..
        /// </summary>
        [JsonPropertyName("base")]
        public string Base { get; set; }

        /// <summary>
        /// Not described yet..
        /// </summary>
        [JsonPropertyName("exponent")]
        public string Exponent { get; set; }

        /// <summary>
        /// Not described yet..
        /// </summary>
        [JsonPropertyName("modulus")]
        public string Modulus { get; set; }
    }
}