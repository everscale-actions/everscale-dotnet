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
    public class ResultOfGetCodeFromTvc
    {
        /// <summary>
        /// Contract code encoded as base64
        /// </summary>
        [JsonPropertyName("code")]
        public string Code { get; set; }
    }
}