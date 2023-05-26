using System;
using System.Numerics;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace EverscaleNet.Client.Models
{
    /// <summary>
    /// <para>[UNSTABLE](UNSTABLE.md) Describes the operation that the DeBot wants to perform.</para>
    /// </summary>
#if NET6_0_OR_GREATER
    [JsonPolymorphic(TypeDiscriminatorPropertyName = "type")]
    [JsonDerivedType(typeof(Transaction), nameof(Transaction))]
#endif
    public abstract class DebotActivity
    {
        /// <summary>
        /// <para>DeBot wants to create new transaction in blockchain.</para>
        /// </summary>
#if !NET6_0_OR_GREATER
        [Dahomey.Json.Attributes.JsonDiscriminator("Transaction")]
#endif
        public class Transaction : DebotActivity
        {
            /// <summary>
            /// <para>External inbound message BOC.</para>
            /// </summary>
            [JsonPropertyName("msg")]
            public string Msg { get; set; }

            /// <summary>
            /// <para>Target smart contract address.</para>
            /// </summary>
            [JsonPropertyName("dst")]
            public string Dst { get; set; }

            /// <summary>
            /// <para>List of spendings as a result of transaction.</para>
            /// </summary>
            [JsonPropertyName("out")]
            public Spending[] Out { get; set; }

            /// <summary>
            /// <para>Transaction total fee.</para>
            /// </summary>
            [JsonPropertyName("fee")]
            public BigInteger Fee { get; set; }

            /// <summary>
            /// <para>Indicates if target smart contract updates its code.</para>
            /// </summary>
            [JsonPropertyName("setcode")]
            public bool Setcode { get; set; }

            /// <summary>
            /// <para>Public key from keypair that was used to sign external message.</para>
            /// </summary>
            [JsonPropertyName("signkey")]
            public string Signkey { get; set; }

            /// <summary>
            /// <para>Signing box handle used to sign external message.</para>
            /// </summary>
            [JsonPropertyName("signing_box_handle")]
            public uint SigningBoxHandle { get; set; }
        }
    }
}