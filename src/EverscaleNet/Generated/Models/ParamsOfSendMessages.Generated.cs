using System;
using System.Numerics;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace EverscaleNet.Client.Models
{
    /// <summary>
    /// <para>Not described yet..</para>
    /// </summary>
    public class ParamsOfSendMessages
    {
        /// <summary>
        /// <para>Messages that must be sent to the blockchain.</para>
        /// </summary>
        [JsonPropertyName("messages")]
        public MessageSendingParams[] Messages { get; set; }

        /// <summary>
        /// <para>Optional message monitor queue that starts monitoring for the processing results for sent messages.</para>
        /// </summary>
        [JsonPropertyName("monitor_queue")]
        public string MonitorQueue { get; set; }
    }
}