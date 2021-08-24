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
    public class ParamsOfCreateEncryptionBox
    {
        /// <summary>
        /// Encryption algorithm specifier including cipher parameters (key, IV, etc)
        /// </summary>
        [JsonPropertyName("algorithm")]
        public EncryptionAlgorithm Algorithm { get; set; }
    }
}