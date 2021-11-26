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
    public class ParamsOfProofBlockData
    {
        /// <summary>
        /// Single block's data, retrieved from TONOS API, that needs proof. Required fields are `id` and/or top-level `boc` (for block identification), others are optional.
        /// </summary>
        [JsonPropertyName("block")]
        public JsonElement? Block { get; set; }
    }
}