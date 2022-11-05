using System;
using System.Numerics;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace EverscaleNet.Client.Models
{
    /// <summary>
    /// <para>Not described yet..</para>
    /// </summary>
    public class ResultOfDecodeTvc
    {
        /// <summary>
        /// <para>Contract code BOC encoded as base64 or BOC handle</para>
        /// </summary>
        [JsonPropertyName("code")]
        public string Code { get; set; }

        /// <summary>
        /// <para>Contract code hash</para>
        /// </summary>
        [JsonPropertyName("code_hash")]
        public string CodeHash { get; set; }

        /// <summary>
        /// <para>Contract code depth</para>
        /// </summary>
        [JsonPropertyName("code_depth")]
        public uint? CodeDepth { get; set; }

        /// <summary>
        /// <para>Contract data BOC encoded as base64 or BOC handle</para>
        /// </summary>
        [JsonPropertyName("data")]
        public string Data { get; set; }

        /// <summary>
        /// <para>Contract data hash</para>
        /// </summary>
        [JsonPropertyName("data_hash")]
        public string DataHash { get; set; }

        /// <summary>
        /// <para>Contract data depth</para>
        /// </summary>
        [JsonPropertyName("data_depth")]
        public uint? DataDepth { get; set; }

        /// <summary>
        /// <para>Contract library BOC encoded as base64 or BOC handle</para>
        /// </summary>
        [JsonPropertyName("library")]
        public string Library { get; set; }

        /// <summary>
        /// <para>`special.tick` field.</para>
        /// <para>Specifies the contract ability to handle tick transactions</para>
        /// </summary>
        [JsonPropertyName("tick")]
        public bool? Tick { get; set; }

        /// <summary>
        /// <para>`special.tock` field.</para>
        /// <para>Specifies the contract ability to handle tock transactions</para>
        /// </summary>
        [JsonPropertyName("tock")]
        public bool? Tock { get; set; }

        /// <summary>
        /// <para>Is present and non-zero only in instances of large smart contracts</para>
        /// </summary>
        [JsonPropertyName("split_depth")]
        public uint? SplitDepth { get; set; }

        /// <summary>
        /// <para>Compiler version, for example 'sol 0.49.0'</para>
        /// </summary>
        [JsonPropertyName("compiler_version")]
        public string CompilerVersion { get; set; }
    }
}