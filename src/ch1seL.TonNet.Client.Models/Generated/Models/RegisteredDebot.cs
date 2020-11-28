using Dahomey.Json.Attributes;
using System;
using System.Numerics;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace ch1seL.TonNet.Client.Models
{
    /// <summary>
    ///  [UNSTABLE](UNSTABLE.md) Structure for storing debot handle returned from `start` and `fetch` functions.
    /// </summary>
    public class RegisteredDebot
    {
        /// <summary>
        ///  Debot handle which references an instance of debot engine.
        /// </summary>
        [JsonPropertyName("debot_handle")]
        public uint DebotHandle { get; set; }
    }
}