using System;
using System.Numerics;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace EverscaleNet.Client.Models
{
    /// <summary>
    /// <para>Not described yet..</para>
    /// </summary>
    public class ResultOfEncodeBoc
    {
        /// <summary>
        /// <para>Encoded cell BOC or BOC cache key.</para>
        /// </summary>
        [JsonPropertyName("boc")]
        public string Boc { get; set; }
    }
}