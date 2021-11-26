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
    public class ParamsOfScrypt
    {
        /// <summary>
        /// The password bytes to be hashed. Must be encoded with `base64`.
        /// </summary>
        [JsonPropertyName("password")]
        public string Password { get; set; }

        /// <summary>
        /// Salt bytes that modify the hash to protect against Rainbow table attacks. Must be encoded with `base64`.
        /// </summary>
        [JsonPropertyName("salt")]
        public string Salt { get; set; }

        /// <summary>
        /// CPU/memory cost parameter
        /// </summary>
        [JsonPropertyName("log_n")]
        public byte LogN { get; set; }

        /// <summary>
        /// The block size parameter, which fine-tunes sequential memory read size and performance.
        /// </summary>
        [JsonPropertyName("r")]
        public uint R { get; set; }

        /// <summary>
        /// Parallelization parameter.
        /// </summary>
        [JsonPropertyName("p")]
        public uint P { get; set; }

        /// <summary>
        /// Intended output length in octets of the derived key.
        /// </summary>
        [JsonPropertyName("dk_len")]
        public uint DkLen { get; set; }
    }
}