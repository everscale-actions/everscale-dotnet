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
    public class ResultOfConvertAddress
    {
        /// <summary>
        /// Not described yet..
        /// </summary>
        [JsonPropertyName("address")]
        public string Address { get; set; }
    }
}