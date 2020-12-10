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
    public class DecodedOutput
    {
        /// <summary>
        /// <para>If the message can't be decoded, then `None` will be stored in</para>
        /// <para>the appropriate position.</para>
        /// </summary>
        [JsonPropertyName("out_messages")]
        public JsonElement?[] OutMessages { get; set; }

        /// <summary>
        /// Not described yet..
        /// </summary>
        [JsonPropertyName("output")]
        public JsonElement? Output { get; set; }
    }
}