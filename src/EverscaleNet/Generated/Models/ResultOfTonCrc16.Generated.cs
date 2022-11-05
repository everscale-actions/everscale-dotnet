using System;
using System.Numerics;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace EverscaleNet.Client.Models
{
    /// <summary>
    /// <para>Not described yet..</para>
    /// </summary>
    public class ResultOfTonCrc16
    {
        /// <summary>
        /// <para>Calculated CRC for input data.</para>
        /// </summary>
        [JsonPropertyName("crc")]
        public ushort Crc { get; set; }
    }
}