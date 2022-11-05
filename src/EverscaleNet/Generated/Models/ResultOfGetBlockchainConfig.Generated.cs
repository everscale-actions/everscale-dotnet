using System;
using System.Numerics;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace EverscaleNet.Client.Models
{
    /// <summary>
    /// <para>Not described yet..</para>
    /// </summary>
    public class ResultOfGetBlockchainConfig
    {
        /// <summary>
        /// <para>Blockchain config BOC encoded as base64</para>
        /// </summary>
        [JsonPropertyName("config_boc")]
        public string ConfigBoc { get; set; }
    }
}