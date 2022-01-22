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
    public class ResultOfFindLastShardBlock
    {
        /// <summary>
        /// Account shard last block ID
        /// </summary>
        [JsonPropertyName("block_id")]
        public string BlockId { get; set; }
    }
}