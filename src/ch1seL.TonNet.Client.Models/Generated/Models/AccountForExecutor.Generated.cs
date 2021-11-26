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
    public abstract class AccountForExecutor
    {
        /// <summary>
        /// Non-existing account to run a creation internal message. Should be used with `skip_transaction_check = true` if the message has no deploy data since transactions on the uninitialized account are always aborted
        /// </summary>
        [JsonDiscriminator("None")]
        public class None : AccountForExecutor
        {
        }

        /// <summary>
        /// Emulate uninitialized account to run deploy message
        /// </summary>
        [JsonDiscriminator("Uninit")]
        public class Uninit : AccountForExecutor
        {
        }

        /// <summary>
        /// Account state to run message
        /// </summary>
        [JsonDiscriminator("Account")]
        public class Account : AccountForExecutor
        {
            /// <summary>
            /// Account state to run message
            /// </summary>
            [JsonPropertyName("boc")]
            public string Boc { get; set; }

            /// <summary>
            /// Account state to run message
            /// </summary>
            [JsonPropertyName("unlimited_balance")]
            public bool UnlimitedBalance { get; set; }
        }
    }
}