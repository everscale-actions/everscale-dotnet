using System;
using System.Numerics;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace EverscaleNet.Client.Models
{
    /// <summary>
    /// <para>Crypto Box Secret.</para>
    /// </summary>
#if NET6_0_OR_GREATER
    [JsonPolymorphic(TypeDiscriminatorPropertyName = "type")]
    [JsonDerivedType(typeof(RandomSeedPhrase), nameof(RandomSeedPhrase))]
    [JsonDerivedType(typeof(PredefinedSeedPhrase), nameof(PredefinedSeedPhrase))]
    [JsonDerivedType(typeof(EncryptedSecret), nameof(EncryptedSecret))]
#endif
    public abstract class CryptoBoxSecret
    {
        /// <summary>
        /// <para>Creates Crypto Box from a random seed phrase. This option can be used if a developer doesn't want the seed phrase to leave the core library's memory, where it is stored encrypted.</para>
        /// <para>This type should be used upon the first wallet initialization, all further initializations</para>
        /// <para>should use `EncryptedSecret` type instead.</para>
        /// <para>Get `encrypted_secret` with `get_crypto_box_info` function and store it on your side.</para>
        /// </summary>
#if !NET6_0_OR_GREATER
        [Dahomey.Json.Attributes.JsonDiscriminator("RandomSeedPhrase")]
#endif
        public class RandomSeedPhrase : CryptoBoxSecret
        {
            /// <summary>
            /// <para>Creates Crypto Box from a random seed phrase. This option can be used if a developer doesn't want the seed phrase to leave the core library's memory, where it is stored encrypted.</para>
            /// <para>This type should be used upon the first wallet initialization, all further initializations</para>
            /// <para>should use `EncryptedSecret` type instead.</para>
            /// <para>Get `encrypted_secret` with `get_crypto_box_info` function and store it on your side.</para>
            /// </summary>
            [JsonPropertyName("dictionary")]
            public MnemonicDictionary? Dictionary { get; set; }

            /// <summary>
            /// <para>Creates Crypto Box from a random seed phrase. This option can be used if a developer doesn't want the seed phrase to leave the core library's memory, where it is stored encrypted.</para>
            /// <para>This type should be used upon the first wallet initialization, all further initializations</para>
            /// <para>should use `EncryptedSecret` type instead.</para>
            /// <para>Get `encrypted_secret` with `get_crypto_box_info` function and store it on your side.</para>
            /// </summary>
            [JsonPropertyName("wordcount")]
            public byte Wordcount { get; set; }
        }

        /// <summary>
        /// <para>Restores crypto box instance from an existing seed phrase. This type should be used when Crypto Box is initialized from a seed phrase, entered by a user.</para>
        /// <para>This type should be used only upon the first wallet initialization, all further</para>
        /// <para>initializations should use `EncryptedSecret` type instead.</para>
        /// <para>Get `encrypted_secret` with `get_crypto_box_info` function and store it on your side.</para>
        /// </summary>
#if !NET6_0_OR_GREATER
        [Dahomey.Json.Attributes.JsonDiscriminator("PredefinedSeedPhrase")]
#endif
        public class PredefinedSeedPhrase : CryptoBoxSecret
        {
            /// <summary>
            /// <para>Restores crypto box instance from an existing seed phrase. This type should be used when Crypto Box is initialized from a seed phrase, entered by a user.</para>
            /// <para>This type should be used only upon the first wallet initialization, all further</para>
            /// <para>initializations should use `EncryptedSecret` type instead.</para>
            /// <para>Get `encrypted_secret` with `get_crypto_box_info` function and store it on your side.</para>
            /// </summary>
            [JsonPropertyName("phrase")]
            public string Phrase { get; set; }

            /// <summary>
            /// <para>Restores crypto box instance from an existing seed phrase. This type should be used when Crypto Box is initialized from a seed phrase, entered by a user.</para>
            /// <para>This type should be used only upon the first wallet initialization, all further</para>
            /// <para>initializations should use `EncryptedSecret` type instead.</para>
            /// <para>Get `encrypted_secret` with `get_crypto_box_info` function and store it on your side.</para>
            /// </summary>
            [JsonPropertyName("dictionary")]
            public MnemonicDictionary? Dictionary { get; set; }

            /// <summary>
            /// <para>Restores crypto box instance from an existing seed phrase. This type should be used when Crypto Box is initialized from a seed phrase, entered by a user.</para>
            /// <para>This type should be used only upon the first wallet initialization, all further</para>
            /// <para>initializations should use `EncryptedSecret` type instead.</para>
            /// <para>Get `encrypted_secret` with `get_crypto_box_info` function and store it on your side.</para>
            /// </summary>
            [JsonPropertyName("wordcount")]
            public byte Wordcount { get; set; }
        }

        /// <summary>
        /// <para>Use this type for wallet reinitializations, when you already have `encrypted_secret` on hands. To get `encrypted_secret`, use `get_crypto_box_info` function after you initialized your crypto box for the first time.</para>
        /// <para>It is an object, containing seed phrase or private key, encrypted with</para>
        /// <para>`secret_encryption_salt` and password from `password_provider`.</para>
        /// <para>Note that if you want to change salt or password provider, then you need to reinitialize</para>
        /// <para>the wallet with `PredefinedSeedPhrase`, then get `EncryptedSecret` via `get_crypto_box_info`,</para>
        /// <para>store it somewhere, and only after that initialize the wallet with `EncryptedSecret` type.</para>
        /// </summary>
#if !NET6_0_OR_GREATER
        [Dahomey.Json.Attributes.JsonDiscriminator("EncryptedSecret")]
#endif
        public class EncryptedSecret : CryptoBoxSecret
        {
            /// <summary>
            /// <para>Use this type for wallet reinitializations, when you already have `encrypted_secret` on hands. To get `encrypted_secret`, use `get_crypto_box_info` function after you initialized your crypto box for the first time.</para>
            /// <para>It is an object, containing seed phrase or private key, encrypted with</para>
            /// <para>`secret_encryption_salt` and password from `password_provider`.</para>
            /// <para>Note that if you want to change salt or password provider, then you need to reinitialize</para>
            /// <para>the wallet with `PredefinedSeedPhrase`, then get `EncryptedSecret` via `get_crypto_box_info`,</para>
            /// <para>store it somewhere, and only after that initialize the wallet with `EncryptedSecret` type.</para>
            /// </summary>
            [JsonPropertyName("encrypted_secret")]
            public string EncryptedSecretAccessor { get; set; }
        }
    }
}