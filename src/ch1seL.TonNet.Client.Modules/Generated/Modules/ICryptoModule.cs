using ch1seL.TonNet.Abstract;
using ch1seL.TonNet.Client.Models;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace ch1seL.TonNet.Client
{
    public interface ICryptoModule : ITonModule
    {
        /// <summary>
        /// <para> Performs prime factorization – decomposition of a composite number</para>
        /// <para> into a product of smaller prime integers (factors).</para>
        /// <para> See [https://en.wikipedia.org/wiki/Integer_factorization]</para>
        /// </summary>
        public Task<FactorizeResponse> Factorize(FactorizeRequest @params, CancellationToken cancellationToken = default);

        /// <summary>
        /// <para> Performs modular exponentiation for big integers (`base`^`exponent` mod `modulus`).</para>
        /// <para> See [https://en.wikipedia.org/wiki/Modular_exponentiation]</para>
        /// </summary>
        public Task<ModularPowerResponse> ModularPower(ModularPowerRequest @params, CancellationToken cancellationToken = default);

        /// <summary>
        ///  Calculates CRC16 using TON algorithm.
        /// </summary>
        public Task<TonCrc16Response> TonCrc16(TonCrc16Request @params, CancellationToken cancellationToken = default);

        /// <summary>
        ///  Generates random byte array of the specified length and returns it in `base64` format
        /// </summary>
        public Task<GenerateRandomBytesResponse> GenerateRandomBytes(GenerateRandomBytesRequest @params, CancellationToken cancellationToken = default);

        /// <summary>
        ///  Converts public key to ton safe_format
        /// </summary>
        public Task<ConvertPublicKeyToTonSafeFormatResponse> ConvertPublicKeyToTonSafeFormat(ConvertPublicKeyToTonSafeFormatRequest @params, CancellationToken cancellationToken = default);

        /// <summary>
        ///  Generates random ed25519 key pair.
        /// </summary>
        public Task<KeyPair> GenerateRandomSignKeys(CancellationToken cancellationToken = default);

        /// <summary>
        ///  Signs a data using the provided keys.
        /// </summary>
        public Task<SignResponse> Sign(SignRequest @params, CancellationToken cancellationToken = default);

        /// <summary>
        /// <para> Verifies signed data using the provided public key.</para>
        /// <para> Raises error if verification is failed.</para>
        /// </summary>
        public Task<VerifySignatureResponse> VerifySignature(VerifySignatureRequest @params, CancellationToken cancellationToken = default);

        /// <summary>
        ///  Calculates SHA256 hash of the specified data.
        /// </summary>
        public Task<HashResponse> Sha256(HashRequest @params, CancellationToken cancellationToken = default);

        /// <summary>
        ///  Calculates SHA512 hash of the specified data.
        /// </summary>
        public Task<HashResponse> Sha512(HashRequest @params, CancellationToken cancellationToken = default);

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
        public Task<ScryptResponse> Scrypt(ScryptRequest @params, CancellationToken cancellationToken = default);

        /// <summary>
        ///  Generates a key pair for signing from the secret key
        /// </summary>
        public Task<KeyPair> NaclSignKeypairFromSecretKey(NaclSignKeyPairFromSecretRequest @params, CancellationToken cancellationToken = default);

        /// <summary>
        ///  Signs data using the signer's secret key.
        /// </summary>
        public Task<NaclSignResponse> NaclSign(NaclSignRequest @params, CancellationToken cancellationToken = default);

        /// <summary>
        /// Not described yet..
        /// </summary>
        public Task<NaclSignOpenResponse> NaclSignOpen(NaclSignOpenRequest @params, CancellationToken cancellationToken = default);

        /// <summary>
        /// Not described yet..
        /// </summary>
        public Task<NaclSignDetachedResponse> NaclSignDetached(NaclSignRequest @params, CancellationToken cancellationToken = default);

        /// <summary>
        /// Not described yet..
        /// </summary>
        public Task<KeyPair> NaclBoxKeypair(CancellationToken cancellationToken = default);

        /// <summary>
        ///  Generates key pair from a secret key
        /// </summary>
        public Task<KeyPair> NaclBoxKeypairFromSecretKey(NaclBoxKeyPairFromSecretRequest @params, CancellationToken cancellationToken = default);

        /// <summary>
        /// <para> Public key authenticated encryption</para>
        /// <para> Encrypt and authenticate a message using the senders secret key, the recievers public</para>
        /// <para> key, and a nonce. </para>
        /// </summary>
        public Task<NaclBoxResponse> NaclBox(NaclBoxRequest @params, CancellationToken cancellationToken = default);

        /// <summary>
        /// <para> Decrypt and verify the cipher text using the recievers secret key, the senders public</para>
        /// <para> key, and the nonce.</para>
        /// </summary>
        public Task<NaclBoxOpenResponse> NaclBoxOpen(NaclBoxOpenRequest @params, CancellationToken cancellationToken = default);

        /// <summary>
        ///  Encrypt and authenticate message using nonce and secret key.
        /// </summary>
        public Task<NaclBoxResponse> NaclSecretBox(NaclSecretBoxRequest @params, CancellationToken cancellationToken = default);

        /// <summary>
        ///  Decrypts and verifies cipher text using `nonce` and secret `key`.
        /// </summary>
        public Task<NaclBoxOpenResponse> NaclSecretBoxOpen(NaclSecretBoxOpenRequest @params, CancellationToken cancellationToken = default);

        /// <summary>
        ///  Prints the list of words from the specified dictionary
        /// </summary>
        public Task<MnemonicWordsResponse> MnemonicWords(MnemonicWordsRequest @params, CancellationToken cancellationToken = default);

        /// <summary>
        ///  Generates a random mnemonic from the specified dictionary and word count
        /// </summary>
        public Task<MnemonicFromRandomResponse> MnemonicFromRandom(MnemonicFromRandomRequest @params, CancellationToken cancellationToken = default);

        /// <summary>
        ///  Generates mnemonic from pre-generated entropy
        /// </summary>
        public Task<MnemonicFromEntropyResponse> MnemonicFromEntropy(MnemonicFromEntropyRequest @params, CancellationToken cancellationToken = default);

        /// <summary>
        /// <para> The phrase supplied will be checked for word length and validated according to the checksum</para>
        /// <para> specified in BIP0039.</para>
        /// </summary>
        public Task<MnemonicVerifyResponse> MnemonicVerify(MnemonicVerifyRequest @params, CancellationToken cancellationToken = default);

        /// <summary>
        /// <para> Validates the seed phrase, generates master key and then derives</para>
        /// <para> the key pair from the master key and the specified path</para>
        /// </summary>
        public Task<KeyPair> MnemonicDeriveSignKeys(MnemonicDeriveSignKeysRequest @params, CancellationToken cancellationToken = default);

        /// <summary>
        ///  Generates an extended master private key that will be the root for all the derived keys
        /// </summary>
        public Task<HDKeyXPrvFromMnemonicResponse> HdkeyXprvFromMnemonic(HDKeyXPrvFromMnemonicRequest @params, CancellationToken cancellationToken = default);

        /// <summary>
        ///  Returns extended private key derived from the specified extended private key and child index
        /// </summary>
        public Task<HDKeyDeriveFromXPrvResponse> HdkeyDeriveFromXprv(HDKeyDeriveFromXPrvRequest @params, CancellationToken cancellationToken = default);

        /// <summary>
        ///  Derives the exented private key from the specified key and path
        /// </summary>
        public Task<HDKeyDeriveFromXPrvPathResponse> HdkeyDeriveFromXprvPath(HDKeyDeriveFromXPrvPathRequest @params, CancellationToken cancellationToken = default);

        /// <summary>
        ///  Extracts the private key from the serialized extended private key
        /// </summary>
        public Task<HDKeySecretFromXPrvResponse> HdkeySecretFromXprv(HDKeySecretFromXPrvRequest @params, CancellationToken cancellationToken = default);

        /// <summary>
        ///  Extracts the public key from the serialized extended private key
        /// </summary>
        public Task<HDKeyPublicFromXPrvResponse> HdkeyPublicFromXprv(HDKeyPublicFromXPrvRequest @params, CancellationToken cancellationToken = default);

        /// <summary>
        ///  Performs symmetric `chacha20` encryption.
        /// </summary>
        public Task<ChaCha20Response> Chacha20(ChaCha20Request @params, CancellationToken cancellationToken = default);
    }
}