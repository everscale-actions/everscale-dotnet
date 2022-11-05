using System;
using System.Numerics;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace EverscaleNet.Client.Models
{
    /// <summary>
    /// <para>Not described yet..</para>
    /// </summary>
    public class RegisteredEncryptionBox
    {
        /// <summary>
        /// <para>Handle of the encryption box.</para>
        /// </summary>
        [JsonPropertyName("handle")]
        public uint Handle { get; set; }
    }
}