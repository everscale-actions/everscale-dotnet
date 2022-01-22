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
    public class ParamsOfParse
    {
        /// <summary>
        /// BOC encoded as base64
        /// </summary>
        [JsonPropertyName("boc")]
        public string Boc { get; set; }
    }
}