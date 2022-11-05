using System;
using System.Numerics;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace EverscaleNet.Client.Models
{
    /// <summary>
    /// <para>Not described yet..</para>
    /// </summary>
    public class ParamsOfScrypt
    {
        /// <summary>
        /// <para>The password bytes to be hashed. Must be encoded with `base64`.</para>
        /// </summary>
        [JsonPropertyName("password")]
        public string Password { get; set; }

        /// <summary>
        /// <para>Salt bytes that modify the hash to protect against Rainbow table attacks. Must be encoded with `base64`.</para>
        /// </summary>
        [JsonPropertyName("salt")]
        public string Salt { get; set; }

        /// <summary>
        /// <para>CPU/memory cost parameter</para>
        /// </summary>
        [JsonPropertyName("log_n")]
        public byte LogN { get; set; }

        /// <summary>
        /// <para>The block size parameter, which fine-tunes sequential memory read size and performance.</para>
        /// </summary>
        [JsonPropertyName("r")]
        public uint R { get; set; }

        /// <summary>
        /// <para>Parallelization parameter.</para>
        /// </summary>
        [JsonPropertyName("p")]
        public uint P { get; set; }

        /// <summary>
        /// <para>Intended output length in octets of the derived key.</para>
        /// </summary>
        [JsonPropertyName("dk_len")]
        public uint DkLen { get; set; }
    }
}