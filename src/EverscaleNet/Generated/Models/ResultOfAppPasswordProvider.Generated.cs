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
    [JsonDerivedType(typeof(GetPassword), nameof(GetPassword))]
#endif
    public abstract class ResultOfAppPasswordProvider
    {
        /// <summary>
        /// <para>Not described yet..</para>
        /// </summary>
#if !NET6_0_OR_GREATER
        [Dahomey.Json.Attributes.JsonDiscriminator("GetPassword")]
#endif
        public class GetPassword : ResultOfAppPasswordProvider
        {
            /// <summary>
            /// <para>Password, encrypted and encoded to base64. Crypto box uses this password to decrypt its secret (seed phrase).</para>
            /// </summary>
            [JsonPropertyName("encrypted_password")]
            public string EncryptedPassword { get; set; }

            /// <summary>
            /// <para>Hex encoded public key of a temporary key pair, used for password encryption on application side.</para>
            /// </summary>
            [JsonPropertyName("app_encryption_pubkey")]
            public string AppEncryptionPubkey { get; set; }
        }
    }
}