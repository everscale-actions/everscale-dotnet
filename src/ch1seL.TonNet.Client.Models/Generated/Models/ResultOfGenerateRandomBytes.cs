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
    public class ResultOfGenerateRandomBytes
    {
        /// <summary>
        /// Not described yet..
        /// </summary>
        [JsonPropertyName("bytes")]
        public string Bytes { get; set; }
    }
}