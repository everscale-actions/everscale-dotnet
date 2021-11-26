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
        /// <para>Notifies the application that the account's current shard block will be fetched from the network. This step is performed before the message sending so that sdk knows starting from which block it will search for the transaction.</para>
        /// <para>Fetched block will be used later in waiting phase.</para>
        /// </summary>
        [JsonDiscriminator("WillFetchFirstBlock")]
        public class WillFetchFirstBlock : ProcessingEvent
        {
        }

        /// <summary>
        /// <para>Notifies the app that the client has failed to fetch the account's current shard block.</para>
        /// <para>This may happen due to the network issues. Receiving this event means that message processing will not proceed -</para>
        /// <para>message was not sent, and Developer can try to run `process_message` again,</para>
        /// <para>in the hope that the connection is restored.</para>
        /// </summary>
        [JsonDiscriminator("FetchFirstBlockFailed")]
        public class FetchFirstBlockFailed : ProcessingEvent
        {
            /// <summary>
            /// <para>Notifies the app that the client has failed to fetch the account's current shard block.</para>
            /// <para>This may happen due to the network issues. Receiving this event means that message processing will not proceed -</para>
            /// <para>message was not sent, and Developer can try to run `process_message` again,</para>
            /// <para>in the hope that the connection is restored.</para>
            /// </summary>
            [JsonPropertyName("error")]
            public ClientError Error { get; set; }
        }

        /// <summary>
        /// Notifies the app that the message will be sent to the network. This event means that the account's current shard block was successfully fetched and the message was successfully created (`abi.encode_message` function was executed successfully).
        /// </summary>
        [JsonDiscriminator("WillSend")]
        public class WillSend : ProcessingEvent
        {
            /// <summary>
            /// Notifies the app that the message will be sent to the network. This event means that the account's current shard block was successfully fetched and the message was successfully created (`abi.encode_message` function was executed successfully).
            /// </summary>
            [JsonPropertyName("shard_block_id")]
            public string ShardBlockId { get; set; }

            /// <summary>
            /// Notifies the app that the message will be sent to the network. This event means that the account's current shard block was successfully fetched and the message was successfully created (`abi.encode_message` function was executed successfully).
            /// </summary>
            [JsonPropertyName("message_id")]
            public string MessageId { get; set; }

            /// <summary>
            /// Notifies the app that the message will be sent to the network. This event means that the account's current shard block was successfully fetched and the message was successfully created (`abi.encode_message` function was executed successfully).
            /// </summary>
            [JsonPropertyName("message")]
            public string Message { get; set; }
        }

        /// <summary>
        /// <para>Notifies the app that the message was sent to the network, i.e `processing.send_message` was successfuly executed. Now, the message is in the blockchain. If Application exits at this phase, Developer needs to proceed with processing after the application is restored with `wait_for_transaction` function, passing shard_block_id and message from this event.</para>
        /// <para>Do not forget to specify abi of your contract as well, it is crucial for proccessing. See `processing.wait_for_transaction` documentation.</para>
        /// </summary>
        [JsonDiscriminator("DidSend")]
        public class DidSend : ProcessingEvent
        {
            /// <summary>
            /// <para>Notifies the app that the message was sent to the network, i.e `processing.send_message` was successfuly executed. Now, the message is in the blockchain. If Application exits at this phase, Developer needs to proceed with processing after the application is restored with `wait_for_transaction` function, passing shard_block_id and message from this event.</para>
            /// <para>Do not forget to specify abi of your contract as well, it is crucial for proccessing. See `processing.wait_for_transaction` documentation.</para>
            /// </summary>
            [JsonPropertyName("shard_block_id")]
            public string ShardBlockId { get; set; }

            /// <summary>
            /// <para>Notifies the app that the message was sent to the network, i.e `processing.send_message` was successfuly executed. Now, the message is in the blockchain. If Application exits at this phase, Developer needs to proceed with processing after the application is restored with `wait_for_transaction` function, passing shard_block_id and message from this event.</para>
            /// <para>Do not forget to specify abi of your contract as well, it is crucial for proccessing. See `processing.wait_for_transaction` documentation.</para>
            /// </summary>
            [JsonPropertyName("message_id")]
            public string MessageId { get; set; }

            /// <summary>
            /// <para>Notifies the app that the message was sent to the network, i.e `processing.send_message` was successfuly executed. Now, the message is in the blockchain. If Application exits at this phase, Developer needs to proceed with processing after the application is restored with `wait_for_transaction` function, passing shard_block_id and message from this event.</para>
            /// <para>Do not forget to specify abi of your contract as well, it is crucial for proccessing. See `processing.wait_for_transaction` documentation.</para>
            /// </summary>
            [JsonPropertyName("message")]
            public string Message { get; set; }
        }

        /// <summary>
        /// <para>Notifies the app that the sending operation was failed with network error.</para>
        /// <para>Nevertheless the processing will be continued at the waiting</para>
        /// <para>phase because the message possibly has been delivered to the</para>
        /// <para>node.</para>
        /// <para>If Application exits at this phase, Developer needs to proceed with processing</para>
        /// <para>after the application is restored with `wait_for_transaction` function, passing</para>
        /// <para>shard_block_id and message from this event. Do not forget to specify abi of your contract</para>
        /// <para>as well, it is crucial for proccessing. See `processing.wait_for_transaction` documentation.</para>
        /// </summary>
        [JsonDiscriminator("SendFailed")]
        public class SendFailed : ProcessingEvent
        {
            /// <summary>
            /// <para>Notifies the app that the sending operation was failed with network error.</para>
            /// <para>Nevertheless the processing will be continued at the waiting</para>
            /// <para>phase because the message possibly has been delivered to the</para>
            /// <para>node.</para>
            /// <para>If Application exits at this phase, Developer needs to proceed with processing</para>
            /// <para>after the application is restored with `wait_for_transaction` function, passing</para>
            /// <para>shard_block_id and message from this event. Do not forget to specify abi of your contract</para>
            /// <para>as well, it is crucial for proccessing. See `processing.wait_for_transaction` documentation.</para>
            /// </summary>
            [JsonPropertyName("shard_block_id")]
            public string ShardBlockId { get; set; }

            /// <summary>
            /// <para>Notifies the app that the sending operation was failed with network error.</para>
            /// <para>Nevertheless the processing will be continued at the waiting</para>
            /// <para>phase because the message possibly has been delivered to the</para>
            /// <para>node.</para>
            /// <para>If Application exits at this phase, Developer needs to proceed with processing</para>
            /// <para>after the application is restored with `wait_for_transaction` function, passing</para>
            /// <para>shard_block_id and message from this event. Do not forget to specify abi of your contract</para>
            /// <para>as well, it is crucial for proccessing. See `processing.wait_for_transaction` documentation.</para>
            /// </summary>
            [JsonPropertyName("message_id")]
            public string MessageId { get; set; }

            /// <summary>
            /// <para>Notifies the app that the sending operation was failed with network error.</para>
            /// <para>Nevertheless the processing will be continued at the waiting</para>
            /// <para>phase because the message possibly has been delivered to the</para>
            /// <para>node.</para>
            /// <para>If Application exits at this phase, Developer needs to proceed with processing</para>
            /// <para>after the application is restored with `wait_for_transaction` function, passing</para>
            /// <para>shard_block_id and message from this event. Do not forget to specify abi of your contract</para>
            /// <para>as well, it is crucial for proccessing. See `processing.wait_for_transaction` documentation.</para>
            /// </summary>
            [JsonPropertyName("message")]
            public string Message { get; set; }

            /// <summary>
            /// <para>Notifies the app that the sending operation was failed with network error.</para>
            /// <para>Nevertheless the processing will be continued at the waiting</para>
            /// <para>phase because the message possibly has been delivered to the</para>
            /// <para>node.</para>
            /// <para>If Application exits at this phase, Developer needs to proceed with processing</para>
            /// <para>after the application is restored with `wait_for_transaction` function, passing</para>
            /// <para>shard_block_id and message from this event. Do not forget to specify abi of your contract</para>
            /// <para>as well, it is crucial for proccessing. See `processing.wait_for_transaction` documentation.</para>
            /// </summary>
            [JsonPropertyName("error")]
            public ClientError Error { get; set; }
        }

        /// <summary>
        /// <para>Notifies the app that the next shard block will be fetched from the network.</para>
        /// <para>Event can occurs more than one time due to block walking</para>
        /// <para>procedure.</para>
        /// <para>If Application exits at this phase, Developer needs to proceed with processing</para>
        /// <para>after the application is restored with `wait_for_transaction` function, passing</para>
        /// <para>shard_block_id and message from this event. Do not forget to specify abi of your contract</para>
        /// <para>as well, it is crucial for proccessing. See `processing.wait_for_transaction` documentation.</para>
        /// </summary>
        [JsonDiscriminator("WillFetchNextBlock")]
        public class WillFetchNextBlock : ProcessingEvent
        {
            /// <summary>
            /// <para>Notifies the app that the next shard block will be fetched from the network.</para>
            /// <para>Event can occurs more than one time due to block walking</para>
            /// <para>procedure.</para>
            /// <para>If Application exits at this phase, Developer needs to proceed with processing</para>
            /// <para>after the application is restored with `wait_for_transaction` function, passing</para>
            /// <para>shard_block_id and message from this event. Do not forget to specify abi of your contract</para>
            /// <para>as well, it is crucial for proccessing. See `processing.wait_for_transaction` documentation.</para>
            /// </summary>
            [JsonPropertyName("shard_block_id")]
            public string ShardBlockId { get; set; }

            /// <summary>
            /// <para>Notifies the app that the next shard block will be fetched from the network.</para>
            /// <para>Event can occurs more than one time due to block walking</para>
            /// <para>procedure.</para>
            /// <para>If Application exits at this phase, Developer needs to proceed with processing</para>
            /// <para>after the application is restored with `wait_for_transaction` function, passing</para>
            /// <para>shard_block_id and message from this event. Do not forget to specify abi of your contract</para>
            /// <para>as well, it is crucial for proccessing. See `processing.wait_for_transaction` documentation.</para>
            /// </summary>
            [JsonPropertyName("message_id")]
            public string MessageId { get; set; }

            /// <summary>
            /// <para>Notifies the app that the next shard block will be fetched from the network.</para>
            /// <para>Event can occurs more than one time due to block walking</para>
            /// <para>procedure.</para>
            /// <para>If Application exits at this phase, Developer needs to proceed with processing</para>
            /// <para>after the application is restored with `wait_for_transaction` function, passing</para>
            /// <para>shard_block_id and message from this event. Do not forget to specify abi of your contract</para>
            /// <para>as well, it is crucial for proccessing. See `processing.wait_for_transaction` documentation.</para>
            /// </summary>
            [JsonPropertyName("message")]
            public string Message { get; set; }
        }

        /// <summary>
        /// <para>Notifies the app that the next block can't be fetched.</para>
        /// <para>If no block was fetched within `NetworkConfig.wait_for_timeout` then processing stops.</para>
        /// <para>This may happen when the shard stops, or there are other network issues.</para>
        /// <para>In this case Developer should resume message processing with `wait_for_transaction`, passing shard_block_id,</para>
        /// <para>message and contract abi to it. Note that passing ABI is crucial, because it will influence the processing strategy.</para>
        /// <para>Another way to tune this is to specify long timeout in `NetworkConfig.wait_for_timeout`</para>
        /// </summary>
        [JsonDiscriminator("FetchNextBlockFailed")]
        public class FetchNextBlockFailed : ProcessingEvent
        {
            /// <summary>
            /// <para>Notifies the app that the next block can't be fetched.</para>
            /// <para>If no block was fetched within `NetworkConfig.wait_for_timeout` then processing stops.</para>
            /// <para>This may happen when the shard stops, or there are other network issues.</para>
            /// <para>In this case Developer should resume message processing with `wait_for_transaction`, passing shard_block_id,</para>
            /// <para>message and contract abi to it. Note that passing ABI is crucial, because it will influence the processing strategy.</para>
            /// <para>Another way to tune this is to specify long timeout in `NetworkConfig.wait_for_timeout`</para>
            /// </summary>
            [JsonPropertyName("shard_block_id")]
            public string ShardBlockId { get; set; }

            /// <summary>
            /// <para>Notifies the app that the next block can't be fetched.</para>
            /// <para>If no block was fetched within `NetworkConfig.wait_for_timeout` then processing stops.</para>
            /// <para>This may happen when the shard stops, or there are other network issues.</para>
            /// <para>In this case Developer should resume message processing with `wait_for_transaction`, passing shard_block_id,</para>
            /// <para>message and contract abi to it. Note that passing ABI is crucial, because it will influence the processing strategy.</para>
            /// <para>Another way to tune this is to specify long timeout in `NetworkConfig.wait_for_timeout`</para>
            /// </summary>
            [JsonPropertyName("message_id")]
            public string MessageId { get; set; }

            /// <summary>
            /// <para>Notifies the app that the next block can't be fetched.</para>
            /// <para>If no block was fetched within `NetworkConfig.wait_for_timeout` then processing stops.</para>
            /// <para>This may happen when the shard stops, or there are other network issues.</para>
            /// <para>In this case Developer should resume message processing with `wait_for_transaction`, passing shard_block_id,</para>
            /// <para>message and contract abi to it. Note that passing ABI is crucial, because it will influence the processing strategy.</para>
            /// <para>Another way to tune this is to specify long timeout in `NetworkConfig.wait_for_timeout`</para>
            /// </summary>
            [JsonPropertyName("message")]
            public string Message { get; set; }

            /// <summary>
            /// <para>Notifies the app that the next block can't be fetched.</para>
            /// <para>If no block was fetched within `NetworkConfig.wait_for_timeout` then processing stops.</para>
            /// <para>This may happen when the shard stops, or there are other network issues.</para>
            /// <para>In this case Developer should resume message processing with `wait_for_transaction`, passing shard_block_id,</para>
            /// <para>message and contract abi to it. Note that passing ABI is crucial, because it will influence the processing strategy.</para>
            /// <para>Another way to tune this is to specify long timeout in `NetworkConfig.wait_for_timeout`</para>
            /// </summary>
            [JsonPropertyName("error")]
            public ClientError Error { get; set; }
        }

        /// <summary>
        /// <para>Notifies the app that the message was not executed within expire timeout on-chain and will never be because it is already expired. The expiration timeout can be configured with `AbiConfig` parameters.</para>
        /// <para>This event occurs only for the contracts which ABI includes "expire" header.</para>
        /// <para>If Application specifies `NetworkConfig.message_retries_count` &gt; 0, then `process_message`</para>
        /// <para>will perform retries: will create a new message and send it again and repeat it untill it reaches</para>
        /// <para>the maximum retries count or receives a successful result.  All the processing</para>
        /// <para>events will be repeated.</para>
        /// </summary>
        [JsonDiscriminator("MessageExpired")]
        public class MessageExpired : ProcessingEvent
        {
            /// <summary>
            /// <para>Notifies the app that the message was not executed within expire timeout on-chain and will never be because it is already expired. The expiration timeout can be configured with `AbiConfig` parameters.</para>
            /// <para>This event occurs only for the contracts which ABI includes "expire" header.</para>
            /// <para>If Application specifies `NetworkConfig.message_retries_count` &gt; 0, then `process_message`</para>
            /// <para>will perform retries: will create a new message and send it again and repeat it untill it reaches</para>
            /// <para>the maximum retries count or receives a successful result.  All the processing</para>
            /// <para>events will be repeated.</para>
            /// </summary>
            [JsonPropertyName("message_id")]
            public string MessageId { get; set; }

            /// <summary>
            /// <para>Notifies the app that the message was not executed within expire timeout on-chain and will never be because it is already expired. The expiration timeout can be configured with `AbiConfig` parameters.</para>
            /// <para>This event occurs only for the contracts which ABI includes "expire" header.</para>
            /// <para>If Application specifies `NetworkConfig.message_retries_count` &gt; 0, then `process_message`</para>
            /// <para>will perform retries: will create a new message and send it again and repeat it untill it reaches</para>
            /// <para>the maximum retries count or receives a successful result.  All the processing</para>
            /// <para>events will be repeated.</para>
            /// </summary>
            [JsonPropertyName("message")]
            public string Message { get; set; }

            /// <summary>
            /// <para>Notifies the app that the message was not executed within expire timeout on-chain and will never be because it is already expired. The expiration timeout can be configured with `AbiConfig` parameters.</para>
            /// <para>This event occurs only for the contracts which ABI includes "expire" header.</para>
            /// <para>If Application specifies `NetworkConfig.message_retries_count` &gt; 0, then `process_message`</para>
            /// <para>will perform retries: will create a new message and send it again and repeat it untill it reaches</para>
            /// <para>the maximum retries count or receives a successful result.  All the processing</para>
            /// <para>events will be repeated.</para>
            /// </summary>
            [JsonPropertyName("error")]
            public ClientError Error { get; set; }
        }
    }
}