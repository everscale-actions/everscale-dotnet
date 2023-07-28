using System;
using System.Numerics;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace EverscaleNet.Client.Models
{
    /// <summary>
    /// <para>[UNSTABLE](UNSTABLE.md) [DEPRECATED](DEPRECATED.md) Structure for storing debot handle returned from `init` function.</para>
    /// </summary>
    public class RegisteredDebot
    {
        /// <summary>
        /// <para>Debot handle which references an instance of debot engine.</para>
        /// </summary>
        [JsonPropertyName("debot_handle")]
        public uint DebotHandle { get; set; }

        /// <summary>
        /// <para>Debot abi as json string.</para>
        /// </summary>
        [JsonPropertyName("debot_abi")]
        public string DebotAbi { get; set; }

        /// <summary>
        /// <para>Debot metadata.</para>
        /// </summary>
        [JsonPropertyName("info")]
        public DebotInfo Info { get; set; }
    }
}