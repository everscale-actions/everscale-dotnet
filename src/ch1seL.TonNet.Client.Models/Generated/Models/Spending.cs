using Dahomey.Json.Attributes;
using System;
using System.Numerics;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace ch1seL.TonNet.Client.Models
{
    /// <summary>
    /// [UNSTABLE](UNSTABLE.md) Describes how much funds will be debited from the target  contract balance as a result of the transaction.
    /// </summary>
    public class Spending
    {
        /// <summary>
        /// Amount of nanotokens that will be sent to `dst` address.
        /// </summary>
        [JsonPropertyName("amount")]
        public ulong Amount { get; set; }

        /// <summary>
        /// Destination address of recipient of funds.
        /// </summary>
        [JsonPropertyName("dst")]
        public string Dst { get; set; }
    }
}