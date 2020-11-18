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
        [JsonDiscriminator("None")]
        /// <summary>
        /// <para> Non-existing account to run a creation internal message.</para>
        /// <para> Should be used with `skip_transaction_check = true` if the message has no deploy data</para>
        /// <para> since transactions on the uninitialized account are always aborted</para>
        /// </summary>
        public class None : AccountForExecutor
        {
        }

        [JsonDiscriminator("Uninit")]
        /// <summary>
        ///  Emulate uninitialized account to run deploy message
        /// </summary>
        public class Uninit : AccountForExecutor
        {
        }

        [JsonDiscriminator("Account")]
        /// <summary>
        ///  Account state to run message
        /// </summary>
        public class Account : AccountForExecutor
        {
            /// <summary>
            ///  Account state to run message
            /// </summary>
            [JsonPropertyName("boc")]
            public string Boc { get; set; }

            /// <summary>
            ///  Account state to run message
            /// </summary>
            [JsonPropertyName("unlimited_balance")]
            public bool UnlimitedBalance { get; set; }
        }
    }
}