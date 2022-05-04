using Dahomey.Json.Attributes;
using System;
using System.Numerics;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace EverscaleNet.Client.Models
{
    /// <summary>
    /// Not described yet..
    /// </summary>
    public enum CryptoErrorCode
    {
        /// <summary>
        /// Not described yet..
        /// </summary>
        InvalidPublicKey = 100,
        /// <summary>
        /// Not described yet..
        /// </summary>
        InvalidSecretKey = 101,
        /// <summary>
        /// Not described yet..
        /// </summary>
        InvalidKey = 102,
        /// <summary>
        /// Not described yet..
        /// </summary>
        InvalidFactorizeChallenge = 106,
        /// <summary>
        /// Not described yet..
        /// </summary>
        InvalidBigInt = 107,
        /// <summary>
        /// Not described yet..
        /// </summary>
        ScryptFailed = 108,
        /// <summary>
        /// Not described yet..
        /// </summary>
        InvalidKeySize = 109,
        /// <summary>
        /// Not described yet..
        /// </summary>
        NaclSecretBoxFailed = 110,
        /// <summary>
        /// Not described yet..
        /// </summary>
        NaclBoxFailed = 111,
        /// <summary>
        /// Not described yet..
        /// </summary>
        NaclSignFailed = 112,
        /// <summary>
        /// Not described yet..
        /// </summary>
        Bip39InvalidEntropy = 113,
        /// <summary>
        /// Not described yet..
        /// </summary>
        Bip39InvalidPhrase = 114,
        /// <summary>
        /// Not described yet..
        /// </summary>
        Bip32InvalidKey = 115,
        /// <summary>
        /// Not described yet..
        /// </summary>
        Bip32InvalidDerivePath = 116,
        /// <summary>
        /// Not described yet..
        /// </summary>
        Bip39InvalidDictionary = 117,
        /// <summary>
        /// Not described yet..
        /// </summary>
        Bip39InvalidWordCount = 118,
        /// <summary>
        /// Not described yet..
        /// </summary>
        MnemonicGenerationFailed = 119,
        /// <summary>
        /// Not described yet..
        /// </summary>
        MnemonicFromEntropyFailed = 120,
        /// <summary>
        /// Not described yet..
        /// </summary>
        SigningBoxNotRegistered = 121,
        /// <summary>
        /// Not described yet..
        /// </summary>
        InvalidSignature = 122,
        /// <summary>
        /// Not described yet..
        /// </summary>
        EncryptionBoxNotRegistered = 123,
        /// <summary>
        /// Not described yet..
        /// </summary>
        InvalidIvSize = 124,
        /// <summary>
        /// Not described yet..
        /// </summary>
        UnsupportedCipherMode = 125,
        /// <summary>
        /// Not described yet..
        /// </summary>
        CannotCreateCipher = 126,
        /// <summary>
        /// Not described yet..
        /// </summary>
        EncryptDataError = 127,
        /// <summary>
        /// Not described yet..
        /// </summary>
        DecryptDataError = 128,
        /// <summary>
        /// Not described yet..
        /// </summary>
        IvRequired = 129,
        /// <summary>
        /// Not described yet..
        /// </summary>
        CryptoBoxNotRegistered = 130,
        /// <summary>
        /// Not described yet..
        /// </summary>
        InvalidCryptoBoxType = 131,
        /// <summary>
        /// Not described yet..
        /// </summary>
        CryptoBoxSecretSerializationError = 132,
        /// <summary>
        /// Not described yet..
        /// </summary>
        CryptoBoxSecretDeserializationError = 133,
        /// <summary>
        /// Not described yet..
        /// </summary>
        InvalidNonceSize = 134
    }
}