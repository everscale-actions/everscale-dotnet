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
    public abstract class AddressStringFormat
    {
        [JsonDiscriminator("AccountId")]
        /// <summary>
        /// Not described yet..
        /// </summary>
        public class AccountId : AddressStringFormat
        {
        }

        [JsonDiscriminator("Hex")]
        /// <summary>
        /// Not described yet..
        /// </summary>
        public class Hex : AddressStringFormat
        {
        }

        [JsonDiscriminator("Base64")]
        /// <summary>
        /// Not described yet..
        /// </summary>
        public class Base64 : AddressStringFormat
        {
            /// <summary>
            /// Not described yet..
            /// </summary>
            [JsonPropertyName("url")]
            public bool Url { get; set; }

            /// <summary>
            /// Not described yet..
            /// </summary>
            [JsonPropertyName("test")]
            public bool Test { get; set; }

            /// <summary>
            /// Not described yet..
            /// </summary>
            [JsonPropertyName("bounce")]
            public bool Bounce { get; set; }
        }
    }
}