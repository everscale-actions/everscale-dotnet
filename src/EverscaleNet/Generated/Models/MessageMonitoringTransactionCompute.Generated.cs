using System;
using System.Numerics;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace EverscaleNet.Client.Models
{
    /// <summary>
    /// <para>Not described yet..</para>
    /// </summary>
    public class MessageMonitoringTransactionCompute
    {
        /// <summary>
        /// <para>Compute phase exit code.</para>
        /// </summary>
        [JsonPropertyName("exit_code")]
        public int ExitCode { get; set; }
    }
}