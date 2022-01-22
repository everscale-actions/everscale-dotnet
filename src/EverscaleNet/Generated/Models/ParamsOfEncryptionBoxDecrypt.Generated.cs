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
    public class ParamsOfEncryptionBoxDecrypt
    {
        /// <summary>
        /// Encryption box handle
        /// </summary>
        [JsonPropertyName("encryption_box")]
        public uint EncryptionBox { get; set; }

        /// <summary>
        /// Data to be decrypted, encoded in Base64
        /// </summary>
        [JsonPropertyName("data")]
        public string Data { get; set; }
    }
}