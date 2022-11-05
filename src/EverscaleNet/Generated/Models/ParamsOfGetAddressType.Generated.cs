using System;
using System.Numerics;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace EverscaleNet.Client.Models
{
    /// <summary>
    /// <para>Not described yet..</para>
    /// </summary>
    public class ParamsOfGetAddressType
    {
        /// <summary>
        /// <para>Account address in any TON format.</para>
        /// </summary>
        [JsonPropertyName("address")]
        public string Address { get; set; }
    }
}