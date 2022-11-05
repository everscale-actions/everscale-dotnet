using System;
using System.Numerics;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace EverscaleNet.Client.Models
{
    /// <summary>
    /// <para>Not described yet..</para>
    /// </summary>
    public class DecodedOutput
    {
        /// <summary>
        /// <para>Decoded bodies of the out messages.</para>
        /// <para>If the message can't be decoded, then `None` will be stored in</para>
        /// <para>the appropriate position.</para>
        /// </summary>
        [JsonPropertyName("out_messages")]
        public DecodedMessageBody[] OutMessages { get; set; }

        /// <summary>
        /// <para>Decoded body of the function output message.</para>
        /// </summary>
        [JsonPropertyName("output")]
        public JsonElement? Output { get; set; }
    }
}