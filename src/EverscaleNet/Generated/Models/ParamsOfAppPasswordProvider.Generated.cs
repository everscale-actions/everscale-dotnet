using System;
using System.Numerics;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace EverscaleNet.Client.Models
{
    /// <summary>
    /// <para>Interface that provides a callback that returns an encrypted password, used for cryptobox secret encryption</para>
    /// <para>To secure the password while passing it from application to the library,</para>
    /// <para>the library generates a temporary key pair, passes the pubkey</para>
    /// <para>to the passwordProvider, decrypts the received password with private key,</para>
    /// <para>and deletes the key pair right away.</para>
    /// <para>Application should generate a temporary nacl_box_keypair</para>
    /// <para>and encrypt the password with naclbox function using nacl_box_keypair.secret</para>
    /// <para>and encryption_public_key keys + nonce = 24-byte prefix of encryption_public_key.</para>
    /// </summary>
#if NET6_0_OR_GREATER
    [JsonPolymorphic(TypeDiscriminatorPropertyName = "type")]
    [JsonDerivedType(typeof(GetPassword), nameof(GetPassword))]
#endif
    public abstract class ParamsOfAppPasswordProvider
    {
        /// <summary>
        /// <para>Not described yet..</para>
        /// </summary>
#if !NET6_0_OR_GREATER
        [Dahomey.Json.Attributes.JsonDiscriminator("GetPassword")]
#endif
        public class GetPassword : ParamsOfAppPasswordProvider
        {
            /// <summary>
            /// <para>Temporary library pubkey, that is used on application side for password encryption, along with application temporary private key and nonce. Used for password decryption on library side.</para>
            /// </summary>
            [JsonPropertyName("encryption_public_key")]
            public string EncryptionPublicKey { get; set; }
        }
    }
}