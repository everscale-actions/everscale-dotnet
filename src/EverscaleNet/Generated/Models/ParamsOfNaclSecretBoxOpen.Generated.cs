using System;
using System.Numerics;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace EverscaleNet.Client.Models
{
    /// <summary>
    /// <para>Not described yet..</para>
    /// </summary>
    public class ParamsOfNaclSecretBoxOpen
    {
        /// <summary>
        /// <para>Data that must be decrypted.</para>
        /// <para>Encoded with `base64`.</para>
        /// </summary>
        [JsonPropertyName("encrypted")]
        public string Encrypted { get; set; }

        /// <summary>
        /// <para>Nonce in `hex`</para>
        /// </summary>
        [JsonPropertyName("nonce")]
        public string Nonce { get; set; }

        /// <summary>
        /// <para>Secret key - unprefixed 0-padded to 64 symbols hex string</para>
        /// </summary>
        [JsonPropertyName("key")]
        public string Key { get; set; }
    }
}