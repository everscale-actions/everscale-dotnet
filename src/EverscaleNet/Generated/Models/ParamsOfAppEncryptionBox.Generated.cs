using System;
using System.Numerics;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace EverscaleNet.Client.Models
{
    /// <summary>
    /// <para>Interface for data encryption/decryption</para>
    /// </summary>
#if NET6_0_OR_GREATER
    [JsonPolymorphic(TypeDiscriminatorPropertyName = "type")]
    [JsonDerivedType(typeof(GetInfo), nameof(GetInfo))]
    [JsonDerivedType(typeof(Encrypt), nameof(Encrypt))]
    [JsonDerivedType(typeof(Decrypt), nameof(Decrypt))]
#endif
    public abstract class ParamsOfAppEncryptionBox
    {
        /// <summary>
        /// <para>Get encryption box info</para>
        /// </summary>
#if !NET6_0_OR_GREATER
        [Dahomey.Json.Attributes.JsonDiscriminator("GetInfo")]
#endif
        public class GetInfo : ParamsOfAppEncryptionBox
        {
        }

        /// <summary>
        /// <para>Encrypt data</para>
        /// </summary>
#if !NET6_0_OR_GREATER
        [Dahomey.Json.Attributes.JsonDiscriminator("Encrypt")]
#endif
        public class Encrypt : ParamsOfAppEncryptionBox
        {
            /// <summary>
            /// <para>Data, encoded in Base64</para>
            /// </summary>
            [JsonPropertyName("data")]
            public string Data { get; set; }
        }

        /// <summary>
        /// <para>Decrypt data</para>
        /// </summary>
#if !NET6_0_OR_GREATER
        [Dahomey.Json.Attributes.JsonDiscriminator("Decrypt")]
#endif
        public class Decrypt : ParamsOfAppEncryptionBox
        {
            /// <summary>
            /// <para>Data, encoded in Base64</para>
            /// </summary>
            [JsonPropertyName("data")]
            public string Data { get; set; }
        }
    }
}