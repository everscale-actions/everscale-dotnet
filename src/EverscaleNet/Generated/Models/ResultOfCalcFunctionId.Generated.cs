using System;
using System.Numerics;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace EverscaleNet.Client.Models
{
    /// <summary>
    /// <para>Not described yet..</para>
    /// </summary>
    public class ResultOfCalcFunctionId
    {
        /// <summary>
        /// <para>Contract function ID</para>
        /// </summary>
        [JsonPropertyName("function_id")]
        public uint FunctionId { get; set; }
    }
}