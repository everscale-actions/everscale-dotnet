using System;
using System.Numerics;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace EverscaleNet.Client.Models
{
    /// <summary>
    /// <para>[UNSTABLE](UNSTABLE.md) [DEPRECATED](DEPRECATED.md) Parameters for executing debot action.</para>
    /// </summary>
    public class ParamsOfExecute
    {
        /// <summary>
        /// <para>Debot handle which references an instance of debot engine.</para>
        /// </summary>
        [JsonPropertyName("debot_handle")]
        public uint DebotHandle { get; set; }

        /// <summary>
        /// <para>Debot Action that must be executed.</para>
        /// </summary>
        [JsonPropertyName("action")]
        public DebotAction Action { get; set; }
    }
}