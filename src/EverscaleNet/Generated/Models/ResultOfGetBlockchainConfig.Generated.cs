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
    public class ResultOfGetBlockchainConfig
    {
        /// <summary>
        /// Blockchain config BOC encoded as base64
        /// </summary>
        [JsonPropertyName("config_boc")]
        public string ConfigBoc { get; set; }
    }
}