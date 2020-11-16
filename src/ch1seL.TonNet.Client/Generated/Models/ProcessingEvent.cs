using System;
using System.Numerics;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace ch1seL.TonNet.Client.Models
{
    public abstract class ProcessingEvent
    {
        public class WillFetchFirstBlock : ProcessingEvent
        {
        }

        public class FetchFirstBlockFailed : ProcessingEvent
        {
            [JsonPropertyName("error")]
            public ClientError Error { get; set; }
        }

        public class WillSend : ProcessingEvent
        {
            [JsonPropertyName("shard_block_id")]
            public string ShardBlockId { get; set; }
            [JsonPropertyName("message_id")]
            public string MessageId { get; set; }
            [JsonPropertyName("message")]
            public string Message { get; set; }
        }

        public class DidSend : ProcessingEvent
        {
            [JsonPropertyName("shard_block_id")]
            public string ShardBlockId { get; set; }
            [JsonPropertyName("message_id")]
            public string MessageId { get; set; }
            [JsonPropertyName("message")]
            public string Message { get; set; }
        }

        public class SendFailed : ProcessingEvent
        {
            [JsonPropertyName("shard_block_id")]
            public string ShardBlockId { get; set; }
            [JsonPropertyName("message_id")]
            public string MessageId { get; set; }
            [JsonPropertyName("message")]
            public string Message { get; set; }
            [JsonPropertyName("error")]
            public ClientError Error { get; set; }
        }

        public class WillFetchNextBlock : ProcessingEvent
        {
            [JsonPropertyName("shard_block_id")]
            public string ShardBlockId { get; set; }
            [JsonPropertyName("message_id")]
            public string MessageId { get; set; }
            [JsonPropertyName("message")]
            public string Message { get; set; }
        }

        public class FetchNextBlockFailed : ProcessingEvent
        {
            [JsonPropertyName("shard_block_id")]
            public string ShardBlockId { get; set; }
            [JsonPropertyName("message_id")]
            public string MessageId { get; set; }
            [JsonPropertyName("message")]
            public string Message { get; set; }
            [JsonPropertyName("error")]
            public ClientError Error { get; set; }
        }

        public class MessageExpired : ProcessingEvent
        {
            [JsonPropertyName("message_id")]
            public string MessageId { get; set; }
            [JsonPropertyName("message")]
            public string Message { get; set; }
            [JsonPropertyName("error")]
            public ClientError Error { get; set; }
        }
    }
}