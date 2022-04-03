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
    public class ResultOfGetCryptoBoxSeedPhrase
    {
        /// <summary>
        /// Not described yet..
        /// </summary>
        [JsonPropertyName("phrase")]
        public string Phrase { get; set; }

        /// <summary>
        /// Not described yet..
        /// </summary>
        [JsonPropertyName("dictionary")]
        public byte Dictionary { get; set; }

        /// <summary>
        /// Not described yet..
        /// </summary>
        [JsonPropertyName("wordcount")]
        public byte Wordcount { get; set; }
    }
}