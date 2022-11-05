using System;
using System.Numerics;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace EverscaleNet.Client.Models
{
    /// <summary>
    /// <para>Not described yet..</para>
    /// </summary>
    public class ParamsOfCalcFunctionId
    {
        /// <summary>
        /// <para>Contract ABI.</para>
        /// </summary>
        [JsonPropertyName("abi")]
        public Abi Abi { get; set; }

        /// <summary>
        /// <para>Contract function name</para>
        /// </summary>
        [JsonPropertyName("function_name")]
        public string FunctionName { get; set; }

        /// <summary>
        /// <para>If set to `true` output function ID will be returned which is used in contract response. Default is `false`</para>
        /// </summary>
        [JsonPropertyName("output")]
        public bool? Output { get; set; }
    }
}