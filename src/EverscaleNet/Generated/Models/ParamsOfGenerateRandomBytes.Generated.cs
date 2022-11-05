using System;
using System.Numerics;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace EverscaleNet.Client.Models
{
    /// <summary>
    /// <para>Not described yet..</para>
    /// </summary>
    public class ParamsOfGenerateRandomBytes
    {
        /// <summary>
        /// <para>Size of random byte array.</para>
        /// </summary>
        [JsonPropertyName("length")]
        public uint Length { get; set; }
    }
}