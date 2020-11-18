using ch1seL.TonNet.Abstract;
using ch1seL.TonNet.Client.Models;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace ch1seL.TonNet.Client
{
    public class CryptoModule : ICryptoModule
    {
        private readonly ITonClientAdapter _tonClientAdapter;

        public CryptoModule(ITonClientAdapter tonClientAdapter)
        {
            _tonClientAdapter = tonClientAdapter;
        }

        /// <summary>
        /// <para> Performs prime factorization – decomposition of a composite number</para>
        /// <para> into a product of smaller prime integers (factors).</para>
        /// <para> See [https://en.wikipedia.org/wiki/Integer_factorization]</para>
        /// </summary>
        public async Task<FactorizeResponse> Factorize(FactorizeRequest @params, CancellationToken cancellationToken = default)
        {
            return await _tonClientAdapter.Request<FactorizeRequest, FactorizeResponse>("crypto.factorize", @params, cancellationToken);
        }

        /// <summary>
        /// <para> Performs modular exponentiation for big integers (`base`^`exponent` mod `modulus`).</para>
        /// <para> See [https://en.wikipedia.org/wiki/Modular_exponentiation]</para>
        /// </summary>
        public async Task<ModularPowerResponse> ModularPower(ModularPowerRequest @params, CancellationToken cancellationToken = default)
        {
            return await _tonClientAdapter.Request<ModularPowerRequest, ModularPowerResponse>("crypto.modular_power", @params, cancellationToken);
        }

        /// <summary>
        ///  Calculates CRC16 using TON algorithm.
        /// </summary>
        public async Task<TonCrc16Response> TonCrc16(TonCrc16Request @params, CancellationToken cancellationToken = default)
        {
            return await _tonClientAdapter.Request<TonCrc16Request, TonCrc16Response>("crypto.ton_crc16", @params, cancellationToken);
        }

        /// <summary>
        ///  Generates random byte array of the specified length and returns it in `base64` format
        /// </summary>
        public async Task<GenerateRandomBytesResponse> GenerateRandomBytes(GenerateRandomBytesRequest @params, CancellationToken cancellationToken = default)
        {
            return await _tonClientAdapter.Request<GenerateRandomBytesRequest, GenerateRandomBytesResponse>("crypto.generate_random_bytes", @params, cancellationToken);
        }

        /// <summary>
        ///  Converts public key to ton safe_format
        /// </summary>
        public async Task<ConvertPublicKeyToTonSafeFormatResponse> ConvertPublicKeyToTonSafeFormat(ConvertPublicKeyToTonSafeFormatRequest @params, CancellationToken cancellationToken = default)
        {
            return await _tonClientAdapter.Request<ConvertPublicKeyToTonSafeFormatRequest, ConvertPublicKeyToTonSafeFormatResponse>("crypto.convert_public_key_to_ton_safe_format", @params, cancellationToken);
        }

        /// <summary>
        ///  Generates random ed25519 key pair.
        /// </summary>
        public async Task<KeyPair> GenerateRandomSignKeys(CancellationToken cancellationToken = default)
        {
            return await _tonClientAdapter.Request<KeyPair>("crypto.generate_random_sign_keys", cancellationToken);
        }

        /// <summary>
        ///  Signs a data using the provided keys.
        /// </summary>
        public async Task<SignResponse> Sign(SignRequest @params, CancellationToken cancellationToken = default)
        {
            return await _tonClientAdapter.Request<SignRequest, SignResponse>("crypto.sign", @params, cancellationToken);
        }

        /// <summary>
        /// <para> Verifies signed data using the provided public key.</para>
        /// <para> Raises error if verification is failed.</para>
        /// </summary>
        public async Task<VerifySignatureResponse> VerifySignature(VerifySignatureRequest @params, CancellationToken cancellationToken = default)
        {
            return await _tonClientAdapter.Request<VerifySignatureRequest, VerifySignatureResponse>("crypto.verify_signature", @params, cancellationToken);
        }

        /// <summary>
        ///  Calculates SHA256 hash of the specified data.
        /// </summary>
        public async Task<HashResponse> Sha256(HashRequest @params, CancellationToken cancellationToken = default)
        {
            return await _tonClientAdapter.Request<HashRequest, HashResponse>("crypto.sha256", @params, cancellationToken);
        }

        /// <summary>
        ///  Calculates SHA512 hash of the specified data.
        /// </summary>
        public async Task<HashResponse> Sha512(HashRequest @params, CancellationToken cancellationToken = default)
        {
            return await _tonClientAdapter.Request<HashRequest, HashResponse>("crypto.sha512", @params, cancellationToken);
        }

        /// <summary>
        /// <para> Derives key from `password` and `key` using `scrypt` algorithm.</para>
        /// <para> See [https://en.wikipedia.org/wiki/Scrypt].</para>
        /// <para> # Arguments</para>
        /// <para> - `log_n` - The log2 of the Scrypt parameter `N`</para>
        /// <para> - `r` - The Scrypt parameter `r`</para>
        /// <para> - `p` - The Scrypt parameter `p`</para>
        /// <para> # Conditions</para>
        /// <para> - `log_n` must be less than `64`</para>
        /// <para> - `r` must be greater than `0` and less than or equal to `4294967295`</para>
        /// <para> - `p` must be greater than `0` and less than `4294967295`</para>
        /// <para> # Recommended values sufficient for most use-cases</para>
        /// <para> - `log_n = 15` (`n = 32768`)</para>
        /// <para> - `r = 8`</para>
        /// <para> - `p = 1`</para>
        /// </summary>
        public async Task<ScryptResponse> Scrypt(ScryptRequest @params, CancellationToken cancellationToken = default)
        {
            return await _tonClientAdapter.Request<ScryptRequest, ScryptResponse>("crypto.scrypt", @params, cancellationToken);
        }

        /// <summary>
        ///  Generates a key pair for signing from the secret key
        /// </summary>
        public async Task<KeyPair> NaclSignKeypairFromSecretKey(NaclSignKeyPairFromSecretRequest @params, CancellationToken cancellationToken = default)
        {
            return await _tonClientAdapter.Request<NaclSignKeyPairFromSecretRequest, KeyPair>("crypto.nacl_sign_keypair_from_secret_key", @params, cancellationToken);
        }

        /// <summary>
        ///  Signs data using the signer's secret key.
        /// </summary>
        public async Task<NaclSignResponse> NaclSign(NaclSignRequest @params, CancellationToken cancellationToken = default)
        {
            return await _tonClientAdapter.Request<NaclSignRequest, NaclSignResponse>("crypto.nacl_sign", @params, cancellationToken);
        }

        /// <summary>
        /// Not described yet..
        /// </summary>
        public async Task<NaclSignOpenResponse> NaclSignOpen(NaclSignOpenRequest @params, CancellationToken cancellationToken = default)
        {
            return await _tonClientAdapter.Request<NaclSignOpenRequest, NaclSignOpenResponse>("crypto.nacl_sign_open", @params, cancellationToken);
        }

        /// <summary>
        /// Not described yet..
        /// </summary>
        public async Task<NaclSignDetachedResponse> NaclSignDetached(NaclSignRequest @params, CancellationToken cancellationToken = default)
        {
            return await _tonClientAdapter.Request<NaclSignRequest, NaclSignDetachedResponse>("crypto.nacl_sign_detached", @params, cancellationToken);
        }

        /// <summary>
        /// Not described yet..
        /// </summary>
        public async Task<KeyPair> NaclBoxKeypair(CancellationToken cancellationToken = default)
        {
            return await _tonClientAdapter.Request<KeyPair>("crypto.nacl_box_keypair", cancellationToken);
        }

        /// <summary>
        ///  Generates key pair from a secret key
        /// </summary>
        public async Task<KeyPair> NaclBoxKeypairFromSecretKey(NaclBoxKeyPairFromSecretRequest @params, CancellationToken cancellationToken = default)
        {
            return await _tonClientAdapter.Request<NaclBoxKeyPairFromSecretRequest, KeyPair>("crypto.nacl_box_keypair_from_secret_key", @params, cancellationToken);
        }

        /// <summary>
        /// <para> Public key authenticated encryption</para>
        /// <para> Encrypt and authenticate a message using the senders secret key, the recievers public</para>
        /// <para> key, and a nonce. </para>
        /// </summary>
        public async Task<NaclBoxResponse> NaclBox(NaclBoxRequest @params, CancellationToken cancellationToken = default)
        {
            return await _tonClientAdapter.Request<NaclBoxRequest, NaclBoxResponse>("crypto.nacl_box", @params, cancellationToken);
        }

        /// <summary>
        /// <para> Decrypt and verify the cipher text using the recievers secret key, the senders public</para>
        /// <para> key, and the nonce.</para>
        /// </summary>
        public async Task<NaclBoxOpenResponse> NaclBoxOpen(NaclBoxOpenRequest @params, CancellationToken cancellationToken = default)
        {
            return await _tonClientAdapter.Request<NaclBoxOpenRequest, NaclBoxOpenResponse>("crypto.nacl_box_open", @params, cancellationToken);
        }

        /// <summary>
        ///  Encrypt and authenticate message using nonce and secret key.
        /// </summary>
        public async Task<NaclBoxResponse> NaclSecretBox(NaclSecretBoxRequest @params, CancellationToken cancellationToken = default)
        {
            return await _tonClientAdapter.Request<NaclSecretBoxRequest, NaclBoxResponse>("crypto.nacl_secret_box", @params, cancellationToken);
        }

        /// <summary>
        ///  Decrypts and verifies cipher text using `nonce` and secret `key`.
        /// </summary>
        public async Task<NaclBoxOpenResponse> NaclSecretBoxOpen(NaclSecretBoxOpenRequest @params, CancellationToken cancellationToken = default)
        {
            return await _tonClientAdapter.Request<NaclSecretBoxOpenRequest, NaclBoxOpenResponse>("crypto.nacl_secret_box_open", @params, cancellationToken);
        }

        /// <summary>
        ///  Prints the list of words from the specified dictionary
        /// </summary>
        public async Task<MnemonicWordsResponse> MnemonicWords(MnemonicWordsRequest @params, CancellationToken cancellationToken = default)
        {
            return await _tonClientAdapter.Request<MnemonicWordsRequest, MnemonicWordsResponse>("crypto.mnemonic_words", @params, cancellationToken);
        }

        /// <summary>
        ///  Generates a random mnemonic from the specified dictionary and word count
        /// </summary>
        public async Task<MnemonicFromRandomResponse> MnemonicFromRandom(MnemonicFromRandomRequest @params, CancellationToken cancellationToken = default)
        {
            return await _tonClientAdapter.Request<MnemonicFromRandomRequest, MnemonicFromRandomResponse>("crypto.mnemonic_from_random", @params, cancellationToken);
        }

        /// <summary>
        ///  Generates mnemonic from pre-generated entropy
        /// </summary>
        public async Task<MnemonicFromEntropyResponse> MnemonicFromEntropy(MnemonicFromEntropyRequest @params, CancellationToken cancellationToken = default)
        {
            return await _tonClientAdapter.Request<MnemonicFromEntropyRequest, MnemonicFromEntropyResponse>("crypto.mnemonic_from_entropy", @params, cancellationToken);
        }

        /// <summary>
        /// <para> The phrase supplied will be checked for word length and validated according to the checksum</para>
        /// <para> specified in BIP0039.</para>
        /// </summary>
        public async Task<MnemonicVerifyResponse> MnemonicVerify(MnemonicVerifyRequest @params, CancellationToken cancellationToken = default)
        {
            return await _tonClientAdapter.Request<MnemonicVerifyRequest, MnemonicVerifyResponse>("crypto.mnemonic_verify", @params, cancellationToken);
        }

        /// <summary>
        /// <para> Validates the seed phrase, generates master key and then derives</para>
        /// <para> the key pair from the master key and the specified path</para>
        /// </summary>
        public async Task<KeyPair> MnemonicDeriveSignKeys(MnemonicDeriveSignKeysRequest @params, CancellationToken cancellationToken = default)
        {
            return await _tonClientAdapter.Request<MnemonicDeriveSignKeysRequest, KeyPair>("crypto.mnemonic_derive_sign_keys", @params, cancellationToken);
        }

        /// <summary>
        ///  Generates an extended master private key that will be the root for all the derived keys
        /// </summary>
        public async Task<HDKeyXPrvFromMnemonicResponse> HdkeyXprvFromMnemonic(HDKeyXPrvFromMnemonicRequest @params, CancellationToken cancellationToken = default)
        {
            return await _tonClientAdapter.Request<HDKeyXPrvFromMnemonicRequest, HDKeyXPrvFromMnemonicResponse>("crypto.hdkey_xprv_from_mnemonic", @params, cancellationToken);
        }

        /// <summary>
        ///  Returns extended private key derived from the specified extended private key and child index
        /// </summary>
        public async Task<HDKeyDeriveFromXPrvResponse> HdkeyDeriveFromXprv(HDKeyDeriveFromXPrvRequest @params, CancellationToken cancellationToken = default)
        {
            return await _tonClientAdapter.Request<HDKeyDeriveFromXPrvRequest, HDKeyDeriveFromXPrvResponse>("crypto.hdkey_derive_from_xprv", @params, cancellationToken);
        }

        /// <summary>
        ///  Derives the exented private key from the specified key and path
        /// </summary>
        public async Task<HDKeyDeriveFromXPrvPathResponse> HdkeyDeriveFromXprvPath(HDKeyDeriveFromXPrvPathRequest @params, CancellationToken cancellationToken = default)
        {
            return await _tonClientAdapter.Request<HDKeyDeriveFromXPrvPathRequest, HDKeyDeriveFromXPrvPathResponse>("crypto.hdkey_derive_from_xprv_path", @params, cancellationToken);
        }

        /// <summary>
        ///  Extracts the private key from the serialized extended private key
        /// </summary>
        public async Task<HDKeySecretFromXPrvResponse> HdkeySecretFromXprv(HDKeySecretFromXPrvRequest @params, CancellationToken cancellationToken = default)
        {
            return await _tonClientAdapter.Request<HDKeySecretFromXPrvRequest, HDKeySecretFromXPrvResponse>("crypto.hdkey_secret_from_xprv", @params, cancellationToken);
        }

        /// <summary>
        ///  Extracts the public key from the serialized extended private key
        /// </summary>
        public async Task<HDKeyPublicFromXPrvResponse> HdkeyPublicFromXprv(HDKeyPublicFromXPrvRequest @params, CancellationToken cancellationToken = default)
        {
            return await _tonClientAdapter.Request<HDKeyPublicFromXPrvRequest, HDKeyPublicFromXPrvResponse>("crypto.hdkey_public_from_xprv", @params, cancellationToken);
        }

        /// <summary>
        ///  Performs symmetric `chacha20` encryption.
        /// </summary>
        public async Task<ChaCha20Response> Chacha20(ChaCha20Request @params, CancellationToken cancellationToken = default)
        {
            return await _tonClientAdapter.Request<ChaCha20Request, ChaCha20Response>("crypto.chacha20", @params, cancellationToken);
        }
    }
}