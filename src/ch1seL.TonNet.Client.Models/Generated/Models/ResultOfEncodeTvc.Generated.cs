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
    public class ResultOfEncodeTvc
    {
        /// <summary>
        /// Contract TVC image BOC encoded as base64 or BOC handle of boc_cache parameter was specified
        /// </summary>
        [JsonPropertyName("tvc")]
        public string Tvc { get; set; }
    }
}