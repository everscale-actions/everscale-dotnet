using System;
using System.Numerics;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace EverscaleNet.Client.Models
{
    /// <summary>
    /// <para>Not described yet..</para>
    /// </summary>
    [JsonPolymorphic(TypeDiscriminatorPropertyName = "type")]
    [JsonDerivedType(typeof(None), nameof(None))]
    [JsonDerivedType(typeof(Uninit), nameof(Uninit))]
    [JsonDerivedType(typeof(Account), nameof(Account))]
    public abstract class AccountForExecutor
    {
        /// <summary>
        /// <para>Non-existing account to run a creation internal message. Should be used with `skip_transaction_check = true` if the message has no deploy data since transactions on the uninitialized account are always aborted</para>
        /// </summary>
        public class None : AccountForExecutor
        {
        }

        /// <summary>
        /// <para>Emulate uninitialized account to run deploy message</para>
        /// </summary>
        public class Uninit : AccountForExecutor
        {
        }

        /// <summary>
        /// <para>Account state to run message</para>
        /// </summary>
        public class Account : AccountForExecutor
        {
            /// <summary>
            /// <para>Account BOC.</para>
            /// </summary>
            [JsonPropertyName("boc")]
            public string Boc { get; set; }

            /// <summary>
            /// <para>Flag for running account with the unlimited balance.</para>
            /// </summary>
            [JsonPropertyName("unlimited_balance")]
            public bool? UnlimitedBalance { get; set; }
        }
    }
}