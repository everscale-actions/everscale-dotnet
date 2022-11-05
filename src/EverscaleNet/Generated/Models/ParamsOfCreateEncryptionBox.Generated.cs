using System;
using System.Numerics;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace EverscaleNet.Client.Models
{
    /// <summary>
    /// <para>Not described yet..</para>
    /// </summary>
    public class ParamsOfCreateEncryptionBox
    {
        /// <summary>
        /// <para>Encryption algorithm specifier including cipher parameters (key, IV, etc)</para>
        /// </summary>
        [JsonPropertyName("algorithm")]
        public EncryptionAlgorithm Algorithm { get; set; }
    }
}