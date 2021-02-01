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
        /// Std address of interface or debot.
        /// </summary>
        [JsonPropertyName("source")]
        public string Source { get; set; }

        /// <summary>
        /// Function Id to call
        /// </summary>
        [JsonPropertyName("func_id")]
        public uint FuncId { get; set; }

        /// <summary>
        /// Json string with parameters
        /// </summary>
        [JsonPropertyName("params")]
        public string @params { get; set; }
    }
}