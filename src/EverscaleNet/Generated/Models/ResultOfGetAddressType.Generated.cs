using System;
using System.Numerics;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace EverscaleNet.Client.Models
{
    /// <summary>
    /// <para>Not described yet..</para>
    /// </summary>
    public class ResultOfGetAddressType
    {
        /// <summary>
        /// <para>Account address type.</para>
        /// </summary>
        [JsonPropertyName("address_type")]
        public AccountAddressType? AddressType { get; set; }
    }
}