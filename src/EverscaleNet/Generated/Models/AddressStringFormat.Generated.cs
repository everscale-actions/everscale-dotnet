using System;
using System.Numerics;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace EverscaleNet.Client.Models
{
    /// <summary>
    /// <para>Not described yet..</para>
    /// </summary>
#if NET6_0_OR_GREATER
    [JsonPolymorphic(TypeDiscriminatorPropertyName = "type")]
    [JsonDerivedType(typeof(AccountId), nameof(AccountId))]
    [JsonDerivedType(typeof(Hex), nameof(Hex))]
    [JsonDerivedType(typeof(Base64), nameof(Base64))]
#endif
    public abstract class AddressStringFormat
    {
        /// <summary>
        /// <para>Not described yet..</para>
        /// </summary>
#if !NET6_0_OR_GREATER
        [Dahomey.Json.Attributes.JsonDiscriminator("AccountId")]
#endif
        public class AccountId : AddressStringFormat
        {
        }

        /// <summary>
        /// <para>Not described yet..</para>
        /// </summary>
#if !NET6_0_OR_GREATER
        [Dahomey.Json.Attributes.JsonDiscriminator("Hex")]
#endif
        public class Hex : AddressStringFormat
        {
        }

        /// <summary>
        /// <para>Not described yet..</para>
        /// </summary>
#if !NET6_0_OR_GREATER
        [Dahomey.Json.Attributes.JsonDiscriminator("Base64")]
#endif
        public class Base64 : AddressStringFormat
        {
            /// <summary>
            /// <para>Not described yet..</para>
            /// </summary>
            [JsonPropertyName("url")]
            public bool Url { get; set; }

            /// <summary>
            /// <para>Not described yet..</para>
            /// </summary>
            [JsonPropertyName("test")]
            public bool Test { get; set; }

            /// <summary>
            /// <para>Not described yet..</para>
            /// </summary>
            [JsonPropertyName("bounce")]
            public bool Bounce { get; set; }
        }
    }
}