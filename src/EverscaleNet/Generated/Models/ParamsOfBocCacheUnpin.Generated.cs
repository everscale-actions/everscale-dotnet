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
    public class ParamsOfBocCacheUnpin
    {
        /// <summary>
        /// Pinned name
        /// </summary>
        [JsonPropertyName("pin")]
        public string Pin { get; set; }

        /// <summary>
        /// <para>Reference to the cached BOC.</para>
        /// <para>If it is provided then only referenced BOC is unpinned</para>
        /// </summary>
        [JsonPropertyName("boc_ref")]
        public string BocRef { get; set; }
    }
}