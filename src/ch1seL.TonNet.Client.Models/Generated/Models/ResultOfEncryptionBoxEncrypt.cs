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