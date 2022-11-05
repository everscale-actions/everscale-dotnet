using System;
using System.Numerics;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace EverscaleNet.Client.Models
{
    /// <summary>
    /// <para>Not described yet..</para>
    /// </summary>
    public class ResultOfModularPower
    {
        /// <summary>
        /// <para>Result of modular exponentiation</para>
        /// </summary>
        [JsonPropertyName("modular_power")]
        public string ModularPower { get; set; }
    }
}