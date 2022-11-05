using System;
using System.Numerics;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace EverscaleNet.Client.Models
{
    /// <summary>
    /// <para>Not described yet..</para>
    /// </summary>
    public enum CryptoErrorCode
    {
        /// <summary>
        /// <para>Not described yet..</para>
        /// </summary>
        InvalidPublicKey = 100,
        /// <summary>
        /// <para>Not described yet..</para>
        /// </summary>
        InvalidSecretKey = 101,
        /// <summary>
        /// <para>Not described yet..</para>
        /// </summary>
        InvalidKey = 102,
        /// <summary>
        /// <para>Not described yet..</para>
        /// </summary>
        InvalidFactorizeChallenge = 106,
        /// <summary>
        /// <para>Not described yet..</para>
        /// </summary>
        InvalidBigInt = 107,
        /// <summary>
        /// <para>Not described yet..</para>
        /// </summary>
        ScryptFailed = 108,
        /// <summary>
        /// <para>Not described yet..</para>
        /// </summary>
        InvalidKeySize = 109,
        /// <summary>
        /// <para>Not described yet..</para>
        /// </summary>
        NaclSecretBoxFailed = 110,
        /// <summary>
        /// <para>Not described yet..</para>
        /// </summary>
        NaclBoxFailed = 111,
        /// <summary>
        /// <para>Not described yet..</para>
        /// </summary>
        NaclSignFailed = 112,
        /// <summary>
        /// <para>Not described yet..</para>
        /// </summary>
        Bip39InvalidEntropy = 113,
        /// <summary>
        /// <para>Not described yet..</para>
        /// </summary>
        Bip39InvalidPhrase = 114,
        /// <summary>
        /// <para>Not described yet..</para>
        /// </summary>
        Bip32InvalidKey = 115,
        /// <summary>
        /// <para>Not described yet..</para>
        /// </summary>
        Bip32InvalidDerivePath = 116,
        /// <summary>
        /// <para>Not described yet..</para>
        /// </summary>
        Bip39InvalidDictionary = 117,
        /// <summary>
        /// <para>Not described yet..</para>
        /// </summary>
        Bip39InvalidWordCount = 118,
        /// <summary>
        /// <para>Not described yet..</para>
        /// </summary>
        MnemonicGenerationFailed = 119,
        /// <summary>
        /// <para>Not described yet..</para>
        /// </summary>
        MnemonicFromEntropyFailed = 120,
        /// <summary>
        /// <para>Not described yet..</para>
        /// </summary>
        SigningBoxNotRegistered = 121,
        /// <summary>
        /// <para>Not described yet..</para>
        /// </summary>
        InvalidSignature = 122,
        /// <summary>
        /// <para>Not described yet..</para>
        /// </summary>
        EncryptionBoxNotRegistered = 123,
        /// <summary>
        /// <para>Not described yet..</para>
        /// </summary>
        InvalidIvSize = 124,
        /// <summary>
        /// <para>Not described yet..</para>
        /// </summary>
        UnsupportedCipherMode = 125,
        /// <summary>
        /// <para>Not described yet..</para>
        /// </summary>
        CannotCreateCipher = 126,
        /// <summary>
        /// <para>Not described yet..</para>
        /// </summary>
        EncryptDataError = 127,
        /// <summary>
        /// <para>Not described yet..</para>
        /// </summary>
        DecryptDataError = 128,
        /// <summary>
        /// <para>Not described yet..</para>
        /// </summary>
        IvRequired = 129,
        /// <summary>
        /// <para>Not described yet..</para>
        /// </summary>
        CryptoBoxNotRegistered = 130,
        /// <summary>
        /// <para>Not described yet..</para>
        /// </summary>
        InvalidCryptoBoxType = 131,
        /// <summary>
        /// <para>Not described yet..</para>
        /// </summary>
        CryptoBoxSecretSerializationError = 132,
        /// <summary>
        /// <para>Not described yet..</para>
        /// </summary>
        CryptoBoxSecretDeserializationError = 133,
        /// <summary>
        /// <para>Not described yet..</para>
        /// </summary>
        InvalidNonceSize = 134
    }
}