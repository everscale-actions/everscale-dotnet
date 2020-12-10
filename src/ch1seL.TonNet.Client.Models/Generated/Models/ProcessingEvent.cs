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
        /// Fetched block will be used later in waiting phase.
        /// </summary>
        [JsonDiscriminator("WillFetchFirstBlock")]
        public class WillFetchFirstBlock : ProcessingEvent
        {
        }

        /// <summary>
        /// Message processing has finished.
        /// </summary>
        [JsonDiscriminator("FetchFirstBlockFailed")]
        public class FetchFirstBlockFailed : ProcessingEvent
        {
            /// <summary>
            /// Message processing has finished.
            /// </summary>
            [JsonPropertyName("error")]
            public ClientError Error { get; set; }
        }

        /// <summary>
        /// Not described yet..
        /// </summary>
        [JsonDiscriminator("WillSend")]
        public class WillSend : ProcessingEvent
        {
            /// <summary>
            /// Not described yet..
            /// </summary>
            [JsonPropertyName("shard_block_id")]
            public string ShardBlockId { get; set; }

            /// <summary>
            /// Not described yet..
            /// </summary>
            [JsonPropertyName("message_id")]
            public string MessageId { get; set; }

            /// <summary>
            /// Not described yet..
            /// </summary>
            [JsonPropertyName("message")]
            public string Message { get; set; }
        }

        /// <summary>
        /// Not described yet..
        /// </summary>
        [JsonDiscriminator("DidSend")]
        public class DidSend : ProcessingEvent
        {
            /// <summary>
            /// Not described yet..
            /// </summary>
            [JsonPropertyName("shard_block_id")]
            public string ShardBlockId { get; set; }

            /// <summary>
            /// Not described yet..
            /// </summary>
            [JsonPropertyName("message_id")]
            public string MessageId { get; set; }

            /// <summary>
            /// Not described yet..
            /// </summary>
            [JsonPropertyName("message")]
            public string Message { get; set; }
        }

        /// <summary>
        /// <para>Nevertheless the processing will be continued at the waiting</para>
        /// <para>phase because the message possibly has been delivered to the</para>
        /// <para>node.</para>
        /// </summary>
        [JsonDiscriminator("SendFailed")]
        public class SendFailed : ProcessingEvent
        {
            /// <summary>
            /// <para>Nevertheless the processing will be continued at the waiting</para>
            /// <para>phase because the message possibly has been delivered to the</para>
            /// <para>node.</para>
            /// </summary>
            [JsonPropertyName("shard_block_id")]
            public string ShardBlockId { get; set; }

            /// <summary>
            /// <para>Nevertheless the processing will be continued at the waiting</para>
            /// <para>phase because the message possibly has been delivered to the</para>
            /// <para>node.</para>
            /// </summary>
            [JsonPropertyName("message_id")]
            public string MessageId { get; set; }

            /// <summary>
            /// <para>Nevertheless the processing will be continued at the waiting</para>
            /// <para>phase because the message possibly has been delivered to the</para>
            /// <para>node.</para>
            /// </summary>
            [JsonPropertyName("message")]
            public string Message { get; set; }

            /// <summary>
            /// <para>Nevertheless the processing will be continued at the waiting</para>
            /// <para>phase because the message possibly has been delivered to the</para>
            /// <para>node.</para>
            /// </summary>
            [JsonPropertyName("error")]
            public ClientError Error { get; set; }
        }

        /// <summary>
        /// <para>Event can occurs more than one time due to block walking</para>
        /// <para>procedure.</para>
        /// </summary>
        [JsonDiscriminator("WillFetchNextBlock")]
        public class WillFetchNextBlock : ProcessingEvent
        {
            /// <summary>
            /// <para>Event can occurs more than one time due to block walking</para>
            /// <para>procedure.</para>
            /// </summary>
            [JsonPropertyName("shard_block_id")]
            public string ShardBlockId { get; set; }

            /// <summary>
            /// <para>Event can occurs more than one time due to block walking</para>
            /// <para>procedure.</para>
            /// </summary>
            [JsonPropertyName("message_id")]
            public string MessageId { get; set; }

            /// <summary>
            /// <para>Event can occurs more than one time due to block walking</para>
            /// <para>procedure.</para>
            /// </summary>
            [JsonPropertyName("message")]
            public string Message { get; set; }
        }

        /// <summary>
        /// Processing will be continued after `network_resume_timeout`.
        /// </summary>
        [JsonDiscriminator("FetchNextBlockFailed")]
        public class FetchNextBlockFailed : ProcessingEvent
        {
            /// <summary>
            /// Processing will be continued after `network_resume_timeout`.
            /// </summary>
            [JsonPropertyName("shard_block_id")]
            public string ShardBlockId { get; set; }

            /// <summary>
            /// Processing will be continued after `network_resume_timeout`.
            /// </summary>
            [JsonPropertyName("message_id")]
            public string MessageId { get; set; }

            /// <summary>
            /// Processing will be continued after `network_resume_timeout`.
            /// </summary>
            [JsonPropertyName("message")]
            public string Message { get; set; }

            /// <summary>
            /// Processing will be continued after `network_resume_timeout`.
            /// </summary>
            [JsonPropertyName("error")]
            public ClientError Error { get; set; }
        }

        /// <summary>
        /// <para>Event occurs for contracts which ABI includes header "expire"</para>
        /// <para>Processing will be continued from encoding phase after</para>
        /// <para>`expiration_retries_timeout`.</para>
        /// </summary>
        [JsonDiscriminator("MessageExpired")]
        public class MessageExpired : ProcessingEvent
        {
            /// <summary>
            /// <para>Event occurs for contracts which ABI includes header "expire"</para>
            /// <para>Processing will be continued from encoding phase after</para>
            /// <para>`expiration_retries_timeout`.</para>
            /// </summary>
            [JsonPropertyName("message_id")]
            public string MessageId { get; set; }

            /// <summary>
            /// <para>Event occurs for contracts which ABI includes header "expire"</para>
            /// <para>Processing will be continued from encoding phase after</para>
            /// <para>`expiration_retries_timeout`.</para>
            /// </summary>
            [JsonPropertyName("message")]
            public string Message { get; set; }

            /// <summary>
            /// <para>Event occurs for contracts which ABI includes header "expire"</para>
            /// <para>Processing will be continued from encoding phase after</para>
            /// <para>`expiration_retries_timeout`.</para>
            /// </summary>
            [JsonPropertyName("error")]
            public ClientError Error { get; set; }
        }
    }
}