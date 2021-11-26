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
    public class AesInfo
    {
        /// <summary>
        /// Not described yet..
        /// </summary>
        [JsonPropertyName("mode")]
        public CipherMode Mode { get; set; }

        /// <summary>
        /// Not described yet..
        /// </summary>
        [JsonPropertyName("iv")]
        public string Iv { get; set; }
    }
}