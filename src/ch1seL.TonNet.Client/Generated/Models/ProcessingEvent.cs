using System;
using System.Numerics;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace ch1seL.TonNet.Client.Models
{
    /// <summary>
    /// Not described yet..
    /// </summary>
    public abstract class ProcessingEvent
    {
        /// <summary>
        /// <para> Notifies the app that the current shard block will be fetched</para>
        /// <para> from the network.</para>
        /// <para> Fetched block will be used later in waiting phase.</para>
        /// </summary>
        public class WillFetchFirstBlock : ProcessingEvent
        {
        }

        /// <summary>
        /// <para> Notifies the app that the client has failed to fetch current</para>
        /// <para> shard block.</para>
        /// <para> Message processing has finished.</para>
        /// </summary>
        public class FetchFirstBlockFailed : ProcessingEvent
        {
            /// <summary>
            /// <para> Notifies the app that the client has failed to fetch current</para>
            /// <para> shard block.</para>
            /// <para> Message processing has finished.</para>
            /// </summary>
            [JsonPropertyName("error")]
            public ClientError Error { get; set; }
        }

        /// <summary>
        /// <para> Notifies the app that the message will be sent to the</para>
        /// <para> network.</para>
        /// </summary>
        public class WillSend : ProcessingEvent
        {
            /// <summary>
            /// <para> Notifies the app that the message will be sent to the</para>
            /// <para> network.</para>
            /// </summary>
            [JsonPropertyName("shard_block_id")]
            public string ShardBlockId { get; set; }

            /// <summary>
            /// <para> Notifies the app that the message will be sent to the</para>
            /// <para> network.</para>
            /// </summary>
            [JsonPropertyName("message_id")]
            public string MessageId { get; set; }

            /// <summary>
            /// <para> Notifies the app that the message will be sent to the</para>
            /// <para> network.</para>
            /// </summary>
            [JsonPropertyName("message")]
            public string Message { get; set; }
        }

        /// <summary>
        ///  Notifies the app that the message was sent to the network.
        /// </summary>
        public class DidSend : ProcessingEvent
        {
            /// <summary>
            ///  Notifies the app that the message was sent to the network.
            /// </summary>
            [JsonPropertyName("shard_block_id")]
            public string ShardBlockId { get; set; }

            /// <summary>
            ///  Notifies the app that the message was sent to the network.
            /// </summary>
            [JsonPropertyName("message_id")]
            public string MessageId { get; set; }

            /// <summary>
            ///  Notifies the app that the message was sent to the network.
            /// </summary>
            [JsonPropertyName("message")]
            public string Message { get; set; }
        }

        /// <summary>
        /// <para> Notifies the app that the sending operation was failed with</para>
        /// <para> network error.</para>
        /// <para> Nevertheless the processing will be continued at the waiting</para>
        /// <para> phase because the message possibly has been delivered to the</para>
        /// <para> node.</para>
        /// </summary>
        public class SendFailed : ProcessingEvent
        {
            /// <summary>
            /// <para> Notifies the app that the sending operation was failed with</para>
            /// <para> network error.</para>
            /// <para> Nevertheless the processing will be continued at the waiting</para>
            /// <para> phase because the message possibly has been delivered to the</para>
            /// <para> node.</para>
            /// </summary>
            [JsonPropertyName("shard_block_id")]
            public string ShardBlockId { get; set; }

            /// <summary>
            /// <para> Notifies the app that the sending operation was failed with</para>
            /// <para> network error.</para>
            /// <para> Nevertheless the processing will be continued at the waiting</para>
            /// <para> phase because the message possibly has been delivered to the</para>
            /// <para> node.</para>
            /// </summary>
            [JsonPropertyName("message_id")]
            public string MessageId { get; set; }

            /// <summary>
            /// <para> Notifies the app that the sending operation was failed with</para>
            /// <para> network error.</para>
            /// <para> Nevertheless the processing will be continued at the waiting</para>
            /// <para> phase because the message possibly has been delivered to the</para>
            /// <para> node.</para>
            /// </summary>
            [JsonPropertyName("message")]
            public string Message { get; set; }

            /// <summary>
            /// <para> Notifies the app that the sending operation was failed with</para>
            /// <para> network error.</para>
            /// <para> Nevertheless the processing will be continued at the waiting</para>
            /// <para> phase because the message possibly has been delivered to the</para>
            /// <para> node.</para>
            /// </summary>
            [JsonPropertyName("error")]
            public ClientError Error { get; set; }
        }

        /// <summary>
        /// <para> Notifies the app that the next shard block will be fetched</para>
        /// <para> from the network.</para>
        /// <para> Event can occurs more than one time due to block walking</para>
        /// <para> procedure.</para>
        /// </summary>
        public class WillFetchNextBlock : ProcessingEvent
        {
            /// <summary>
            /// <para> Notifies the app that the next shard block will be fetched</para>
            /// <para> from the network.</para>
            /// <para> Event can occurs more than one time due to block walking</para>
            /// <para> procedure.</para>
            /// </summary>
            [JsonPropertyName("shard_block_id")]
            public string ShardBlockId { get; set; }

            /// <summary>
            /// <para> Notifies the app that the next shard block will be fetched</para>
            /// <para> from the network.</para>
            /// <para> Event can occurs more than one time due to block walking</para>
            /// <para> procedure.</para>
            /// </summary>
            [JsonPropertyName("message_id")]
            public string MessageId { get; set; }

            /// <summary>
            /// <para> Notifies the app that the next shard block will be fetched</para>
            /// <para> from the network.</para>
            /// <para> Event can occurs more than one time due to block walking</para>
            /// <para> procedure.</para>
            /// </summary>
            [JsonPropertyName("message")]
            public string Message { get; set; }
        }

        /// <summary>
        /// <para> Notifies the app that the next block can't be fetched due to</para>
        /// <para> error.</para>
        /// <para> Processing will be continued after `network_resume_timeout`.</para>
        /// </summary>
        public class FetchNextBlockFailed : ProcessingEvent
        {
            /// <summary>
            /// <para> Notifies the app that the next block can't be fetched due to</para>
            /// <para> error.</para>
            /// <para> Processing will be continued after `network_resume_timeout`.</para>
            /// </summary>
            [JsonPropertyName("shard_block_id")]
            public string ShardBlockId { get; set; }

            /// <summary>
            /// <para> Notifies the app that the next block can't be fetched due to</para>
            /// <para> error.</para>
            /// <para> Processing will be continued after `network_resume_timeout`.</para>
            /// </summary>
            [JsonPropertyName("message_id")]
            public string MessageId { get; set; }

            /// <summary>
            /// <para> Notifies the app that the next block can't be fetched due to</para>
            /// <para> error.</para>
            /// <para> Processing will be continued after `network_resume_timeout`.</para>
            /// </summary>
            [JsonPropertyName("message")]
            public string Message { get; set; }

            /// <summary>
            /// <para> Notifies the app that the next block can't be fetched due to</para>
            /// <para> error.</para>
            /// <para> Processing will be continued after `network_resume_timeout`.</para>
            /// </summary>
            [JsonPropertyName("error")]
            public ClientError Error { get; set; }
        }

        /// <summary>
        /// <para> Notifies the app that the message was expired.</para>
        /// <para> Event occurs for contracts which ABI includes header "expire"</para>
        /// <para> Processing will be continued from encoding phase after</para>
        /// <para> `expiration_retries_timeout`.</para>
        /// </summary>
        public class MessageExpired : ProcessingEvent
        {
            /// <summary>
            /// <para> Notifies the app that the message was expired.</para>
            /// <para> Event occurs for contracts which ABI includes header "expire"</para>
            /// <para> Processing will be continued from encoding phase after</para>
            /// <para> `expiration_retries_timeout`.</para>
            /// </summary>
            [JsonPropertyName("message_id")]
            public string MessageId { get; set; }

            /// <summary>
            /// <para> Notifies the app that the message was expired.</para>
            /// <para> Event occurs for contracts which ABI includes header "expire"</para>
            /// <para> Processing will be continued from encoding phase after</para>
            /// <para> `expiration_retries_timeout`.</para>
            /// </summary>
            [JsonPropertyName("message")]
            public string Message { get; set; }

            /// <summary>
            /// <para> Notifies the app that the message was expired.</para>
            /// <para> Event occurs for contracts which ABI includes header "expire"</para>
            /// <para> Processing will be continued from encoding phase after</para>
            /// <para> `expiration_retries_timeout`.</para>
            /// </summary>
            [JsonPropertyName("error")]
            public ClientError Error { get; set; }
        }
    }
}