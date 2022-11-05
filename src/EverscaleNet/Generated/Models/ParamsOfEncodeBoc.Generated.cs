using System;
using System.Numerics;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace EverscaleNet.Client.Models
{
    /// <summary>
    /// <para>Not described yet..</para>
    /// </summary>
    public class ParamsOfEncodeBoc
    {
        /// <summary>
        /// <para>Cell builder operations.</para>
        /// </summary>
        [JsonPropertyName("builder")]
        public BuilderOp[] Builder { get; set; }

        /// <summary>
        /// <para>Cache type to put the result. The BOC itself returned if no cache type provided.</para>
        /// </summary>
        [JsonPropertyName("boc_cache")]
        public BocCacheType BocCache { get; set; }
    }
}