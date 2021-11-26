using Dahomey.Json.Attributes;
using System;
using System.Numerics;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace ch1seL.TonNet.Client.Models
{
    /// <summary>
    /// Not described yet..
    /// </summary>
    public abstract class BocCacheType
    {
        /// <summary>
        /// <para>Pin the BOC with `pin` name.</para>
        /// <para>Such BOC will not be removed from cache until it is unpinned</para>
        /// </summary>
        [JsonDiscriminator("Pinned")]
        public class Pinned : BocCacheType
        {
            /// <summary>
            /// <para>Pin the BOC with `pin` name.</para>
            /// <para>Such BOC will not be removed from cache until it is unpinned</para>
            /// </summary>
            [JsonPropertyName("pin")]
            public string Pin { get; set; }
        }

        /// <summary>
        ///  
        /// </summary>
        [JsonDiscriminator("Unpinned")]
        public class Unpinned : BocCacheType
        {
        }
    }
}