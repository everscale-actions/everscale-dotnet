using System;
using System.Numerics;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace EverscaleNet.Client.Models
{
    /// <summary>
    /// <para>Not described yet..</para>
    /// </summary>
    public class ParamsOfDecodeBoc
    {
        /// <summary>
        /// <para>Parameters to decode from BOC</para>
        /// </summary>
        [JsonPropertyName("params")]
        public AbiParam[] @params { get; set; }

        /// <summary>
        /// <para>Data BOC or BOC handle</para>
        /// </summary>
        [JsonPropertyName("boc")]
        public string Boc { get; set; }

        /// <summary>
        /// <para>Not described yet..</para>
        /// </summary>
        [JsonPropertyName("allow_partial")]
        public bool AllowPartial { get; set; }
    }
}