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
    public class EncodeAccountRequest
    {
        /// <summary>
        ///  Source of the account state init.
        /// </summary>
        [JsonPropertyName("state_init")]
        public StateInitSource StateInit { get; set; }

        /// <summary>
        ///  Initial balance.
        /// </summary>
        [JsonPropertyName("balance"), JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public ulong? Balance { get; set; }

        /// <summary>
        ///  Initial value for the `last_trans_lt`.
        /// </summary>
        [JsonPropertyName("last_trans_lt"), JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public ulong? LastTransLt { get; set; }

        /// <summary>
        ///  Initial value for the `last_paid`.
        /// </summary>
        [JsonPropertyName("last_paid"), JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public uint? LastPaid { get; set; }
    }
}