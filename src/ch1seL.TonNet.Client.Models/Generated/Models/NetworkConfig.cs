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
    public class NetworkConfig
    {
        /// <summary>
        /// DApp Server public address. For instance, for `net.ton.dev/graphql` GraphQL endpoint the server address will be net.ton.dev
        /// </summary>
        [JsonPropertyName("server_address")]
        public string ServerAddress { get; set; }

        /// <summary>
        /// <para>List of DApp Server addresses.</para>
        /// <para>Any correct URL format can be specified, including IP addresses This parameter is prevailing over `server_address`.</para>
        /// </summary>
        [JsonPropertyName("endpoints")]
        public string[] Endpoints { get; set; }

        /// <summary>
        /// <para>Deprecated.</para>
        /// <para>You must use `network.max_reconnect_timeout` that allows to specify maximum network resolving timeout.</para>
        /// </summary>
        [JsonPropertyName("network_retries_count")]
        public sbyte? NetworkRetriesCount { get; set; }

        /// <summary>
        /// <para>Maximum time for sequential reconnections in ms.</para>
        /// <para>Default value is 120000 (2 min)</para>
        /// </summary>
        [JsonPropertyName("max_reconnect_timeout")]
        public uint? MaxReconnectTimeout { get; set; }

        /// <summary>
        /// Deprecated
        /// </summary>
        [JsonPropertyName("reconnect_timeout")]
        public uint? ReconnectTimeout { get; set; }

        /// <summary>
        /// The number of automatic message processing retries that SDK performs in case of `Message Expired (507)` error - but only for those messages which local emulation was successful or failed with replay protection error. The default value is 5.
        /// </summary>
        [JsonPropertyName("message_retries_count")]
        public sbyte? MessageRetriesCount { get; set; }

        /// <summary>
        /// Timeout that is used to process message delivery for the contracts which ABI does not include "expire" header. If the message is not delivered within the specified timeout the appropriate error occurs.
        /// </summary>
        [JsonPropertyName("message_processing_timeout")]
        public uint? MessageProcessingTimeout { get; set; }

        /// <summary>
        /// Maximum timeout that is used for query response. The default value is 40 sec.
        /// </summary>
        [JsonPropertyName("wait_for_timeout")]
        public uint? WaitForTimeout { get; set; }

        /// <summary>
        /// <para>Maximum time difference between server and client.</para>
        /// <para>If client's device time is out of sync and difference is more than the threshold then error will occur. Also an error will occur if the specified threshold is more than</para>
        /// <para>`message_processing_timeout/2`.</para>
        /// <para>The default value is 15 sec.</para>
        /// </summary>
        [JsonPropertyName("out_of_sync_threshold")]
        public uint? OutOfSyncThreshold { get; set; }

        /// <summary>
        /// <para>Access key to GraphQL API.</para>
        /// <para>At the moment is not used in production</para>
        /// </summary>
        [JsonPropertyName("access_key")]
        public string AccessKey { get; set; }
    }
}