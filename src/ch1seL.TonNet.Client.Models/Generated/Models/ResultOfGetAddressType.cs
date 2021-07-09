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
    public class ResultOfGetAddressType
    {
        /// <summary>
        /// Account address type.
        /// </summary>
        [JsonPropertyName("address_type")]
        public AccountAddressType AddressType { get; set; }
    }
}