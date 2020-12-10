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
        /// Function name that is being called.
        /// </summary>
        [JsonPropertyName("function_name")]
        public string FunctionName { get; set; }

        /// <summary>
        /// <para>Function header.</para>
        /// <para>If an application omits some header parameters required by the</para>
        /// <para>contract's ABI, the library will set the default values for</para>
        /// <para>them.</para>
        /// </summary>
        [JsonPropertyName("header")]
        public FunctionHeader Header { get; set; }

        /// <summary>
        /// Function input parameters according to ABI.
        /// </summary>
        [JsonPropertyName("input")]
        public JsonElement? Input { get; set; }
    }
}