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
    public abstract class Abi
    {
        [JsonDiscriminator("Contract")]
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

        [JsonDiscriminator("Json")]
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

        [JsonDiscriminator("Handle")]
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

        [JsonDiscriminator("Serialized")]
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