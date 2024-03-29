using System;
using System.Numerics;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace EverscaleNet.Client.Models
{
    /// <summary>
    /// <para>Not described yet..</para>
    /// </summary>
    [JsonPolymorphic(TypeDiscriminatorPropertyName = "type")]
    [JsonDerivedType(typeof(Pinned), nameof(Pinned))]
    [JsonDerivedType(typeof(Unpinned), nameof(Unpinned))]
    public abstract class BocCacheType
    {
        /// <summary>
        /// <para>Pin the BOC with `pin` name.</para>
        /// <para>Such BOC will not be removed from cache until it is unpinned BOCs can have several pins and each of the pins has reference counter indicating how many</para>
        /// <para>times the BOC was pinned with the pin. BOC is removed from cache after all references for all</para>
        /// <para>pins are unpinned with `cache_unpin` function calls.</para>
        /// </summary>
        public class Pinned : BocCacheType
        {
            /// <summary>
            /// <para>Not described yet..</para>
            /// </summary>
            [JsonPropertyName("pin")]
            public string Pin { get; set; }
        }

        /// <summary>
        /// <para>BOC is placed into a common BOC pool with limited size regulated by LRU (least recently used) cache lifecycle.</para>
        /// <para>BOC resides there until it is replaced with other BOCs if it is not used</para>
        /// </summary>
        public class Unpinned : BocCacheType
        {
        }
    }
}