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
    public abstract class BocCacheType
    {
        /// <summary>
        /// <para>Pin the BOC with `pin` name.</para>
        /// <para>Such BOC will not be removed from cache until it is unpinned BOCs can have several pins and each of the pins has reference counter indicating how many</para>
        /// <para>times the BOC was pinned with the pin. BOC is removed from cache after all references for all</para>
        /// <para>pins are unpinned with `cache_unpin` function calls.</para>
        /// </summary>
        [JsonDiscriminator("Pinned")]
        public class Pinned : BocCacheType
        {
            /// <summary>
            /// <para>Pin the BOC with `pin` name.</para>
            /// <para>Such BOC will not be removed from cache until it is unpinned BOCs can have several pins and each of the pins has reference counter indicating how many</para>
            /// <para>times the BOC was pinned with the pin. BOC is removed from cache after all references for all</para>
            /// <para>pins are unpinned with `cache_unpin` function calls.</para>
            /// </summary>
            [JsonPropertyName("pin")]
            public string Pin { get; set; }
        }

        /// <summary>
        /// <para>BOC is placed into a common BOC pool with limited size regulated by LRU (least recently used) cache lifecycle.</para>
        /// <para>BOC resides there until it is replaced with other BOCs if it is not used</para>
        /// </summary>
        [JsonDiscriminator("Unpinned")]
        public class Unpinned : BocCacheType
        {
        }
    }
}