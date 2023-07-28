using System;
using System.Numerics;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace EverscaleNet.Client.Models
{
    /// <summary>
    /// <para>[UNSTABLE](UNSTABLE.md) [DEPRECATED](DEPRECATED.md)</para>
    /// </summary>
    public class ParamsOfRemove
    {
        /// <summary>
        /// <para>Debot handle which references an instance of debot engine.</para>
        /// </summary>
        [JsonPropertyName("debot_handle")]
        public uint DebotHandle { get; set; }
    }
}