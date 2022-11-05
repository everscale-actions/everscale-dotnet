using System;
using System.Numerics;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace EverscaleNet.Client.Models
{
    /// <summary>
    /// <para>Not described yet..</para>
    /// </summary>
    public class ResultOfEncodeTvc
    {
        /// <summary>
        /// <para>Contract TVC image BOC encoded as base64 or BOC handle of boc_cache parameter was specified</para>
        /// </summary>
        [JsonPropertyName("tvc")]
        public string Tvc { get; set; }
    }
}