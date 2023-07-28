using System;
using System.Numerics;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace EverscaleNet.Client.Models
{
    /// <summary>
    /// <para>[UNSTABLE](UNSTABLE.md) [DEPRECATED](DEPRECATED.md) Describes DeBot metadata.</para>
    /// </summary>
    public class DebotInfo
    {
        /// <summary>
        /// <para>DeBot short name.</para>
        /// </summary>
        [JsonPropertyName("name")]
        public string Name { get; set; }

        /// <summary>
        /// <para>DeBot semantic version.</para>
        /// </summary>
        [JsonPropertyName("version")]
        public string Version { get; set; }

        /// <summary>
        /// <para>The name of DeBot deployer.</para>
        /// </summary>
        [JsonPropertyName("publisher")]
        public string Publisher { get; set; }

        /// <summary>
        /// <para>Short info about DeBot.</para>
        /// </summary>
        [JsonPropertyName("caption")]
        public string Caption { get; set; }

        /// <summary>
        /// <para>The name of DeBot developer.</para>
        /// </summary>
        [JsonPropertyName("author")]
        public string Author { get; set; }

        /// <summary>
        /// <para>TON address of author for questions and donations.</para>
        /// </summary>
        [JsonPropertyName("support")]
        public string Support { get; set; }

        /// <summary>
        /// <para>String with the first messsage from DeBot.</para>
        /// </summary>
        [JsonPropertyName("hello")]
        public string Hello { get; set; }

        /// <summary>
        /// <para>String with DeBot interface language (ISO-639).</para>
        /// </summary>
        [JsonPropertyName("language")]
        public string Language { get; set; }

        /// <summary>
        /// <para>String with DeBot ABI.</para>
        /// </summary>
        [JsonPropertyName("dabi")]
        public string Dabi { get; set; }

        /// <summary>
        /// <para>DeBot icon.</para>
        /// </summary>
        [JsonPropertyName("icon")]
        public string Icon { get; set; }

        /// <summary>
        /// <para>Vector with IDs of DInterfaces used by DeBot.</para>
        /// </summary>
        [JsonPropertyName("interfaces")]
        public string[] Interfaces { get; set; }

        /// <summary>
        /// <para>ABI version ("x.y") supported by DeBot</para>
        /// </summary>
        [JsonPropertyName("dabiVersion")]
        public string DabiVersion { get; set; }
    }
}