using System;
using System.Numerics;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace EverscaleNet.Client.Models
{
    /// <summary>
    /// <para>Not described yet..</para>
    /// </summary>
    [JsonPolymorphic(TypeDiscriminatorPropertyName = "type")]
    [JsonDerivedType(typeof(Contract), nameof(Contract))]
    [JsonDerivedType(typeof(Json), nameof(Json))]
    [JsonDerivedType(typeof(Handle), nameof(Handle))]
    [JsonDerivedType(typeof(Serialized), nameof(Serialized))]
    public abstract class Abi
    {
        /// <summary>
        /// <para>Not described yet..</para>
        /// </summary>
        public class Contract : Abi
        {
            /// <summary>
            /// <para>Not described yet..</para>
            /// </summary>
            [JsonPropertyName("value")]
            public AbiContract Value { get; set; }
        }

        /// <summary>
        /// <para>Not described yet..</para>
        /// </summary>
        public class Json : Abi
        {
            /// <summary>
            /// <para>Not described yet..</para>
            /// </summary>
            [JsonPropertyName("value")]
            public string Value { get; set; }
        }

        /// <summary>
        /// <para>Not described yet..</para>
        /// </summary>
        public class Handle : Abi
        {
            /// <summary>
            /// <para>Not described yet..</para>
            /// </summary>
            [JsonPropertyName("value")]
            public uint Value { get; set; }
        }

        /// <summary>
        /// <para>Not described yet..</para>
        /// </summary>
        public class Serialized : Abi
        {
            /// <summary>
            /// <para>Not described yet..</para>
            /// </summary>
            [JsonPropertyName("value")]
            public AbiContract Value { get; set; }
        }
    }
}