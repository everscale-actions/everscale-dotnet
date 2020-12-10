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
    public class ParamsOfEncodeAccount
    {
        /// <summary>
        /// Not described yet..
        /// </summary>
        [JsonPropertyName("state_init")]
        public StateInitSource StateInit { get; set; }

        /// <summary>
        /// Not described yet..
        /// </summary>
        [JsonPropertyName("balance")]
        public ulong? Balance { get; set; }

        /// <summary>
        /// Not described yet..
        /// </summary>
        [JsonPropertyName("last_trans_lt")]
        public ulong? LastTransLt { get; set; }

        /// <summary>
        /// Not described yet..
        /// </summary>
        [JsonPropertyName("last_paid")]
        public uint? LastPaid { get; set; }
    }
}