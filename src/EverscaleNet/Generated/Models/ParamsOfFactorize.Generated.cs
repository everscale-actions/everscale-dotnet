using System;
using System.Numerics;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace EverscaleNet.Client.Models
{
    /// <summary>
    /// <para>Not described yet..</para>
    /// </summary>
    public class ParamsOfFactorize
    {
        /// <summary>
        /// <para>Hexadecimal representation of u64 composite number.</para>
        /// </summary>
        [JsonPropertyName("composite")]
        public string Composite { get; set; }
    }
}