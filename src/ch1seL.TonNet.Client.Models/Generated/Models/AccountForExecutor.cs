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
        /// Not described yet..
        /// </summary>
        [JsonDiscriminator("None")]
        public class None : AccountForExecutor
        {
        }

        /// <summary>
        /// Not described yet..
        /// </summary>
        [JsonDiscriminator("Uninit")]
        public class Uninit : AccountForExecutor
        {
        }

        /// <summary>
        /// Not described yet..
        /// </summary>
        [JsonDiscriminator("Account")]
        public class Account : AccountForExecutor
        {
            /// <summary>
            /// Not described yet..
            /// </summary>
            [JsonPropertyName("boc")]
            public string Boc { get; set; }

            /// <summary>
            /// Not described yet..
            /// </summary>
            [JsonPropertyName("unlimited_balance")]
            public bool UnlimitedBalance { get; set; }
        }
    }
}