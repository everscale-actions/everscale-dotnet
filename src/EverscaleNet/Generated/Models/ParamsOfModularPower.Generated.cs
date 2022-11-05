using System;
using System.Numerics;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace EverscaleNet.Client.Models
{
    /// <summary>
    /// <para>Not described yet..</para>
    /// </summary>
    public class ParamsOfModularPower
    {
        /// <summary>
        /// <para>`base` argument of calculation.</para>
        /// </summary>
        [JsonPropertyName("base")]
        public string Base { get; set; }

        /// <summary>
        /// <para>`exponent` argument of calculation.</para>
        /// </summary>
        [JsonPropertyName("exponent")]
        public string Exponent { get; set; }

        /// <summary>
        /// <para>`modulus` argument of calculation.</para>
        /// </summary>
        [JsonPropertyName("modulus")]
        public string Modulus { get; set; }
    }
}