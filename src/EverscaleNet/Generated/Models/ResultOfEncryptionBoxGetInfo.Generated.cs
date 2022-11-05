using System;
using System.Numerics;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace EverscaleNet.Client.Models
{
    /// <summary>
    /// <para>Not described yet..</para>
    /// </summary>
    public class ResultOfEncryptionBoxGetInfo
    {
        /// <summary>
        /// <para>Encryption box information</para>
        /// </summary>
        [JsonPropertyName("info")]
        public EncryptionBoxInfo Info { get; set; }
    }
}