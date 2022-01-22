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
    public class ResultOfConvertAddress
    {
        /// <summary>
        /// Address in the specified format
        /// </summary>
        [JsonPropertyName("address")]
        public string Address { get; set; }
    }
}