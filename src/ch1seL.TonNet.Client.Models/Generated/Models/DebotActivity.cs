using System.Text.Json.Serialization;
using Dahomey.Json.Attributes;

namespace ch1seL.TonNet.Client.Models
{
    /// <summary>
    ///     [UNSTABLE](UNSTABLE.md) Describes the operation that the DeBot wants to perform.
    /// </summary>
    public abstract class DebotActivity
    {
        /// <summary>
        ///     DeBot wants to create new transaction in blockchain.
        /// </summary>
        [JsonDiscriminator("Transaction")]
        public class Transaction : DebotActivity
        {
            /// <summary>
            ///     DeBot wants to create new transaction in blockchain.
            /// </summary>
            [JsonPropertyName("msg")]
            public string Msg { get; set; }

            /// <summary>
            ///     DeBot wants to create new transaction in blockchain.
            /// </summary>
            [JsonPropertyName("dst")]
            public string Dst { get; set; }

            /// <summary>
            ///     DeBot wants to create new transaction in blockchain.
            /// </summary>
            [JsonPropertyName("out")]
            public Spending[] Out { get; set; }

            /// <summary>
            ///     DeBot wants to create new transaction in blockchain.
            /// </summary>
            [JsonPropertyName("fee")]
            public ulong Fee { get; set; }

            /// <summary>
            ///     DeBot wants to create new transaction in blockchain.
            /// </summary>
            [JsonPropertyName("setcode")]
            public bool Setcode { get; set; }

            /// <summary>
            ///     DeBot wants to create new transaction in blockchain.
            /// </summary>
            [JsonPropertyName("signkey")]
            public string Signkey { get; set; }

            /// <summary>
            ///     DeBot wants to create new transaction in blockchain.
            /// </summary>
            [JsonPropertyName("signing_box_handle")]
            public uint SigningBoxHandle { get; set; }
        }
    }
}