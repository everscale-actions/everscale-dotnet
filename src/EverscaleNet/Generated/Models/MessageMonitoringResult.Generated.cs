using System;
using System.Numerics;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace EverscaleNet.Client.Models
{
    /// <summary>
    /// <para>Not described yet..</para>
    /// </summary>
    public class MessageMonitoringResult
    {
        /// <summary>
        /// <para>Hash of the message.</para>
        /// </summary>
        [JsonPropertyName("hash")]
        public string Hash { get; set; }

        /// <summary>
        /// <para>Processing status.</para>
        /// </summary>
        [JsonPropertyName("status")]
        public MessageMonitoringStatus? Status { get; set; }

        /// <summary>
        /// <para>In case of `Finalized` the transaction is extracted from the block. In case of `Timeout` the transaction is emulated using the last known account state.</para>
        /// </summary>
        [JsonPropertyName("transaction")]
        public MessageMonitoringTransaction Transaction { get; set; }

        /// <summary>
        /// <para>In case of `Timeout` contains possible error reason.</para>
        /// </summary>
        [JsonPropertyName("error")]
        public string Error { get; set; }

        /// <summary>
        /// <para>User defined data related to this message. This is the same value as passed before with `MessageMonitoringParams` or `SendMessageParams`.</para>
        /// </summary>
        [JsonPropertyName("user_data")]
        public JsonElement? UserData { get; set; }
    }
}