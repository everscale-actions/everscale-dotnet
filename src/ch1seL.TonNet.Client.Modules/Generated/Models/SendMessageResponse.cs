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
    public class SendMessageResponse
    {
        /// <summary>
        /// <para> The last generated shard block of the message destination account before the</para>
        /// <para> message was sent.</para>
        /// <para> This block id must be used as a parameter of the</para>
        /// <para> `wait_for_transaction`.</para>
        /// </summary>
        [JsonPropertyName("shard_block_id")]
        public string ShardBlockId { get; set; }
    }
}