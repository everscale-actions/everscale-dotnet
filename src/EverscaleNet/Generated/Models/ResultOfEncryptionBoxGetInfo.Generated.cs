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
    public class ResultOfEncryptionBoxGetInfo
    {
        /// <summary>
        /// Encryption box information
        /// </summary>
        [JsonPropertyName("info")]
        public EncryptionBoxInfo Info { get; set; }
    }
}