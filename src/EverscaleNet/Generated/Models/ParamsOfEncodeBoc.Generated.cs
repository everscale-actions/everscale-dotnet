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
    public class ParamsOfEncodeBoc
    {
        /// <summary>
        /// Cell builder operations.
        /// </summary>
        [JsonPropertyName("builder")]
        public BuilderOp[] Builder { get; set; }

        /// <summary>
        /// Cache type to put the result. The BOC itself returned if no cache type provided.
        /// </summary>
        [JsonPropertyName("boc_cache")]
        public BocCacheType BocCache { get; set; }
    }
}