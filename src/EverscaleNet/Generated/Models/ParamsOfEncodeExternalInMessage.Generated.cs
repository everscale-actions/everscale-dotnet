using System;
using System.Numerics;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace EverscaleNet.Client.Models
{
    /// <summary>
    /// <para>Not described yet..</para>
    /// </summary>
    public class ParamsOfEncodeExternalInMessage
    {
        /// <summary>
        /// <para>Source address.</para>
        /// </summary>
        [JsonPropertyName("src")]
        public string Src { get; set; }

        /// <summary>
        /// <para>Destination address.</para>
        /// </summary>
        [JsonPropertyName("dst")]
        public string Dst { get; set; }

        /// <summary>
        /// <para>Bag of cells with state init (used in deploy messages).</para>
        /// </summary>
        [JsonPropertyName("init")]
        public string Init { get; set; }

        /// <summary>
        /// <para>Bag of cells with the message body encoded as base64.</para>
        /// </summary>
        [JsonPropertyName("body")]
        public string Body { get; set; }

        /// <summary>
        /// <para>Cache type to put the result.</para>
        /// <para>The BOC itself returned if no cache type provided</para>
        /// </summary>
        [JsonPropertyName("boc_cache")]
        public BocCacheType BocCache { get; set; }
    }
}