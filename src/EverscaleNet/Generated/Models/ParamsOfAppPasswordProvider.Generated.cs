using Dahomey.Json.Attributes;
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
    public abstract class ParamsOfAppPasswordProvider
    {
        /// <summary>
        /// Not described yet..
        /// </summary>
        [JsonDiscriminator("GetPassword")]
        public class GetPassword : ParamsOfAppPasswordProvider
        {
            /// <summary>
            /// Not described yet..
            /// </summary>
            [JsonPropertyName("encryption_public_key")]
            public string EncryptionPublicKey { get; set; }
        }
    }
}