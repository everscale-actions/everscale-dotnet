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
    public class ParamsOfDecodeBoc
    {
        /// <summary>
        /// Parameters to decode from BOC
        /// </summary>
        [JsonPropertyName("params")]
        public AbiParam[] @params { get; set; }

        /// <summary>
        /// Data BOC or BOC handle
        /// </summary>
        [JsonPropertyName("boc")]
        public string Boc { get; set; }

        /// <summary>
        /// Not described yet..
        /// </summary>
        [JsonPropertyName("allow_partial")]
        public bool AllowPartial { get; set; }
    }
}