using System;
using System.Numerics;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace ch1seL.TonNet.Client.Models
{
    /// <summary>
    /// <para> The ABI function header.</para>
    /// <para> Includes several hidden function parameters that contract</para>
    /// <para> uses for security, message delivery monitoring and replay protection reasons.</para>
    /// <para> The actual set of header fields depends on the contract's ABI.</para>
    /// <para> If a contract's ABI does not include some headers, then they are not filled.</para>
    /// </summary>
    public class FunctionHeader
    {
        /// <summary>
        /// <para> Message expiration time in seconds.</para>
        /// <para> If not specified - calculated automatically from message_expiration_timeout(),</para>
        /// <para> try_index and message_expiration_timeout_grow_factor() (if ABI includes `expire` header).</para>
        /// </summary>
        [JsonPropertyName("expire"), JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public uint? Expire { get; set; }

        /// <summary>
        /// <para> Message creation time in milliseconds. If not specified, `now` is used</para>
        /// <para> (if ABI includes `time` header).</para>
        /// </summary>
        [JsonPropertyName("time"), JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public BigInteger? Time { get; set; }

        /// <summary>
        /// <para> Public key is used by the contract to check the signature. Encoded in `hex`.</para>
        /// <para> If not specified, method fails with exception (if ABI includes `pubkey` header)..</para>
        /// </summary>
        [JsonPropertyName("pubkey")]
        public string Pubkey { get; set; }
    }
}