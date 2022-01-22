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
    public class ResultOfDecodeInitialData
    {
        /// <summary>
        /// <para>List of initial values of contract's public variables.</para>
        /// <para>Initial data is decoded if `abi` input parameter is provided</para>
        /// </summary>
        [JsonPropertyName("initial_data")]
        public JsonElement? InitialData { get; set; }

        /// <summary>
        /// Initial account owner's public key
        /// </summary>
        [JsonPropertyName("initial_pubkey")]
        public string InitialPubkey { get; set; }
    }
}