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
    public class TransactionNode
    {
        /// <summary>
        /// Transaction id.
        /// </summary>
        [JsonPropertyName("id")]
        public string Id { get; set; }

        /// <summary>
        /// In message id.
        /// </summary>
        [JsonPropertyName("in_msg")]
        public string InMsg { get; set; }

        /// <summary>
        /// Out message ids.
        /// </summary>
        [JsonPropertyName("out_msgs")]
        public string[] OutMsgs { get; set; }

        /// <summary>
        /// Account address.
        /// </summary>
        [JsonPropertyName("account_addr")]
        public string AccountAddr { get; set; }

        /// <summary>
        /// Transactions total fees.
        /// </summary>
        [JsonPropertyName("total_fees")]
        public string TotalFees { get; set; }

        /// <summary>
        /// Aborted flag.
        /// </summary>
        [JsonPropertyName("aborted")]
        public bool Aborted { get; set; }

        /// <summary>
        /// Compute phase exit code.
        /// </summary>
        [JsonPropertyName("exit_code")]
        public uint? ExitCode { get; set; }
    }
}