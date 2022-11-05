using System;
using System.Numerics;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace EverscaleNet.Client.Models
{
    /// <summary>
    /// <para>Not described yet..</para>
    /// </summary>
    public class ProofsConfig
    {
        /// <summary>
        /// <para>Cache proofs in the local storage.</para>
        /// <para>Default is `true`. If this value is set to `true`, downloaded proofs and master-chain BOCs are saved into the</para>
        /// <para>persistent local storage (e.g. file system for native environments or browser's IndexedDB</para>
        /// <para>for the web); otherwise all the data is cached only in memory in current client's context</para>
        /// <para>and will be lost after destruction of the client.</para>
        /// </summary>
        [JsonPropertyName("cache_in_local_storage")]
        public bool? CacheInLocalStorage { get; set; }
    }
}