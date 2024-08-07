using System;
using System.Numerics;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace EverscaleNet.Client.Models
{
    /// <summary>
    /// <para>Returning values from signing box callbacks.</para>
    /// </summary>
    [JsonPolymorphic(TypeDiscriminatorPropertyName = "type")]
    [JsonDerivedType(typeof(GetInfo), nameof(GetInfo))]
    [JsonDerivedType(typeof(Encrypt), nameof(Encrypt))]
    [JsonDerivedType(typeof(Decrypt), nameof(Decrypt))]
    public abstract class ResultOfAppEncryptionBox
    {
        /// <summary>
        /// <para>Result of getting encryption box info</para>
        /// </summary>
        public class GetInfo : ResultOfAppEncryptionBox
        {
            /// <summary>
            /// <para>Not described yet..</para>
            /// </summary>
            [JsonPropertyName("info")]
            public EncryptionBoxInfo Info { get; set; }
        }

        /// <summary>
        /// <para>Result of encrypting data</para>
        /// </summary>
        public class Encrypt : ResultOfAppEncryptionBox
        {
            /// <summary>
            /// <para>Encrypted data, encoded in Base64</para>
            /// </summary>
            [JsonPropertyName("data")]
            public string Data { get; set; }
        }

        /// <summary>
        /// <para>Result of decrypting data</para>
        /// </summary>
        public class Decrypt : ResultOfAppEncryptionBox
        {
            /// <summary>
            /// <para>Decrypted data, encoded in Base64</para>
            /// </summary>
            [JsonPropertyName("data")]
            public string Data { get; set; }
        }
    }
}