using System;
using System.Numerics;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace ch1seL.TonNet.Client.Models
{
    /// <summary>
    /// Not described yet..
    /// </summary>
    public class RunGetResponse
    {
        /// <summary>
        ///  Values returned by getmethod on stack
        /// </summary>
        [JsonPropertyName("output")]
        public JsonElement Output { get; set; }
    }
}