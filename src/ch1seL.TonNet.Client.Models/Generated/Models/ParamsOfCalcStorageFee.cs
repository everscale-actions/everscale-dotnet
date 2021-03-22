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
    public class ParamsOfCalcStorageFee
    {
        /// <summary>
        /// Not described yet..
        /// </summary>
        [JsonPropertyName("account")]
        public string Account { get; set; }

        /// <summary>
        /// Not described yet..
        /// </summary>
        [JsonPropertyName("period")]
        public uint Period { get; set; }
    }
}