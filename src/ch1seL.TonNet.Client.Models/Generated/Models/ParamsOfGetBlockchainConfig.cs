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
    public class ParamsOfGetBlockchainConfig
    {
        /// <summary>
        /// Key block BOC encoded as base64
        /// </summary>
        [JsonPropertyName("block_boc")]
        public string BlockBoc { get; set; }
    }
}