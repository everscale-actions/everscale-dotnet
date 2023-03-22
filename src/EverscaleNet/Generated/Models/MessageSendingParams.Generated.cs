using System;
using System.Numerics;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace EverscaleNet.Client.Models
{
    /// <summary>
    /// <para>Not described yet..</para>
    /// </summary>
    public class MessageSendingParams
    {
        /// <summary>
        /// <para>BOC of the message, that must be sent to the blockchain.</para>
        /// </summary>
        [JsonPropertyName("boc")]
        public string Boc { get; set; }

        /// <summary>
        /// <para>Expiration time of the message. Must be specified as a UNIX timestamp in seconds.</para>
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