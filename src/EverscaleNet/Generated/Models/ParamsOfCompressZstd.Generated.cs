using System;
using System.Numerics;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace EverscaleNet.Client.Models
{
    /// <summary>
    /// <para>Not described yet..</para>
    /// </summary>
    public class ParamsOfCompressZstd
    {
        /// <summary>
        /// <para>Uncompressed data.</para>
        /// <para>Must be encoded as base64.</para>
        /// </summary>
        [JsonPropertyName("uncompressed")]
        public string Uncompressed { get; set; }

        /// <summary>
        /// <para>Compression level, from 1 to 21. Where: 1 - lowest compression level (fastest compression); 21 - highest compression level (slowest compression). If level is omitted, the default compression level is used (currently `3`).</para>
        /// </summary>
        [JsonPropertyName("level")]
        public int? Level { get; set; }
    }
}