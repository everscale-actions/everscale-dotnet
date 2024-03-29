using System;
using System.Numerics;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace EverscaleNet.Client.Models
{
    /// <summary>
    /// <para>Not described yet..</para>
    /// </summary>
    public class ParamsOfRunGet
    {
        /// <summary>
        /// <para>Account BOC in `base64`</para>
        /// </summary>
        [JsonPropertyName("account")]
        public string Account { get; set; }

        /// <summary>
        /// <para>Function name</para>
        /// </summary>
        [JsonPropertyName("function_name")]
        public string FunctionName { get; set; }

        /// <summary>
        /// <para>Input parameters</para>
        /// </summary>
        [JsonPropertyName("input")]
        public JsonElement? Input { get; set; }

        /// <summary>
        /// <para>Execution options</para>
        /// </summary>
        [JsonPropertyName("execution_options")]
        public ExecutionOptions ExecutionOptions { get; set; }

        /// <summary>
        /// <para>Convert lists based on nested tuples in the **result** into plain arrays.</para>
        /// <para>Default is `false`. Input parameters may use any of lists representations</para>
        /// <para>If you receive this error on Web: "Runtime error. Unreachable code should not be executed...",</para>
        /// <para>set this flag to true.</para>
        /// <para>This may happen, for example, when elector contract contains too many participants</para>
        /// </summary>
        [JsonPropertyName("tuple_list_as_array")]
        public bool? TupleListAsArray { get; set; }
    }
}