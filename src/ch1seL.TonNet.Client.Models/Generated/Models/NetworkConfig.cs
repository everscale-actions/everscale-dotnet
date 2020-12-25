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
        /// <para>Any correct URL format can be specified, including IP addresses</para>
        /// </summary>
        [JsonPropertyName("endpoints")]
        public string[] Endpoints { get; set; }

        /// <summary>
        /// The number of automatic network retries that SDK performs in case of connection problems The default value is 5.
        /// </summary>
        [JsonPropertyName("network_retries_count")]
        public sbyte? NetworkRetriesCount { get; set; }

        /// <summary>
        /// The number of automatic message processing retries that SDK performs in case of `Message Expired (507)` error - but only for those messages which local emulation was successfull or failed with replay protection error. The default value is 5.
        /// </summary>
        [JsonPropertyName("message_retries_count")]
        public sbyte? MessageRetriesCount { get; set; }

        /// <summary>
        /// Timeout that is used to process message delivery for the contracts which ABI does not include "expire" header. If the message is not delivered within the speficied timeout the appropriate error occurs.
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
        /// <para>If client's device time is out of sink and difference is more thanthe threshhold then error will occur. Also the error will occur if the specified threshhold is more than</para>
        /// <para>`message_processing_timeout/2`.</para>
        /// <para>The default value is 15 sec.</para>
        /// </summary>
        [JsonPropertyName("out_of_sync_threshold")]
        public uint? OutOfSyncThreshold { get; set; }

        /// <summary>
        /// Timeout between reconnect attempts
        /// </summary>
        [JsonPropertyName("reconnect_timeout")]
        public uint? ReconnectTimeout { get; set; }

        /// <summary>
        /// <para>Access key to GraphQL API.</para>
        /// <para>At the moment is not used in production</para>
        /// </summary>
        [JsonPropertyName("access_key")]
        public string AccessKey { get; set; }
    }
}