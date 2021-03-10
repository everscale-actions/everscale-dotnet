using Dahomey.Json.Attributes;
using System;
using System.Numerics;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace ch1seL.TonNet.Client.Models
{
    /// <summary>
    /// [UNSTABLE](UNSTABLE.md) Parameters of `send` function.
    /// </summary>
    public class ParamsOfSend
    {
        /// <summary>
        /// Debot handle which references an instance of debot engine.
        /// </summary>
        [JsonPropertyName("debot_handle")]
        public uint DebotHandle { get; set; }

        /// <summary>
        /// BOC of internal message to debot encoded in base64 format.
        /// </summary>
        [JsonPropertyName("message")]
        public string Message { get; set; }
    }
}