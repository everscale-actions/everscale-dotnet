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
    public class NetworkConfig
    {
        /// <summary>
        /// **This field is deprecated, but left for backward-compatibility.** Evernode endpoint.
        /// </summary>
        [JsonPropertyName("server_address")]
        public string ServerAddress { get; set; }

        /// <summary>
        /// <para>List of Evernode endpoints.</para>
        /// <para>Any correct URL format can be specified, including IP addresses. This parameter is prevailing over `server_address`.</para>
        /// <para>Check the full list of [supported network endpoints](https://docs.everos.dev/ever-sdk/reference/ever-os-api/networks).</para>
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
        /// <para>Maximum time for sequential reconnections.</para>
        /// <para>Must be specified in milliseconds. Default is 120000 (2 min).</para>
        /// </summary>
        [JsonPropertyName("max_reconnect_timeout")]
        public uint? MaxReconnectTimeout { get; set; }

        /// <summary>
        /// Deprecated
        /// </summary>
        [JsonPropertyName("reconnect_timeout")]
        public uint? ReconnectTimeout { get; set; }

        /// <summary>
        /// <para>The number of automatic message processing retries that SDK performs in case of `Message Expired (507)` error - but only for those messages which local emulation was successful or failed with replay protection error.</para>
        /// <para>Default is 5.</para>
        /// </summary>
        [JsonPropertyName("message_retries_count")]
        public sbyte? MessageRetriesCount { get; set; }

        /// <summary>
        /// <para>Timeout that is used to process message delivery for the contracts which ABI does not include "expire" header. If the message is not delivered within the specified timeout the appropriate error occurs.</para>
        /// <para>Must be specified in milliseconds. Default is 40000 (40 sec).</para>
        /// </summary>
        [JsonPropertyName("message_processing_timeout")]
        public uint? MessageProcessingTimeout { get; set; }

        /// <summary>
        /// <para>Maximum timeout that is used for query response.</para>
        /// <para>Must be specified in milliseconds. Default is 40000 (40 sec).</para>
        /// </summary>
        [JsonPropertyName("wait_for_timeout")]
        public uint? WaitForTimeout { get; set; }

        /// <summary>
        /// <para>Maximum time difference between server and client.</para>
        /// <para>If client's device time is out of sync and difference is more than the threshold then error will occur. Also an error will occur if the specified threshold is more than</para>
        /// <para>`message_processing_timeout/2`.</para>
        /// <para>Must be specified in milliseconds. Default is 15000 (15 sec).</para>
        /// </summary>
        [JsonPropertyName("out_of_sync_threshold")]
        public uint? OutOfSyncThreshold { get; set; }

        /// <summary>
        /// <para>Maximum number of randomly chosen endpoints the library uses to broadcast a message.</para>
        /// <para>Default is 1.</para>
        /// </summary>
        [JsonPropertyName("sending_endpoint_count")]
        public byte? SendingEndpointCount { get; set; }

        /// <summary>
        /// <para>Frequency of sync latency detection.</para>
        /// <para>Library periodically checks the current endpoint for blockchain data syncronization latency.</para>
        /// <para>If the latency (time-lag) is less then `NetworkConfig.max_latency`</para>
        /// <para>then library selects another endpoint.</para>
        /// <para>Must be specified in milliseconds. Default is 60000 (1 min).</para>
        /// </summary>
        [JsonPropertyName("latency_detection_interval")]
        public uint? LatencyDetectionInterval { get; set; }

        /// <summary>
        /// <para>Maximum value for the endpoint's blockchain data syncronization latency (time-lag). Library periodically checks the current endpoint for blockchain data synchronization latency. If the latency (time-lag) is less then `NetworkConfig.max_latency` then library selects another endpoint.</para>
        /// <para>Must be specified in milliseconds. Default is 60000 (1 min).</para>
        /// </summary>
        [JsonPropertyName("max_latency")]
        public uint? MaxLatency { get; set; }

        /// <summary>
        /// <para>Default timeout for http requests.</para>
        /// <para>Is is used when no timeout specified for the request to limit the answer waiting time. If no answer received during the timeout requests ends with</para>
        /// <para>error.</para>
        /// <para>Must be specified in milliseconds. Default is 60000 (1 min).</para>
        /// </summary>
        [JsonPropertyName("query_timeout")]
        public uint? QueryTimeout { get; set; }

        /// <summary>
        /// <para>Queries protocol.</para>
        /// <para>`HTTP` or `WS`. </para>
        /// <para>Default is `HTTP`.</para>
        /// </summary>
        [JsonPropertyName("queries_protocol")]
        public NetworkQueriesProtocol QueriesProtocol { get; set; }

        /// <summary>
        /// <para>UNSTABLE.</para>
        /// <para>First REMP status awaiting timeout. If no status recieved during the timeout than fallback transaction scenario is activated.</para>
        /// <para>Must be specified in milliseconds. Default is 1000 (1 sec).</para>
        /// </summary>
        [JsonPropertyName("first_remp_status_timeout")]
        public uint? FirstRempStatusTimeout { get; set; }

        /// <summary>
        /// <para>UNSTABLE.</para>
        /// <para>Subsequent REMP status awaiting timeout. If no status recieved during the timeout than fallback transaction scenario is activated.</para>
        /// <para>Must be specified in milliseconds. Default is 5000 (5 sec).</para>
        /// </summary>
        [JsonPropertyName("next_remp_status_timeout")]
        public uint? NextRempStatusTimeout { get; set; }

        /// <summary>
        /// Access key to GraphQL API (Project secret)
        /// </summary>
        [JsonPropertyName("access_key")]
        public string AccessKey { get; set; }
    }
}