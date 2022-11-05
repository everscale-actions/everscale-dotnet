using System;
using System.Numerics;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace EverscaleNet.Client.Models
{
    /// <summary>
    /// <para>[UNSTABLE](UNSTABLE.md) Describes how much funds will be debited from the target  contract balance as a result of the transaction.</para>
    /// </summary>
    public class Spending
    {
        /// <summary>
        /// <para>Amount of nanotokens that will be sent to `dst` address.</para>
        /// </summary>
        [JsonPropertyName("amount")]
        public ulong Amount { get; set; }

        /// <summary>
        /// <para>Destination address of recipient of funds.</para>
        /// </summary>
        [JsonPropertyName("dst")]
        public string Dst { get; set; }
    }
}