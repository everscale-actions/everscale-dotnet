using System;
using System.Numerics;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace EverscaleNet.Client.Models
{
    /// <summary>
    /// <para>Not described yet..</para>
    /// </summary>
    public class ParamsOfParseShardstate
    {
        /// <summary>
        /// <para>BOC encoded as base64</para>
        /// </summary>
        [JsonPropertyName("boc")]
        public string Boc { get; set; }

        /// <summary>
        /// <para>Shardstate identificator</para>
        /// </summary>
        [JsonPropertyName("id")]
        public string Id { get; set; }

        /// <summary>
        /// <para>Workchain shardstate belongs to</para>
        /// </summary>
        [JsonPropertyName("workchain_id")]
        public int WorkchainId { get; set; }
    }
}