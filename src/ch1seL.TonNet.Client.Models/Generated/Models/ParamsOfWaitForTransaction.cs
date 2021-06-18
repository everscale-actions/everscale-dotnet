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
    public class ParamsOfWaitForTransaction
    {
        /// <summary>
        /// <para>Optional ABI for decoding the transaction result.</para>
        /// <para>If it is specified, then the output messages' bodies will be</para>
        /// <para>decoded according to this ABI.</para>
        /// <para>The `abi_decoded` result field will be filled out.</para>
        /// </summary>
        [JsonPropertyName("abi")]
        public Abi Abi { get; set; }

        /// <summary>
        /// <para>Message BOC.</para>
        /// <para>Encoded with `base64`.</para>
        /// </summary>
        [JsonPropertyName("message")]
        public string Message { get; set; }

        /// <summary>
        /// <para>The last generated block id of the destination account shard before the message was sent.</para>
        /// <para>You must provide the same value as the `send_message` has returned.</para>
        /// </summary>
        [JsonPropertyName("shard_block_id")]
        public string ShardBlockId { get; set; }

        /// <summary>
        /// Flag that enables/disables intermediate events
        /// </summary>
        [JsonPropertyName("send_events")]
        public bool SendEvents { get; set; }

        /// <summary>
        /// <para>The list of endpoints to which the message was sent.</para>
        /// <para>Use this field to get more informative errors.</para>
        /// <para>Provide the same value as the `send_message` has returned.</para>
        /// <para>If the message was not delivered (expired), SDK will log the endpoint URLs, used for its sending.</para>
        /// </summary>
        [JsonPropertyName("sending_endpoints")]
        public string[] SendingEndpoints { get; set; }
    }
}