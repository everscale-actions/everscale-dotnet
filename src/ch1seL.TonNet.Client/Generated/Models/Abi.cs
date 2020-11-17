using System;
using System.Numerics;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace ch1seL.TonNet.Client.Models
{
    /// <summary>
    /// Not described yet..
    /// </summary>
    public abstract class Abi
    {
        /// <summary>
        /// Not described yet..
        /// </summary>
        public class Contract : Abi
        {
            /// <summary>
            /// Not described yet..
            /// </summary>
            [JsonPropertyName("value")]
            public AbiContract Value { get; set; }
        }

        /// <summary>
        /// Not described yet..
        /// </summary>
        public class Json : Abi
        {
            /// <summary>
            /// Not described yet..
            /// </summary>
            [JsonPropertyName("value")]
            public string Value { get; set; }
        }

        /// <summary>
        /// Not described yet..
        /// </summary>
        public class Handle : Abi
        {
            /// <summary>
            /// Not described yet..
            /// </summary>
            [JsonPropertyName("value")]
            public uint Value { get; set; }
        }

        /// <summary>
        /// Not described yet..
        /// </summary>
        public class Serialized : Abi
        {
            /// <summary>
            /// Not described yet..
            /// </summary>
            [JsonPropertyName("value")]
            public AbiContract Value { get; set; }
        }
    }
}