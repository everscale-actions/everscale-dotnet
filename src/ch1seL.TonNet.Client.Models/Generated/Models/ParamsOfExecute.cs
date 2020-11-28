using Dahomey.Json.Attributes;
using System;
using System.Numerics;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace ch1seL.TonNet.Client.Models
{
    /// <summary>
    ///  [UNSTABLE](UNSTABLE.md) Parameters for executing debot action.
    /// </summary>
    public class ParamsOfExecute
    {
        /// <summary>
        ///  Debot handle which references an instance of debot engine.
        /// </summary>
        [JsonPropertyName("debot_handle")]
        public uint DebotHandle { get; set; }

        /// <summary>
        ///  Debot Action that must be executed.
        /// </summary>
        [JsonPropertyName("action")]
        public DebotAction Action { get; set; }
    }
}