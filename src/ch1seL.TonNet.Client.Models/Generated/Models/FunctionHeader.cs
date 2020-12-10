using Dahomey.Json.Attributes;
using System;
using System.Numerics;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace ch1seL.TonNet.Client.Models
{
    /// <summary>
    /// <para>Includes several hidden function parameters that contract</para>
    /// <para>uses for security, message delivery monitoring and replay protection reasons.</para>
    /// <para>The actual set of header fields depends on the contract's ABI.</para>
    /// <para>If a contract's ABI does not include some headers, then they are not filled.</para>
    /// </summary>
    public class FunctionHeader
    {
        /// <summary>
        /// Not described yet..
        /// </summary>
        [JsonPropertyName("expire")]
        public uint? Expire { get; set; }

        /// <summary>
        /// If not specified, `now` is used(if ABI includes `time` header).
        /// </summary>
        [JsonPropertyName("time")]
        public ulong? Time { get; set; }

        /// <summary>
        /// Encoded in `hex`.If not specified, method fails with exception (if ABI includes `pubkey` header)..
        /// </summary>
        [JsonPropertyName("pubkey")]
        public string Pubkey { get; set; }
    }
}