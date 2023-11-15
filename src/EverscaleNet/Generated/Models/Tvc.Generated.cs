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
    [JsonDerivedType(typeof(V1), nameof(V1))]
    public abstract class Tvc
    {
        /// <summary>
        /// <para>Not described yet..</para>
        /// </summary>
        public class V1 : Tvc
        {
            /// <summary>
            /// <para>Not described yet..</para>
            /// </summary>
            [JsonPropertyName("value")]
            public TvcV1 Value { get; set; }
        }
    }
}