using System;
using System.Numerics;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace EverscaleNet.Client.Models
{
    /// <summary>
    /// <para>Not described yet..</para>
    /// </summary>
    public class ParamsOfNaclSign
    {
        /// <summary>
        /// <para>Data that must be signed encoded in `base64`.</para>
        /// </summary>
        [JsonPropertyName("unsigned")]
        public string Unsigned { get; set; }

        /// <summary>
        /// <para>Signer's secret key - unprefixed 0-padded to 128 symbols hex string (concatenation of 64 symbols secret and 64 symbols public keys). See `nacl_sign_keypair_from_secret_key`.</para>
        /// </summary>
        [JsonPropertyName("secret")]
        public string Secret { get; set; }
    }
}