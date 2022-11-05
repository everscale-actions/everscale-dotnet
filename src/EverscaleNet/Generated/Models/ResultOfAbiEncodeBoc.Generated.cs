using System;
using System.Numerics;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace EverscaleNet.Client.Models
{
    /// <summary>
    /// <para>Not described yet..</para>
    /// </summary>
    public class ResultOfAbiEncodeBoc
    {
        /// <summary>
        /// <para>BOC encoded as base64</para>
        /// </summary>
        [JsonPropertyName("boc")]
        public string Boc { get; set; }
    }
}