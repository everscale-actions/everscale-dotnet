using System;
using System.Numerics;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace EverscaleNet.Client.Models
{
    /// <summary>
    /// <para>Not described yet..</para>
    /// </summary>
    public class ResultOfSendMessages
    {
        /// <summary>
        /// <para>Messages that was sent to the blockchain for execution.</para>
        /// </summary>
        [JsonPropertyName("messages")]
        public MessageMonitoringParams[] Messages { get; set; }
    }
}