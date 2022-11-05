using System;
using System.Numerics;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace EverscaleNet.Client.Models
{
    /// <summary>
    /// <para>Not described yet..</para>
    /// </summary>
    public class ResultOfQueryTransactionTree
    {
        /// <summary>
        /// <para>Messages.</para>
        /// </summary>
        [JsonPropertyName("messages")]
        public MessageNode[] Messages { get; set; }

        /// <summary>
        /// <para>Transactions.</para>
        /// </summary>
        [JsonPropertyName("transactions")]
        public TransactionNode[] Transactions { get; set; }
    }
}