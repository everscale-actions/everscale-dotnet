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
        /// <para>If it is specified, then the output messages' bodies will be</para>
        /// <para>decoded according to this ABI.</para>
        /// <para>The `abi_decoded` result field will be filled out.</para>
        /// </summary>
        [JsonPropertyName("abi")]
        public Abi Abi { get; set; }

        /// <summary>
        /// Encoded with `base64`.
        /// </summary>
        [JsonPropertyName("message")]
        public string Message { get; set; }

        /// <summary>
        /// You must provide the same value as the `send_message` has returned.
        /// </summary>
        [JsonPropertyName("shard_block_id")]
        public string ShardBlockId { get; set; }

        /// <summary>
        /// Not described yet..
        /// </summary>
        [JsonPropertyName("send_events")]
        public bool SendEvents { get; set; }
    }
}