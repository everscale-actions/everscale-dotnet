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
    public class ParamsOfNaclSign
    {
        /// <summary>
        /// Data that must be signed encoded in `base64`.
        /// </summary>
        [JsonPropertyName("unsigned")]
        public string Unsigned { get; set; }

        /// <summary>
        /// Signer's secret key - unprefixed 0-padded to 128 symbols hex string (concatenation of 64 symbols secret and 64 symbols public keys). See `nacl_sign_keypair_from_secret_key`.
        /// </summary>
        [JsonPropertyName("secret")]
        public string Secret { get; set; }
    }
}