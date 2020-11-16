using ch1seL.TonNet.Abstract;
using ch1seL.TonNet.Client.Abstract;
using ch1seL.TonNet.Client.Models;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace ch1seL.TonNet.Client
{
    public class Crypto : ICrypto
    {
        private readonly ITonClientAdapter _tonClientAdapter;

        public Crypto(ITonClientAdapter tonClientAdapter)
        {
            _tonClientAdapter = tonClientAdapter;
        }

        public async Task<FactorizeResponse> Factorize(FactorizeRequest @params, CancellationToken cancellationToken = default)
        {
            return await _tonClientAdapter.Request<FactorizeRequest, FactorizeResponse>("crypto.factorize", @params, cancellationToken);
        }

        public async Task<ModularPowerResponse> ModularPower(ModularPowerRequest @params, CancellationToken cancellationToken = default)
        {
            return await _tonClientAdapter.Request<ModularPowerRequest, ModularPowerResponse>("crypto.modular_power", @params, cancellationToken);
        }

        public async Task<TonCrc16Response> TonCrc16(TonCrc16Request @params, CancellationToken cancellationToken = default)
        {
            return await _tonClientAdapter.Request<TonCrc16Request, TonCrc16Response>("crypto.ton_crc16", @params, cancellationToken);
        }

        public async Task<GenerateRandomBytesResponse> GenerateRandomBytes(GenerateRandomBytesRequest @params, CancellationToken cancellationToken = default)
        {
            return await _tonClientAdapter.Request<GenerateRandomBytesRequest, GenerateRandomBytesResponse>("crypto.generate_random_bytes", @params, cancellationToken);
        }

        public async Task<ConvertPublicKeyToTonSafeFormatResponse> ConvertPublicKeyToTonSafeFormat(ConvertPublicKeyToTonSafeFormatRequest @params, CancellationToken cancellationToken = default)
        {
            return await _tonClientAdapter.Request<ConvertPublicKeyToTonSafeFormatRequest, ConvertPublicKeyToTonSafeFormatResponse>("crypto.convert_public_key_to_ton_safe_format", @params, cancellationToken);
        }

        public async Task<KeyPair> GenerateRandomSignKeys(CancellationToken cancellationToken = default)
        {
            return await _tonClientAdapter.Request<KeyPair>("crypto.generate_random_sign_keys", cancellationToken);
        }

        public async Task<SignResponse> Sign(SignRequest @params, CancellationToken cancellationToken = default)
        {
            return await _tonClientAdapter.Request<SignRequest, SignResponse>("crypto.sign", @params, cancellationToken);
        }

        public async Task<VerifySignatureResponse> VerifySignature(VerifySignatureRequest @params, CancellationToken cancellationToken = default)
        {
            return await _tonClientAdapter.Request<VerifySignatureRequest, VerifySignatureResponse>("crypto.verify_signature", @params, cancellationToken);
        }

        public async Task<HashResponse> Sha256(HashRequest @params, CancellationToken cancellationToken = default)
        {
            return await _tonClientAdapter.Request<HashRequest, HashResponse>("crypto.sha256", @params, cancellationToken);
        }

        public async Task<HashResponse> Sha512(HashRequest @params, CancellationToken cancellationToken = default)
        {
            return await _tonClientAdapter.Request<HashRequest, HashResponse>("crypto.sha512", @params, cancellationToken);
        }

        public async Task<ScryptResponse> Scrypt(ScryptRequest @params, CancellationToken cancellationToken = default)
        {
            return await _tonClientAdapter.Request<ScryptRequest, ScryptResponse>("crypto.scrypt", @params, cancellationToken);
        }

        public async Task<KeyPair> NaclSignKeypairFromSecretKey(NaclSignKeyPairFromSecretRequest @params, CancellationToken cancellationToken = default)
        {
            return await _tonClientAdapter.Request<NaclSignKeyPairFromSecretRequest, KeyPair>("crypto.nacl_sign_keypair_from_secret_key", @params, cancellationToken);
        }

        public async Task<NaclSignResponse> NaclSign(NaclSignRequest @params, CancellationToken cancellationToken = default)
        {
            return await _tonClientAdapter.Request<NaclSignRequest, NaclSignResponse>("crypto.nacl_sign", @params, cancellationToken);
        }

        public async Task<NaclSignOpenResponse> NaclSignOpen(NaclSignOpenRequest @params, CancellationToken cancellationToken = default)
        {
            return await _tonClientAdapter.Request<NaclSignOpenRequest, NaclSignOpenResponse>("crypto.nacl_sign_open", @params, cancellationToken);
        }

        public async Task<NaclSignDetachedResponse> NaclSignDetached(NaclSignRequest @params, CancellationToken cancellationToken = default)
        {
            return await _tonClientAdapter.Request<NaclSignRequest, NaclSignDetachedResponse>("crypto.nacl_sign_detached", @params, cancellationToken);
        }

        public async Task<KeyPair> NaclBoxKeypair(CancellationToken cancellationToken = default)
        {
            return await _tonClientAdapter.Request<KeyPair>("crypto.nacl_box_keypair", cancellationToken);
        }

        public async Task<KeyPair> NaclBoxKeypairFromSecretKey(NaclBoxKeyPairFromSecretRequest @params, CancellationToken cancellationToken = default)
        {
            return await _tonClientAdapter.Request<NaclBoxKeyPairFromSecretRequest, KeyPair>("crypto.nacl_box_keypair_from_secret_key", @params, cancellationToken);
        }

        public async Task<NaclBoxResponse> NaclBox(NaclBoxRequest @params, CancellationToken cancellationToken = default)
        {
            return await _tonClientAdapter.Request<NaclBoxRequest, NaclBoxResponse>("crypto.nacl_box", @params, cancellationToken);
        }

        public async Task<NaclBoxOpenResponse> NaclBoxOpen(NaclBoxOpenRequest @params, CancellationToken cancellationToken = default)
        {
            return await _tonClientAdapter.Request<NaclBoxOpenRequest, NaclBoxOpenResponse>("crypto.nacl_box_open", @params, cancellationToken);
        }

        public async Task<NaclBoxResponse> NaclSecretBox(NaclSecretBoxRequest @params, CancellationToken cancellationToken = default)
        {
            return await _tonClientAdapter.Request<NaclSecretBoxRequest, NaclBoxResponse>("crypto.nacl_secret_box", @params, cancellationToken);
        }

        public async Task<NaclBoxOpenResponse> NaclSecretBoxOpen(NaclSecretBoxOpenRequest @params, CancellationToken cancellationToken = default)
        {
            return await _tonClientAdapter.Request<NaclSecretBoxOpenRequest, NaclBoxOpenResponse>("crypto.nacl_secret_box_open", @params, cancellationToken);
        }

        public async Task<MnemonicWordsResponse> MnemonicWords(MnemonicWordsRequest @params, CancellationToken cancellationToken = default)
        {
            return await _tonClientAdapter.Request<MnemonicWordsRequest, MnemonicWordsResponse>("crypto.mnemonic_words", @params, cancellationToken);
        }

        public async Task<MnemonicFromRandomResponse> MnemonicFromRandom(MnemonicFromRandomRequest @params, CancellationToken cancellationToken = default)
        {
            return await _tonClientAdapter.Request<MnemonicFromRandomRequest, MnemonicFromRandomResponse>("crypto.mnemonic_from_random", @params, cancellationToken);
        }

        public async Task<MnemonicFromEntropyResponse> MnemonicFromEntropy(MnemonicFromEntropyRequest @params, CancellationToken cancellationToken = default)
        {
            return await _tonClientAdapter.Request<MnemonicFromEntropyRequest, MnemonicFromEntropyResponse>("crypto.mnemonic_from_entropy", @params, cancellationToken);
        }

        public async Task<MnemonicVerifyResponse> MnemonicVerify(MnemonicVerifyRequest @params, CancellationToken cancellationToken = default)
        {
            return await _tonClientAdapter.Request<MnemonicVerifyRequest, MnemonicVerifyResponse>("crypto.mnemonic_verify", @params, cancellationToken);
        }

        public async Task<KeyPair> MnemonicDeriveSignKeys(MnemonicDeriveSignKeysRequest @params, CancellationToken cancellationToken = default)
        {
            return await _tonClientAdapter.Request<MnemonicDeriveSignKeysRequest, KeyPair>("crypto.mnemonic_derive_sign_keys", @params, cancellationToken);
        }

        public async Task<HDKeyXPrvFromMnemonicResponse> HdkeyXprvFromMnemonic(HDKeyXPrvFromMnemonicRequest @params, CancellationToken cancellationToken = default)
        {
            return await _tonClientAdapter.Request<HDKeyXPrvFromMnemonicRequest, HDKeyXPrvFromMnemonicResponse>("crypto.hdkey_xprv_from_mnemonic", @params, cancellationToken);
        }

        public async Task<HDKeyDeriveFromXPrvResponse> HdkeyDeriveFromXprv(HDKeyDeriveFromXPrvRequest @params, CancellationToken cancellationToken = default)
        {
            return await _tonClientAdapter.Request<HDKeyDeriveFromXPrvRequest, HDKeyDeriveFromXPrvResponse>("crypto.hdkey_derive_from_xprv", @params, cancellationToken);
        }

        public async Task<HDKeyDeriveFromXPrvPathResponse> HdkeyDeriveFromXprvPath(HDKeyDeriveFromXPrvPathRequest @params, CancellationToken cancellationToken = default)
        {
            return await _tonClientAdapter.Request<HDKeyDeriveFromXPrvPathRequest, HDKeyDeriveFromXPrvPathResponse>("crypto.hdkey_derive_from_xprv_path", @params, cancellationToken);
        }

        public async Task<HDKeySecretFromXPrvResponse> HdkeySecretFromXprv(HDKeySecretFromXPrvRequest @params, CancellationToken cancellationToken = default)
        {
            return await _tonClientAdapter.Request<HDKeySecretFromXPrvRequest, HDKeySecretFromXPrvResponse>("crypto.hdkey_secret_from_xprv", @params, cancellationToken);
        }

        public async Task<HDKeyPublicFromXPrvResponse> HdkeyPublicFromXprv(HDKeyPublicFromXPrvRequest @params, CancellationToken cancellationToken = default)
        {
            return await _tonClientAdapter.Request<HDKeyPublicFromXPrvRequest, HDKeyPublicFromXPrvResponse>("crypto.hdkey_public_from_xprv", @params, cancellationToken);
        }

        public async Task<ChaCha20Response> Chacha20(ChaCha20Request @params, CancellationToken cancellationToken = default)
        {
            return await _tonClientAdapter.Request<ChaCha20Request, ChaCha20Response>("crypto.chacha20", @params, cancellationToken);
        }
    }
}