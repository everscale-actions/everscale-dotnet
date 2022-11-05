using System;
using System.Numerics;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace EverscaleNet.Client.Models
{
    /// <summary>
    /// <para>Not described yet..</para>
    /// </summary>
    public class MessageNode
    {
        /// <summary>
        /// <para>Message id.</para>
        /// </summary>
        [JsonPropertyName("id")]
        public string Id { get; set; }

        /// <summary>
        /// <para>Source transaction id.</para>
        /// <para>This field is missing for an external inbound messages.</para>
        /// </summary>
        [JsonPropertyName("src_transaction_id")]
        public string SrcTransactionId { get; set; }

        /// <summary>
        /// <para>Destination transaction id.</para>
        /// <para>This field is missing for an external outbound messages.</para>
        /// </summary>
        [JsonPropertyName("dst_transaction_id")]
        public string DstTransactionId { get; set; }

        /// <summary>
        /// <para>Source address.</para>
        /// </summary>
        [JsonPropertyName("src")]
        public string Src { get; set; }

        /// <summary>
        /// <para>Destination address.</para>
        /// </summary>
        [JsonPropertyName("dst")]
        public string Dst { get; set; }

        /// <summary>
        /// <para>Transferred tokens value.</para>
        /// </summary>
        [JsonPropertyName("value")]
        public string Value { get; set; }

        /// <summary>
        /// <para>Bounce flag.</para>
        /// </summary>
        [JsonPropertyName("bounce")]
        public bool Bounce { get; set; }

        /// <summary>
        /// <para>Decoded body.</para>
        /// <para>Library tries to decode message body using provided `params.abi_registry`.</para>
        /// <para>This field will be missing if none of the provided abi can be used to decode.</para>
        /// </summary>
        [JsonPropertyName("decoded_body")]
        public DecodedMessageBody DecodedBody { get; set; }
    }
}