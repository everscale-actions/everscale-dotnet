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
    public abstract class EncryptionAlgorithm
    {
        /// <summary>
        /// Not described yet..
        /// </summary>
        [JsonPropertyName("AES")]
        public AesParams AES { get; set; }
    }
}