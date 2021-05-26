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
    public class ResultOfQueryTransactionTree
    {
        /// <summary>
        /// Messages.
        /// </summary>
        [JsonPropertyName("messages")]
        public MessageNode[] Messages { get; set; }

        /// <summary>
        /// Transactions.
        /// </summary>
        [JsonPropertyName("transactions")]
        public TransactionNode[] Transactions { get; set; }
    }
}