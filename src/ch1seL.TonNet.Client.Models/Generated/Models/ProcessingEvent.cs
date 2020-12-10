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
    public abstract class ProcessingEvent
    {
        /// <summary>
        /// <para>Notifies the app that the current shard block will be fetched from the network.</para>
        /// <para>Fetched block will be used later in waiting phase.</para>
        /// </summary>
        [JsonDiscriminator("WillFetchFirstBlock")]
        public class WillFetchFirstBlock : ProcessingEvent
        {
        }

        /// <summary>
        /// <para>Notifies the app that the client has failed to fetch current shard block.</para>
        /// <para>Message processing has finished.</para>
        /// </summary>
        [JsonDiscriminator("FetchFirstBlockFailed")]
        public class FetchFirstBlockFailed : ProcessingEvent
        {
            /// <summary>
            /// <para>Notifies the app that the client has failed to fetch current shard block.</para>
            /// <para>Message processing has finished.</para>
            /// </summary>
            [JsonPropertyName("error")]
            public ClientError Error { get; set; }
        }

        /// <summary>
        /// Notifies the app that the message will be sent to the network.
        /// </summary>
        [JsonDiscriminator("WillSend")]
        public class WillSend : ProcessingEvent
        {
            /// <summary>
            /// Notifies the app that the message will be sent to the network.
            /// </summary>
            [JsonPropertyName("shard_block_id")]
            public string ShardBlockId { get; set; }

            /// <summary>
            /// Notifies the app that the message will be sent to the network.
            /// </summary>
            [JsonPropertyName("message_id")]
            public string MessageId { get; set; }

            /// <summary>
            /// Notifies the app that the message will be sent to the network.
            /// </summary>
            [JsonPropertyName("message")]
            public string Message { get; set; }
        }

        /// <summary>
        /// Notifies the app that the message was sent to the network.
        /// </summary>
        [JsonDiscriminator("DidSend")]
        public class DidSend : ProcessingEvent
        {
            /// <summary>
            /// Notifies the app that the message was sent to the network.
            /// </summary>
            [JsonPropertyName("shard_block_id")]
            public string ShardBlockId { get; set; }

            /// <summary>
            /// Notifies the app that the message was sent to the network.
            /// </summary>
            [JsonPropertyName("message_id")]
            public string MessageId { get; set; }

            /// <summary>
            /// Notifies the app that the message was sent to the network.
            /// </summary>
            [JsonPropertyName("message")]
            public string Message { get; set; }
        }

        /// <summary>
        /// <para>Notifies the app that the sending operation was failed with network error.</para>
        /// <para>Nevertheless the processing will be continued at the waiting</para>
        /// <para>phase because the message possibly has been delivered to the</para>
        /// <para>node.</para>
        /// </summary>
        [JsonDiscriminator("SendFailed")]
        public class SendFailed : ProcessingEvent
        {
            /// <summary>
            /// <para>Notifies the app that the sending operation was failed with network error.</para>
            /// <para>Nevertheless the processing will be continued at the waiting</para>
            /// <para>phase because the message possibly has been delivered to the</para>
            /// <para>node.</para>
            /// </summary>
            [JsonPropertyName("shard_block_id")]
            public string ShardBlockId { get; set; }

            /// <summary>
            /// <para>Notifies the app that the sending operation was failed with network error.</para>
            /// <para>Nevertheless the processing will be continued at the waiting</para>
            /// <para>phase because the message possibly has been delivered to the</para>
            /// <para>node.</para>
            /// </summary>
            [JsonPropertyName("message_id")]
            public string MessageId { get; set; }

            /// <summary>
            /// <para>Notifies the app that the sending operation was failed with network error.</para>
            /// <para>Nevertheless the processing will be continued at the waiting</para>
            /// <para>phase because the message possibly has been delivered to the</para>
            /// <para>node.</para>
            /// </summary>
            [JsonPropertyName("message")]
            public string Message { get; set; }

            /// <summary>
            /// <para>Notifies the app that the sending operation was failed with network error.</para>
            /// <para>Nevertheless the processing will be continued at the waiting</para>
            /// <para>phase because the message possibly has been delivered to the</para>
            /// <para>node.</para>
            /// </summary>
            [JsonPropertyName("error")]
            public ClientError Error { get; set; }
        }

        /// <summary>
        /// <para>Notifies the app that the next shard block will be fetched from the network.</para>
        /// <para>Event can occurs more than one time due to block walking</para>
        /// <para>procedure.</para>
        /// </summary>
        [JsonDiscriminator("WillFetchNextBlock")]
        public class WillFetchNextBlock : ProcessingEvent
        {
            /// <summary>
            /// <para>Notifies the app that the next shard block will be fetched from the network.</para>
            /// <para>Event can occurs more than one time due to block walking</para>
            /// <para>procedure.</para>
            /// </summary>
            [JsonPropertyName("shard_block_id")]
            public string ShardBlockId { get; set; }

            /// <summary>
            /// <para>Notifies the app that the next shard block will be fetched from the network.</para>
            /// <para>Event can occurs more than one time due to block walking</para>
            /// <para>procedure.</para>
            /// </summary>
            [JsonPropertyName("message_id")]
            public string MessageId { get; set; }

            /// <summary>
            /// <para>Notifies the app that the next shard block will be fetched from the network.</para>
            /// <para>Event can occurs more than one time due to block walking</para>
            /// <para>procedure.</para>
            /// </summary>
            [JsonPropertyName("message")]
            public string Message { get; set; }
        }

        /// <summary>
        /// <para>Notifies the app that the next block can't be fetched due to error.</para>
        /// <para>Processing will be continued after `network_resume_timeout`.</para>
        /// </summary>
        [JsonDiscriminator("FetchNextBlockFailed")]
        public class FetchNextBlockFailed : ProcessingEvent
        {
            /// <summary>
            /// <para>Notifies the app that the next block can't be fetched due to error.</para>
            /// <para>Processing will be continued after `network_resume_timeout`.</para>
            /// </summary>
            [JsonPropertyName("shard_block_id")]
            public string ShardBlockId { get; set; }

            /// <summary>
            /// <para>Notifies the app that the next block can't be fetched due to error.</para>
            /// <para>Processing will be continued after `network_resume_timeout`.</para>
            /// </summary>
            [JsonPropertyName("message_id")]
            public string MessageId { get; set; }

            /// <summary>
            /// <para>Notifies the app that the next block can't be fetched due to error.</para>
            /// <para>Processing will be continued after `network_resume_timeout`.</para>
            /// </summary>
            [JsonPropertyName("message")]
            public string Message { get; set; }

            /// <summary>
            /// <para>Notifies the app that the next block can't be fetched due to error.</para>
            /// <para>Processing will be continued after `network_resume_timeout`.</para>
            /// </summary>
            [JsonPropertyName("error")]
            public ClientError Error { get; set; }
        }

        /// <summary>
        /// <para>Notifies the app that the message was expired.</para>
        /// <para>Event occurs for contracts which ABI includes header "expire"</para>
        /// <para>Processing will be continued from encoding phase after</para>
        /// <para>`expiration_retries_timeout`.</para>
        /// </summary>
        [JsonDiscriminator("MessageExpired")]
        public class MessageExpired : ProcessingEvent
        {
            /// <summary>
            /// <para>Notifies the app that the message was expired.</para>
            /// <para>Event occurs for contracts which ABI includes header "expire"</para>
            /// <para>Processing will be continued from encoding phase after</para>
            /// <para>`expiration_retries_timeout`.</para>
            /// </summary>
            [JsonPropertyName("message_id")]
            public string MessageId { get; set; }

            /// <summary>
            /// <para>Notifies the app that the message was expired.</para>
            /// <para>Event occurs for contracts which ABI includes header "expire"</para>
            /// <para>Processing will be continued from encoding phase after</para>
            /// <para>`expiration_retries_timeout`.</para>
            /// </summary>
            [JsonPropertyName("message")]
            public string Message { get; set; }

            /// <summary>
            /// <para>Notifies the app that the message was expired.</para>
            /// <para>Event occurs for contracts which ABI includes header "expire"</para>
            /// <para>Processing will be continued from encoding phase after</para>
            /// <para>`expiration_retries_timeout`.</para>
            /// </summary>
            [JsonPropertyName("error")]
            public ClientError Error { get; set; }
        }
    }
}