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
    public class GetBocHashRequest
    {
        /// <summary>
        ///  BOC encoded as base64
        /// </summary>
        [JsonPropertyName("boc")]
        public string Boc { get; set; }
    }
}