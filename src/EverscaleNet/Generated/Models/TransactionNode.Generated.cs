using System;
using System.Numerics;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace EverscaleNet.Client.Models
{
    /// <summary>
    /// <para>Not described yet..</para>
    /// </summary>
    public class TransactionNode
    {
        /// <summary>
        /// <para>Transaction id.</para>
        /// </summary>
        [JsonPropertyName("id")]
        public string Id { get; set; }

        /// <summary>
        /// <para>In message id.</para>
        /// </summary>
        [JsonPropertyName("in_msg")]
        public string InMsg { get; set; }

        /// <summary>
        /// <para>Out message ids.</para>
        /// </summary>
        [JsonPropertyName("out_msgs")]
        public string[] OutMsgs { get; set; }

        /// <summary>
        /// <para>Account address.</para>
        /// </summary>
        [JsonPropertyName("account_addr")]
        public string AccountAddr { get; set; }

        /// <summary>
        /// <para>Transactions total fees.</para>
        /// </summary>
        [JsonPropertyName("total_fees")]
        public string TotalFees { get; set; }

        /// <summary>
        /// <para>Aborted flag.</para>
        /// </summary>
        [JsonPropertyName("aborted")]
        public bool Aborted { get; set; }

        /// <summary>
        /// <para>Compute phase exit code.</para>
        /// </summary>
        [JsonPropertyName("exit_code")]
        public uint? ExitCode { get; set; }
    }
}