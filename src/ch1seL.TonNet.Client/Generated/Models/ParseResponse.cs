using System;
using System.Numerics;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace ch1seL.TonNet.Client.Models
{
    /// <summary>
    /// Not described yet..
    /// </summary>
    public class ParseResponse
    {
        /// <summary>
        ///  JSON containing parsed BOC
        /// </summary>
        [JsonPropertyName("parsed")]
        public JsonElement Parsed { get; set; }
    }
}