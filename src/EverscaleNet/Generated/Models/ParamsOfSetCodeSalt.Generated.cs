using System;
using System.Numerics;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace EverscaleNet.Client.Models
{
    /// <summary>
    /// <para>Not described yet..</para>
    /// </summary>
    public class ParamsOfSetCodeSalt
    {
        /// <summary>
        /// <para>Contract code BOC encoded as base64 or code BOC handle</para>
        /// </summary>
        [JsonPropertyName("code")]
        public string Code { get; set; }

        /// <summary>
        /// <para>Code salt to set.</para>
        /// <para>BOC encoded as base64 or BOC handle</para>
        /// </summary>
        [JsonPropertyName("salt")]
        public string Salt { get; set; }

        /// <summary>
        /// <para>Cache type to put the result. The BOC itself returned if no cache type provided.</para>
        /// </summary>
        [JsonPropertyName("boc_cache")]
        public BocCacheType BocCache { get; set; }
    }
}