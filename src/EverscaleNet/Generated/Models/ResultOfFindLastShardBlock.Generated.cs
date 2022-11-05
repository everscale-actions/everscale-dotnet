using System;
using System.Numerics;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace EverscaleNet.Client.Models
{
    /// <summary>
    /// <para>Not described yet..</para>
    /// </summary>
    public class ResultOfFindLastShardBlock
    {
        /// <summary>
        /// <para>Account shard last block ID</para>
        /// </summary>
        [JsonPropertyName("block_id")]
        public string BlockId { get; set; }
    }
}