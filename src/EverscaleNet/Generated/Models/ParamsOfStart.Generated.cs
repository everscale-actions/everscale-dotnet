using System;
using System.Numerics;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace EverscaleNet.Client.Models
{
    /// <summary>
    /// <para>[UNSTABLE](UNSTABLE.md) [DEPRECATED](DEPRECATED.md) Parameters to start DeBot. DeBot must be already initialized with init() function.</para>
    /// </summary>
    public class ParamsOfStart
    {
        /// <summary>
        /// <para>Debot handle which references an instance of debot engine.</para>
        /// </summary>
        [JsonPropertyName("debot_handle")]
        public uint DebotHandle { get; set; }
    }
}