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
    public class ResultOfDecodeTvc
    {
        /// <summary>
        /// Contract code BOC encoded as base64 or BOC handle
        /// </summary>
        [JsonPropertyName("code")]
        public string Code { get; set; }

        /// <summary>
        /// Contract code hash
        /// </summary>
        [JsonPropertyName("code_hash")]
        public string CodeHash { get; set; }

        /// <summary>
        /// Contract code depth
        /// </summary>
        [JsonPropertyName("code_depth")]
        public uint? CodeDepth { get; set; }

        /// <summary>
        /// Contract data BOC encoded as base64 or BOC handle
        /// </summary>
        [JsonPropertyName("data")]
        public string Data { get; set; }

        /// <summary>
        /// Contract data hash
        /// </summary>
        [JsonPropertyName("data_hash")]
        public string DataHash { get; set; }

        /// <summary>
        /// Contract data depth
        /// </summary>
        [JsonPropertyName("data_depth")]
        public uint? DataDepth { get; set; }

        /// <summary>
        /// Contract library BOC encoded as base64 or BOC handle
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
        /// Is present and non-zero only in instances of large smart contracts
        /// </summary>
        [JsonPropertyName("split_depth")]
        public uint? SplitDepth { get; set; }

        /// <summary>
        /// Compiler version, for example 'sol 0.49.0'
        /// </summary>
        [JsonPropertyName("compiler_version")]
        public string CompilerVersion { get; set; }
    }
}