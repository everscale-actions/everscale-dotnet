using Dahomey.Json.Attributes;
using System;
using System.Numerics;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace EverscaleNet.Client.Models
{
    /// <summary>
    /// Not described yet..
    /// </summary>
    public abstract class AddressStringFormat
    {
        /// <summary>
        /// Not described yet..
        /// </summary>
        [JsonDiscriminator("AccountId")]
        public class AccountId : AddressStringFormat
        {
        }

        /// <summary>
        /// Not described yet..
        /// </summary>
        [JsonDiscriminator("Hex")]
        public class Hex : AddressStringFormat
        {
        }

        /// <summary>
        /// Not described yet..
        /// </summary>
        [JsonDiscriminator("Base64")]
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