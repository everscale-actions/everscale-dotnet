using System;
using System.Numerics;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace EverscaleNet.Client.Models
{
    /// <summary>
    /// <para>Not described yet..</para>
    /// </summary>
    public class MessageMonitoringTransaction
    {
        /// <summary>
        /// <para>Hash of the transaction. Present if transaction was included into the blocks. When then transaction was emulated this field will be missing.</para>
        /// </summary>
        [JsonPropertyName("hash")]
        public string Hash { get; set; }

        /// <summary>
        /// <para>Aborted field of the transaction.</para>
        /// </summary>
        [JsonPropertyName("aborted")]
        public bool Aborted { get; set; }

        /// <summary>
        /// <para>Optional information about the compute phase of the transaction.</para>
        /// </summary>
        [JsonPropertyName("compute")]
        public MessageMonitoringTransactionCompute Compute { get; set; }
    }
}