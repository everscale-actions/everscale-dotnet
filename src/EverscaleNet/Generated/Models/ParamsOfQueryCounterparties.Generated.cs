using System;
using System.Numerics;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace EverscaleNet.Client.Models
{
    /// <summary>
    /// <para>Not described yet..</para>
    /// </summary>
    public class ParamsOfQueryCounterparties
    {
        /// <summary>
        /// <para>Account address</para>
        /// </summary>
        [JsonPropertyName("account")]
        public string Account { get; set; }

        /// <summary>
        /// <para>Projection (result) string</para>
        /// </summary>
        [JsonPropertyName("result")]
        public string Result { get; set; }

        /// <summary>
        /// <para>Number of counterparties to return</para>
        /// </summary>
        [JsonPropertyName("first")]
        public uint? First { get; set; }

        /// <summary>
        /// <para>`cursor` field of the last received result</para>
        /// </summary>
        [JsonPropertyName("after")]
        public string After { get; set; }
    }
}