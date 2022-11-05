using System;
using System.Numerics;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace EverscaleNet.Client.Models
{
    /// <summary>
    /// <para>Not described yet..</para>
    /// </summary>
    public class ParamsOfProofBlockData
    {
        /// <summary>
        /// <para>Single block's data, retrieved from TONOS API, that needs proof. Required fields are `id` and/or top-level `boc` (for block identification), others are optional.</para>
        /// </summary>
        [JsonPropertyName("block")]
        public JsonElement? Block { get; set; }
    }
}