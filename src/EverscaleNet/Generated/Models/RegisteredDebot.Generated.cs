using Dahomey.Json.Attributes;
using System;
using System.Numerics;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace EverscaleNet.Client.Models
{
    /// <summary>
    /// [UNSTABLE](UNSTABLE.md) Structure for storing debot handle returned from `init` function.
    /// </summary>
    public class RegisteredDebot
    {
        /// <summary>
        /// Debot handle which references an instance of debot engine.
        /// </summary>
        [JsonPropertyName("debot_handle")]
        public uint DebotHandle { get; set; }

        /// <summary>
        /// Debot abi as json string.
        /// </summary>
        [JsonPropertyName("debot_abi")]
        public string DebotAbi { get; set; }

        /// <summary>
        /// Debot metadata.
        /// </summary>
        [JsonPropertyName("info")]
        public DebotInfo Info { get; set; }
    }
}