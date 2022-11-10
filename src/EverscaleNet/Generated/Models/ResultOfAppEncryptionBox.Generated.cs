using System;
using System.Numerics;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace EverscaleNet.Client.Models
{
    /// <summary>
    /// <para>Returning values from signing box callbacks.</para>
    /// </summary>
#if NET6_0_OR_GREATER
    [JsonPolymorphic(TypeDiscriminatorPropertyName = "type")]
    [JsonDerivedType(typeof(GetInfo), nameof(GetInfo))]
    [JsonDerivedType(typeof(Encrypt), nameof(Encrypt))]
    [JsonDerivedType(typeof(Decrypt), nameof(Decrypt))]
#endif
    public abstract class ResultOfAppEncryptionBox
    {
        /// <summary>
        /// <para>Result of getting encryption box info</para>
        /// </summary>
#if !NET6_0_OR_GREATER
        [Dahomey.Json.Attributes.JsonDiscriminator("GetInfo")]
#endif
        public class GetInfo : ResultOfAppEncryptionBox
        {
            /// <summary>
            /// <para>Result of getting encryption box info</para>
            /// </summary>
            [JsonPropertyName("info")]
            public EncryptionBoxInfo Info { get; set; }
        }

        /// <summary>
        /// <para>Result of encrypting data</para>
        /// </summary>
#if !NET6_0_OR_GREATER
        [Dahomey.Json.Attributes.JsonDiscriminator("Encrypt")]
#endif
        public class Encrypt : ResultOfAppEncryptionBox
        {
            /// <summary>
            /// <para>Result of encrypting data</para>
            /// </summary>
            [JsonPropertyName("data")]
            public string Data { get; set; }
        }

        /// <summary>
        /// <para>Result of decrypting data</para>
        /// </summary>
#if !NET6_0_OR_GREATER
        [Dahomey.Json.Attributes.JsonDiscriminator("Decrypt")]
#endif
        public class Decrypt : ResultOfAppEncryptionBox
        {
            /// <summary>
            /// <para>Result of decrypting data</para>
            /// </summary>
            [JsonPropertyName("data")]
            public string Data { get; set; }
        }
    }
}