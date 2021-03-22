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
        /// <para>Decoded bodies of the out messages.</para>
        /// <para>If the message can't be decoded, then `None` will be stored in</para>
        /// <para>the appropriate position.</para>
        /// </summary>
        [JsonPropertyName("out_messages")]
        public JsonElement[] OutMessages { get; set; }

        /// <summary>
        /// Decoded body of the function output message.
        /// </summary>
        [JsonPropertyName("output")]
        public JsonElement? Output { get; set; }
    }
}