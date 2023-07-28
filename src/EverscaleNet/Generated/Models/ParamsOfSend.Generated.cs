using System;
using System.Numerics;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace EverscaleNet.Client.Models
{
    /// <summary>
    /// <para>[UNSTABLE](UNSTABLE.md) [DEPRECATED](DEPRECATED.md) Parameters of `send` function.</para>
    /// </summary>
    public class ParamsOfSend
    {
        /// <summary>
        /// <para>Debot handle which references an instance of debot engine.</para>
        /// </summary>
        [JsonPropertyName("debot_handle")]
        public uint DebotHandle { get; set; }

        /// <summary>
        /// <para>BOC of internal message to debot encoded in base64 format.</para>
        /// </summary>
        [JsonPropertyName("message")]
        public string Message { get; set; }
    }
}