using System;
using System.Numerics;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace EverscaleNet.Client.Models
{
    /// <summary>
    /// <para>Not described yet..</para>
    /// </summary>
    public class ParamsOfEncryptionBoxDecrypt
    {
        /// <summary>
        /// <para>Encryption box handle</para>
        /// </summary>
        [JsonPropertyName("encryption_box")]
        public uint EncryptionBox { get; set; }

        /// <summary>
        /// <para>Data to be decrypted, encoded in Base64</para>
        /// </summary>
        [JsonPropertyName("data")]
        public string Data { get; set; }
    }
}