using Dahomey.Json.Attributes;
using System;
using System.Numerics;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace ch1seL.TonNet.Client.Models
{
    /// <summary>
    /// [UNSTABLE](UNSTABLE.md) Describes DeBot metadata.
    /// </summary>
    public class DebotInfo
    {
        /// <summary>
        /// DeBot short name.
        /// </summary>
        [JsonPropertyName("name")]
        public string Name { get; set; }

        /// <summary>
        /// DeBot semantic version.
        /// </summary>
        [JsonPropertyName("version")]
        public string Version { get; set; }

        /// <summary>
        /// The name of DeBot deployer.
        /// </summary>
        [JsonPropertyName("publisher")]
        public string Publisher { get; set; }

        /// <summary>
        /// Short info about DeBot.
        /// </summary>
        [JsonPropertyName("caption")]
        public string Caption { get; set; }

        /// <summary>
        /// The name of DeBot developer.
        /// </summary>
        [JsonPropertyName("author")]
        public string Author { get; set; }

        /// <summary>
        /// TON address of author for questions and donations.
        /// </summary>
        [JsonPropertyName("support")]
        public string Support { get; set; }

        /// <summary>
        /// String with the first messsage from DeBot.
        /// </summary>
        [JsonPropertyName("hello")]
        public string Hello { get; set; }

        /// <summary>
        /// String with DeBot interface language (ISO-639).
        /// </summary>
        [JsonPropertyName("language")]
        public string Language { get; set; }

        /// <summary>
        /// String with DeBot ABI.
        /// </summary>
        [JsonPropertyName("dabi")]
        public string Dabi { get; set; }

        /// <summary>
        /// DeBot icon.
        /// </summary>
        [JsonPropertyName("icon")]
        public string Icon { get; set; }

        /// <summary>
        /// Vector with IDs of DInterfaces used by DeBot.
        /// </summary>
        [JsonPropertyName("interfaces")]
        public string[] Interfaces { get; set; }
    }
}