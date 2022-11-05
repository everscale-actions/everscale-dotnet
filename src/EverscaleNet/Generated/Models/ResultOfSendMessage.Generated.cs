using System;
using System.Numerics;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace EverscaleNet.Client.Models
{
    /// <summary>
    /// <para>Not described yet..</para>
    /// </summary>
    public class ResultOfSendMessage
    {
        /// <summary>
        /// <para>The last generated shard block of the message destination account before the message was sent.</para>
        /// <para>This block id must be used as a parameter of the</para>
        /// <para>`wait_for_transaction`.</para>
        /// </summary>
        [JsonPropertyName("shard_block_id")]
        public string ShardBlockId { get; set; }

        /// <summary>
        /// <para>The list of endpoints to which the message was sent.</para>
        /// <para>This list id must be used as a parameter of the</para>
        /// <para>`wait_for_transaction`.</para>
        /// </summary>
        [JsonPropertyName("sending_endpoints")]
        public string[] SendingEndpoints { get; set; }
    }
}