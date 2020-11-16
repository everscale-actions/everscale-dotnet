using ch1seL.TonNet.Abstract;
using ch1seL.TonNet.Client.Abstract;
using ch1seL.TonNet.Client.Models;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace ch1seL.TonNet.Client
{
    public interface ICrypto : ITonModule
    {
        public Task<FactorizeResponse> Factorize(FactorizeRequest @params, CancellationToken cancellationToken = default);
        public Task<ModularPowerResponse> ModularPower(ModularPowerRequest @params, CancellationToken cancellationToken = default);
        public Task<TonCrc16Response> TonCrc16(TonCrc16Request @params, CancellationToken cancellationToken = default);
        public Task<GenerateRandomBytesResponse> GenerateRandomBytes(GenerateRandomBytesRequest @params, CancellationToken cancellationToken = default);
        public Task<ConvertPublicKeyToTonSafeFormatResponse> ConvertPublicKeyToTonSafeFormat(ConvertPublicKeyToTonSafeFormatRequest @params, CancellationToken cancellationToken = default);
        public Task<KeyPair> GenerateRandomSignKeys(CancellationToken cancellationToken = default);
        public Task<SignResponse> Sign(SignRequest @params, CancellationToken cancellationToken = default);
        public Task<VerifySignatureResponse> VerifySignature(VerifySignatureRequest @params, CancellationToken cancellationToken = default);
        public Task<HashResponse> Sha256(HashRequest @params, CancellationToken cancellationToken = default);
        public Task<HashResponse> Sha512(HashRequest @params, CancellationToken cancellationToken = default);
        public Task<ScryptResponse> Scrypt(ScryptRequest @params, CancellationToken cancellationToken = default);
        public Task<KeyPair> NaclSignKeypairFromSecretKey(NaclSignKeyPairFromSecretRequest @params, CancellationToken cancellationToken = default);
        public Task<NaclSignResponse> NaclSign(NaclSignRequest @params, CancellationToken cancellationToken = default);
        public Task<NaclSignOpenResponse> NaclSignOpen(NaclSignOpenRequest @params, CancellationToken cancellationToken = default);
        public Task<NaclSignDetachedResponse> NaclSignDetached(NaclSignRequest @params, CancellationToken cancellationToken = default);
        public Task<KeyPair> NaclBoxKeypair(CancellationToken cancellationToken = default);
        public Task<KeyPair> NaclBoxKeypairFromSecretKey(NaclBoxKeyPairFromSecretRequest @params, CancellationToken cancellationToken = default);
        public Task<NaclBoxResponse> NaclBox(NaclBoxRequest @params, CancellationToken cancellationToken = default);
        public Task<NaclBoxOpenResponse> NaclBoxOpen(NaclBoxOpenRequest @params, CancellationToken cancellationToken = default);
        public Task<NaclBoxResponse> NaclSecretBox(NaclSecretBoxRequest @params, CancellationToken cancellationToken = default);
        public Task<NaclBoxOpenResponse> NaclSecretBoxOpen(NaclSecretBoxOpenRequest @params, CancellationToken cancellationToken = default);
        public Task<MnemonicWordsResponse> MnemonicWords(MnemonicWordsRequest @params, CancellationToken cancellationToken = default);
        public Task<MnemonicFromRandomResponse> MnemonicFromRandom(MnemonicFromRandomRequest @params, CancellationToken cancellationToken = default);
        public Task<MnemonicFromEntropyResponse> MnemonicFromEntropy(MnemonicFromEntropyRequest @params, CancellationToken cancellationToken = default);
        public Task<MnemonicVerifyResponse> MnemonicVerify(MnemonicVerifyRequest @params, CancellationToken cancellationToken = default);
        public Task<KeyPair> MnemonicDeriveSignKeys(MnemonicDeriveSignKeysRequest @params, CancellationToken cancellationToken = default);
        public Task<HDKeyXPrvFromMnemonicResponse> HdkeyXprvFromMnemonic(HDKeyXPrvFromMnemonicRequest @params, CancellationToken cancellationToken = default);
        public Task<HDKeyDeriveFromXPrvResponse> HdkeyDeriveFromXprv(HDKeyDeriveFromXPrvRequest @params, CancellationToken cancellationToken = default);
        public Task<HDKeyDeriveFromXPrvPathResponse> HdkeyDeriveFromXprvPath(HDKeyDeriveFromXPrvPathRequest @params, CancellationToken cancellationToken = default);
        public Task<HDKeySecretFromXPrvResponse> HdkeySecretFromXprv(HDKeySecretFromXPrvRequest @params, CancellationToken cancellationToken = default);
        public Task<HDKeyPublicFromXPrvResponse> HdkeyPublicFromXprv(HDKeyPublicFromXPrvRequest @params, CancellationToken cancellationToken = default);
        public Task<ChaCha20Response> Chacha20(ChaCha20Request @params, CancellationToken cancellationToken = default);
    }
}