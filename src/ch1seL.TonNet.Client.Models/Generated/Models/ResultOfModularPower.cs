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
    public class ResultOfModularPower
    {
        /// <summary>
        /// Not described yet..
        /// </summary>
        [JsonPropertyName("modular_power")]
        public string ModularPower { get; set; }
    }
}