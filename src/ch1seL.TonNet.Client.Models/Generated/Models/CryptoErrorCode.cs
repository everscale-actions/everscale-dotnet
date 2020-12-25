using Dahomey.Json.Attributes;
using System;
using System.Numerics;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace ch1seL.TonNet.Client.Models
{
    /// <summary>
    /// 

    /// </summary>
    public enum CryptoErrorCode
    {
        InvalidPublicKey = 100,
        InvalidSecretKey = 101,
        InvalidKey = 102,
        InvalidFactorizeChallenge = 106,
        InvalidBigInt = 107,
        ScryptFailed = 108,
        InvalidKeySize = 109,
        NaclSecretBoxFailed = 110,
        NaclBoxFailed = 111,
        NaclSignFailed = 112,
        Bip39InvalidEntropy = 113,
        Bip39InvalidPhrase = 114,
        Bip32InvalidKey = 115,
        Bip32InvalidDerivePath = 116,
        Bip39InvalidDictionary = 117,
        Bip39InvalidWordCount = 118,
        MnemonicGenerationFailed = 119,
        MnemonicFromEntropyFailed = 120,
        SigningBoxNotRegistered = 121
    }
}