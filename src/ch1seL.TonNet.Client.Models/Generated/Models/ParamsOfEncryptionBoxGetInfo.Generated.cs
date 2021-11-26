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
    public class ParamsOfEncryptionBoxGetInfo
    {
        /// <summary>
        /// Encryption box handle
        /// </summary>
        [JsonPropertyName("encryption_box")]
        public uint EncryptionBox { get; set; }
    }
}