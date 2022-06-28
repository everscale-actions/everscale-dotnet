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
    public class ParamsOfCalcFunctionId
    {
        /// <summary>
        /// Contract ABI.
        /// </summary>
        [JsonPropertyName("abi")]
        public Abi Abi { get; set; }

        /// <summary>
        /// Contract function name
        /// </summary>
        [JsonPropertyName("function_name")]
        public string FunctionName { get; set; }

        /// <summary>
        /// If set to `true` output function ID will be returned which is used in contract response. Default is `false`
        /// </summary>
        [JsonPropertyName("output")]
        public bool? Output { get; set; }
    }
}