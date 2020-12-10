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
    public class ResultOfQuery
    {
        /// <summary>
        /// Result provided by DAppServer.
        /// </summary>
        [JsonPropertyName("result")]
        public JsonElement? Result { get; set; }
    }
}