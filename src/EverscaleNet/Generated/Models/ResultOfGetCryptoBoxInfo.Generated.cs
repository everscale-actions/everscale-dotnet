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
    public class ResultOfGetCryptoBoxInfo
    {
        /// <summary>
        /// Secret (seed phrase) encrypted with salt and password.
        /// </summary>
        [JsonPropertyName("encrypted_secret")]
        public string EncryptedSecret { get; set; }
    }
}