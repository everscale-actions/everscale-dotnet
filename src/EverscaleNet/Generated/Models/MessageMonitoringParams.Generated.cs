using System;
using System.Numerics;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace EverscaleNet.Client.Models
{
    /// <summary>
    /// <para>Not described yet..</para>
    /// </summary>
    public class MessageMonitoringParams
    {
        /// <summary>
        /// <para>Monitored message identification. Can be provided as a message's BOC or (hash, address) pair. BOC is a preferable way because it helps to determine possible error reason (using TVM execution of the message).</para>
        /// </summary>
        [JsonPropertyName("message")]
        public MonitoredMessage Message { get; set; }

        /// <summary>
        /// <para>Block time Must be specified as a UNIX timestamp in seconds</para>
        /// </summary>
        [JsonPropertyName("wait_until")]
        public uint WaitUntil { get; set; }

        /// <summary>
        /// <para>User defined data associated with this message. Helps to identify this message when user received `MessageMonitoringResult`.</para>
        /// </summary>
        [JsonPropertyName("user_data")]
        public JsonElement? UserData { get; set; }
    }
}