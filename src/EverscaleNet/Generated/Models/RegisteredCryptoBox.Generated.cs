using Dahomey.Json.Attributes;
using System;
using System.Numerics;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace EverscaleNet.Client.Models
{
    /// <summary>
    /// Not described yet..
    /// </summary>
    public class RegisteredCryptoBox
    {
        /// <summary>
        /// Not described yet..
        /// </summary>
        [JsonPropertyName("handle")]
        public uint Handle { get; set; }
    }
}