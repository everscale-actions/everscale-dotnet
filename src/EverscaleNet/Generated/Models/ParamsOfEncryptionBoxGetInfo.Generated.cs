using System;
using System.Numerics;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace EverscaleNet.Client.Models
{
    /// <summary>
    /// <para>Not described yet..</para>
    /// </summary>
    public class ParamsOfEncryptionBoxGetInfo
    {
        /// <summary>
        /// <para>Encryption box handle</para>
        /// </summary>
        [JsonPropertyName("encryption_box")]
        public uint EncryptionBox { get; set; }
    }
}