using System;
using System.Numerics;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace EverscaleNet.Client.Models
{
    /// <summary>
    /// <para>Not described yet..</para>
    /// </summary>
#if NET7_0_OR_GREATER
    [JsonPolymorphic(TypeDiscriminatorPropertyName = "type")]
    [JsonDerivedType(typeof(Contract), nameof(Contract))]
    [JsonDerivedType(typeof(Json), nameof(Json))]
    [JsonDerivedType(typeof(Handle), nameof(Handle))]
    [JsonDerivedType(typeof(Serialized), nameof(Serialized))]
#endif
    public abstract class Abi
    {
        /// <summary>
        /// <para>Not described yet..</para>
        /// </summary>
#if !NET7_0_OR_GREATER
        [Dahomey.Json.Attributes.JsonDiscriminator("Contract")]
#endif
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
#if !NET7_0_OR_GREATER
        [Dahomey.Json.Attributes.JsonDiscriminator("Json")]
#endif
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
#if !NET7_0_OR_GREATER
        [Dahomey.Json.Attributes.JsonDiscriminator("Handle")]
#endif
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
#if !NET7_0_OR_GREATER
        [Dahomey.Json.Attributes.JsonDiscriminator("Serialized")]
#endif
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