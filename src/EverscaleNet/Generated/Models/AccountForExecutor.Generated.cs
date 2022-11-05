using System;
using System.Numerics;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace EverscaleNet.Client.Models
{
    /// <summary>
    /// <para>Not described yet..</para>
    /// </summary>
#if NET7_0_OR_GREATER
    [JsonPolymorphic(TypeDiscriminatorPropertyName = "type")]
    [JsonDerivedType(typeof(None), nameof(None))]
    [JsonDerivedType(typeof(Uninit), nameof(Uninit))]
    [JsonDerivedType(typeof(Account), nameof(Account))]
#endif
    public abstract class AccountForExecutor
    {
        /// <summary>
        /// <para>Non-existing account to run a creation internal message. Should be used with `skip_transaction_check = true` if the message has no deploy data since transactions on the uninitialized account are always aborted</para>
        /// </summary>
#if !NET7_0_OR_GREATER
        [Dahomey.Json.Attributes.JsonDiscriminator("None")]
#endif
        public class None : AccountForExecutor
        {
        }

        /// <summary>
        /// <para>Emulate uninitialized account to run deploy message</para>
        /// </summary>
#if !NET7_0_OR_GREATER
        [Dahomey.Json.Attributes.JsonDiscriminator("Uninit")]
#endif
        public class Uninit : AccountForExecutor
        {
        }

        /// <summary>
        /// <para>Account state to run message</para>
        /// </summary>
#if !NET7_0_OR_GREATER
        [Dahomey.Json.Attributes.JsonDiscriminator("Account")]
#endif
        public class Account : AccountForExecutor
        {
            /// <summary>
            /// <para>Account state to run message</para>
            /// </summary>
            [JsonPropertyName("boc")]
            public string Boc { get; set; }

            /// <summary>
            /// <para>Account state to run message</para>
            /// </summary>
            [JsonPropertyName("unlimited_balance")]
            public bool UnlimitedBalance { get; set; }
        }
    }
}