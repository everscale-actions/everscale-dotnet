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
    public class ParamsOfHash
    {
        /// <summary>
        /// <para>Input data for hash calculation.</para>
        /// <para>Encoded with `base64`.</para>
        /// </summary>
        [JsonPropertyName("data")]
        public string Data { get; set; }
    }
}