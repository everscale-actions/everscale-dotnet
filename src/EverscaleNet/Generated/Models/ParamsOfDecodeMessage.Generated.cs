using System;
using System.Numerics;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace EverscaleNet.Client.Models
{
    /// <summary>
    /// <para>Not described yet..</para>
    /// </summary>
    public class ParamsOfDecodeMessage
    {
        /// <summary>
        /// <para>contract ABI</para>
        /// </summary>
        [JsonPropertyName("abi")]
        public Abi Abi { get; set; }

        /// <summary>
        /// <para>Message BOC</para>
        /// </summary>
        [JsonPropertyName("message")]
        public string Message { get; set; }

        /// <summary>
        /// <para>Flag allowing partial BOC decoding when ABI doesn't describe the full body BOC. Controls decoder behaviour when after decoding all described in ABI params there are some data left in BOC: `true` - return decoded values `false` - return error of incomplete BOC deserialization (default)</para>
        /// </summary>
        [JsonPropertyName("allow_partial")]
        public bool? AllowPartial { get; set; }

        /// <summary>
        /// <para>Function name or function id if is known in advance</para>
        /// </summary>
        [JsonPropertyName("function_name")]
        public string FunctionName { get; set; }

        /// <summary>
        /// <para>Not described yet..</para>
        /// </summary>
        [JsonPropertyName("data_layout")]
        public DataLayout? DataLayout { get; set; }
    }
}