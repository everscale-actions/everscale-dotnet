using System;
using System.Numerics;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace ch1seL.TonNet.Client.Models
{
    /// <summary>
    /// Not described yet..
    /// </summary>
    public class GetApiReferenceResponse
    {
        /// <summary>
        /// Not described yet..
        /// </summary>
        [JsonPropertyName("api")]
        public JsonElement Api { get; set; }
    }
}