using System;
using System.Numerics;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace EverscaleNet.Client.Models
{
    /// <summary>
    /// <para>Not described yet..</para>
    /// </summary>
    public class ResultOfGetCryptoBoxInfo
    {
        /// <summary>
        /// <para>Secret (seed phrase) encrypted with salt and password.</para>
        /// </summary>
        [JsonPropertyName("encrypted_secret")]
        public string EncryptedSecret { get; set; }
    }
}