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
        /// Not described yet..
        /// </summary>
        [JsonPropertyName("password")]
        public string Password { get; set; }

        /// <summary>
        /// Not described yet..
        /// </summary>
        [JsonPropertyName("salt")]
        public string Salt { get; set; }

        /// <summary>
        /// Not described yet..
        /// </summary>
        [JsonPropertyName("log_n")]
        public byte LogN { get; set; }

        /// <summary>
        /// Not described yet..
        /// </summary>
        [JsonPropertyName("r")]
        public uint R { get; set; }

        /// <summary>
        /// Not described yet..
        /// </summary>
        [JsonPropertyName("p")]
        public uint P { get; set; }

        /// <summary>
        /// Not described yet..
        /// </summary>
        [JsonPropertyName("dk_len")]
        public uint DkLen { get; set; }
    }
}