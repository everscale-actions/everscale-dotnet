using Dahomey.Json.Attributes;
using System;
using System.Numerics;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace ch1seL.TonNet.Client.Models
{
    /// <summary>
    /// [UNSTABLE](UNSTABLE.md)
    /// </summary>
    public class ParamsOfRemove
    {
        /// <summary>
        /// Debot handle which references an instance of debot engine.
        /// </summary>
        [JsonPropertyName("debot_handle")]
        public uint DebotHandle { get; set; }
    }
}