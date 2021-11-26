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
    public class ParamsOfSendMessage
    {
        /// <summary>
        /// Message BOC.
        /// </summary>
        [JsonPropertyName("message")]
        public string Message { get; set; }

        /// <summary>
        /// <para>Optional message ABI.</para>
        /// <para>If this parameter is specified and the message has the</para>
        /// <para>`expire` header then expiration time will be checked against</para>
        /// <para>the current time to prevent unnecessary sending of already expired message.</para>
        /// <para>The `message already expired` error will be returned in this</para>
        /// <para>case.</para>
        /// <para>Note, that specifying `abi` for ABI compliant contracts is</para>
        /// <para>strongly recommended, so that proper processing strategy can be</para>
        /// <para>chosen.</para>
        /// </summary>
        [JsonPropertyName("abi")]
        public Abi Abi { get; set; }

        /// <summary>
        /// Flag for requesting events sending
        /// </summary>
        [JsonPropertyName("send_events")]
        public bool SendEvents { get; set; }
    }
}