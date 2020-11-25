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
    public class ParamsOfFactorize
    {
        /// <summary>
        ///  Hexadecimal representation of u64 composite number.
        /// </summary>
        [JsonPropertyName("composite")]
        public string Composite { get; set; }
    }
}