using Dahomey.Json.Attributes;
using System;
using System.Numerics;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace ch1seL.TonNet.Client.Models
{
    /// <summary>
    /// Not described yet..
    /// </summary>
    public class ParamsOfGenerateRandomBytes
    {
        /// <summary>
        /// Size of random byte array.
        /// </summary>
        [JsonPropertyName("length")]
        public uint Length { get; set; }
    }
}