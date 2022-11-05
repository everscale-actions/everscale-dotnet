using System;
using System.Numerics;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace EverscaleNet.Client.Models
{
    /// <summary>
    /// <para>[UNSTABLE](UNSTABLE.md) Describes the operation that the DeBot wants to perform.</para>
    /// </summary>
#if NET7_0_OR_GREATER
    [JsonPolymorphic(TypeDiscriminatorPropertyName = "type")]
    [JsonDerivedType(typeof(Transaction), nameof(Transaction))]
#endif
    public abstract class DebotActivity
    {
        /// <summary>
        /// <para>DeBot wants to create new transaction in blockchain.</para>
        /// </summary>
#if !NET7_0_OR_GREATER
        [Dahomey.Json.Attributes.JsonDiscriminator("Transaction")]
#endif
        public class Transaction : DebotActivity
        {
            /// <summary>
            /// <para>DeBot wants to create new transaction in blockchain.</para>
            /// </summary>
            [JsonPropertyName("msg")]
            public string Msg { get; set; }

            /// <summary>
            /// <para>DeBot wants to create new transaction in blockchain.</para>
            /// </summary>
            [JsonPropertyName("dst")]
            public string Dst { get; set; }

            /// <summary>
            /// <para>DeBot wants to create new transaction in blockchain.</para>
            /// </summary>
            [JsonPropertyName("out")]
            public Spending[] Out { get; set; }

            /// <summary>
            /// <para>DeBot wants to create new transaction in blockchain.</para>
            /// </summary>
            [JsonPropertyName("fee")]
            public ulong Fee { get; set; }

            /// <summary>
            /// <para>DeBot wants to create new transaction in blockchain.</para>
            /// </summary>
            [JsonPropertyName("setcode")]
            public bool Setcode { get; set; }

            /// <summary>
            /// <para>DeBot wants to create new transaction in blockchain.</para>
            /// </summary>
            [JsonPropertyName("signkey")]
            public string Signkey { get; set; }

            /// <summary>
            /// <para>DeBot wants to create new transaction in blockchain.</para>
            /// </summary>
            [JsonPropertyName("signing_box_handle")]
            public uint SigningBoxHandle { get; set; }
        }
    }
}