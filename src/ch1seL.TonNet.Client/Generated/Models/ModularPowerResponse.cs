using System;
using System.Numerics;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace ch1seL.TonNet.Client.Models
{
    /// <summary>
    /// Not described yet..
    /// </summary>
    public class ModularPowerResponse
    {
        /// <summary>
        ///  Result of modular exponentiation
        /// </summary>
        [JsonPropertyName("modular_power")]
        public string ModularPower { get; set; }
    }
}