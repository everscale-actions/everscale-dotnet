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
    public class ResultOfEncodeInitialData
    {
        /// <summary>
        /// Updated data BOC or BOC handle
        /// </summary>
        [JsonPropertyName("data")]
        public string Data { get; set; }
    }
}