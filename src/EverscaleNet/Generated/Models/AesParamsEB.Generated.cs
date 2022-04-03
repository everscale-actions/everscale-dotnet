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
    public class AesParamsEB
    {
        /// <summary>
        /// Not described yet..
        /// </summary>
        [JsonPropertyName("mode")]
        public CipherMode Mode { get; set; }

        /// <summary>
        /// Not described yet..
        /// </summary>
        [JsonPropertyName("key")]
        public string Key { get; set; }

        /// <summary>
        /// Not described yet..
        /// </summary>
        [JsonPropertyName("iv")]
        public string Iv { get; set; }
    }
}