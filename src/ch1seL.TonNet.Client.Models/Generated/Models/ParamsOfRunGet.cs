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
    public class ParamsOfRunGet
    {
        /// <summary>
        /// Account BOC in `base64`
        /// </summary>
        [JsonPropertyName("account")]
        public string Account { get; set; }

        /// <summary>
        /// Function name
        /// </summary>
        [JsonPropertyName("function_name")]
        public string FunctionName { get; set; }

        /// <summary>
        /// Input parameters
        /// </summary>
        [JsonPropertyName("input")]
        public JsonElement? Input { get; set; }

        /// <summary>
        /// Not described yet..
        /// </summary>
        [JsonPropertyName("execution_options")]
        public ExecutionOptions ExecutionOptions { get; set; }
    }
}