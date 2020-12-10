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
    public class CallSet
    {
        /// <summary>
        /// Not described yet..
        /// </summary>
        [JsonPropertyName("function_name")]
        public string FunctionName { get; set; }

        /// <summary>
        /// <para>If an application omits some header parameters required by the</para>
        /// <para>contract's ABI, the library will set the default values for</para>
        /// <para>them.</para>
        /// </summary>
        [JsonPropertyName("header")]
        public FunctionHeader Header { get; set; }

        /// <summary>
        /// Not described yet..
        /// </summary>
        [JsonPropertyName("input")]
        public JsonElement? Input { get; set; }
    }
}