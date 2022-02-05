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
    public class ParamsOfEncodeExternalInMessage
    {
        /// <summary>
        /// Source address.
        /// </summary>
        [JsonPropertyName("src")]
        public string Src { get; set; }

        /// <summary>
        /// Destination address.
        /// </summary>
        [JsonPropertyName("dst")]
        public string Dst { get; set; }

        /// <summary>
        /// Bag of cells with state init (used in deploy messages).
        /// </summary>
        [JsonPropertyName("init")]
        public string Init { get; set; }

        /// <summary>
        /// Bag of cells with the message body encoded as base64.
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