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
    public class ResultOfCalcStorageFee
    {
        /// <summary>
        /// Not described yet..
        /// </summary>
        [JsonPropertyName("fee")]
        public string Fee { get; set; }
    }
}