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
    public class ResultOfRunTvm
    {
        /// <summary>
        /// <para>List of output messages' BOCs.</para>
        /// <para>Encoded as `base64`</para>
        /// </summary>
        [JsonPropertyName("out_messages")]
        public string[] OutMessages { get; set; }

        /// <summary>
        /// Optional decoded message bodies according to the optional `abi` parameter.
        /// </summary>
        [JsonPropertyName("decoded")]
        public DecodedOutput Decoded { get; set; }

        /// <summary>
        /// <para>Updated account state BOC.</para>
        /// <para>Encoded as `base64`. Attention! Only `account_state.storage.state.data` part of the BOC is updated.</para>
        /// </summary>
        [JsonPropertyName("account")]
        public string Account { get; set; }
    }
}