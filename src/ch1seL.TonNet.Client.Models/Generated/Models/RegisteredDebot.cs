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
    public class RegisteredDebot
    {
        /// <summary>
        /// Not described yet..
        /// </summary>
        [JsonPropertyName("debot_handle")]
        public uint DebotHandle { get; set; }
    }
}