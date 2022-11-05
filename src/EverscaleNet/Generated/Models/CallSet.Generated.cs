using System;
using System.Numerics;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace EverscaleNet.Client.Models
{
    /// <summary>
    /// <para>Not described yet..</para>
    /// </summary>
    public class CallSet
    {
        /// <summary>
        /// <para>Function name that is being called. Or function id encoded as string in hex (starting with 0x).</para>
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
        /// <para>Function input parameters according to ABI.</para>
        /// </summary>
        [JsonPropertyName("input")]
        public JsonElement? Input { get; set; }
    }
}