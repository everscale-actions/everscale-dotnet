using System;
using System.Numerics;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace EverscaleNet.Client.Models
{
    /// <summary>
    /// <para>Not described yet..</para>
    /// </summary>
    public class ResultOfEncryptionBoxDecrypt
    {
        /// <summary>
        /// <para>Decrypted data, encoded in Base64.</para>
        /// </summary>
        [JsonPropertyName("data")]
        public string Data { get; set; }
    }
}