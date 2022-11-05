using System;
using System.Numerics;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace EverscaleNet.Client.Models
{
    /// <summary>
    /// <para>Not described yet..</para>
    /// </summary>
    public class ResultOfEncryptionBoxEncrypt
    {
        /// <summary>
        /// <para>Encrypted data, encoded in Base64.</para>
        /// <para>Padded to cipher block size</para>
        /// </summary>
        [JsonPropertyName("data")]
        public string Data { get; set; }
    }
}