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
    public class ResultOfModularPower
    {
        /// <summary>
        /// Result of modular exponentiation
        /// </summary>
        [JsonPropertyName("modular_power")]
        public string ModularPower { get; set; }
    }
}