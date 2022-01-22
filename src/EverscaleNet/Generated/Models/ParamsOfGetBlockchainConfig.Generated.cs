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
    public class ParamsOfGetBlockchainConfig
    {
        /// <summary>
        /// Key block BOC or zerostate BOC encoded as base64
        /// </summary>
        [JsonPropertyName("block_boc")]
        public string BlockBoc { get; set; }
    }
}