using System;
using System.Numerics;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace EverscaleNet.Client.Models
{
    /// <summary>
    /// <para>Not described yet..</para>
    /// </summary>
    public class ParamsOfGetBlockchainConfig
    {
        /// <summary>
        /// <para>Key block BOC or zerostate BOC encoded as base64</para>
        /// </summary>
        [JsonPropertyName("block_boc")]
        public string BlockBoc { get; set; }
    }
}