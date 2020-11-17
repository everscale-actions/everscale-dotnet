using System;
using System.Numerics;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace ch1seL.TonNet.Client.Models
{
    /// <summary>
    /// Not described yet..
    /// </summary>
    public class ConvertAddressRequest
    {
        /// <summary>
        ///  Account address in any TON format.
        /// </summary>
        [JsonPropertyName("address")]
        public string Address { get; set; }

        /// <summary>
        ///  Specify the format to convert to.
        /// </summary>
        [JsonPropertyName("output_format")]
        public AddressStringFormat OutputFormat { get; set; }
    }
}