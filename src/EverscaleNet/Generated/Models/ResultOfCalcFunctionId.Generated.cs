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
    public class ResultOfCalcFunctionId
    {
        /// <summary>
        /// Contract function ID
        /// </summary>
        [JsonPropertyName("function_id")]
        public uint FunctionId { get; set; }
    }
}