using ch1seL.TonNet.Client.Models;
using System;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;

namespace ch1seL.TonNet.Abstract.Modules
{
    public interface ICryptoModule : ITonModule
    {
        /// <summary>
        /// <para>Integer factorization</para>
        /// <para>Performs prime factorization – decomposition of a composite number</para>
        /// <para>into a product of smaller prime integers (factors).</para>
        /// <para>See [https://en.wikipedia.org/wiki/Integer_factorization]</para>
        /// </summary>
        public Task<ResultOfFactorize> Factorize(ParamsOfFactorize @params, CancellationToken cancellationToken = default);

        /// <summary>
        /// <para>Modular exponentiation</para>
        /// <para>Performs modular exponentiation for big integers (`base`^`exponent` mod `modulus`).</para>
        /// <para>See [https://en.wikipedia.org/wiki/Modular_exponentiation]</para>
        /// </summary>
        public Task<ResultOfModularPower> ModularPower(ParamsOfModularPower @params, CancellationToken cancellationToken = default);

        /// <summary>
        /// Calculates CRC16 using TON algorithm.
        /// </summary>
        public Task<ResultOfTonCrc16> TonCrc16(ParamsOfTonCrc16 @params, CancellationToken cancellationToken = default);

        /// <summary>
        /// Generates random byte array of the specified length and returns it in `base64` format
        /// </summary>
        public Task<ResultOfGenerateRandomBytes> GenerateRandomBytes(ParamsOfGenerateRandomBytes @params, CancellationToken cancellationToken = default);

        /// <summary>
        /// Converts public key to ton safe_format
        /// </summary>
        public Task<ResultOfConvertPublicKeyToTonSafeFormat> ConvertPublicKeyToTonSafeFormat(ParamsOfConvertPublicKeyToTonSafeFormat @params, CancellationToken cancellationToken = default);

        /// <summary>
        /// Generates random ed25519 key pair.
        /// </summary>
        public Task<KeyPair> GenerateRandomSignKeys(CancellationToken cancellationToken = default);

        /// <summary>
        /// Signs a data using the provided keys.
        /// </summary>
        public Task<ResultOfSign> Sign(ParamsOfSign @params, CancellationToken cancellationToken = default);

        /// <summary>
        /// Verifies signed data using the provided public key. Raises error if verification is failed.
        /// </summary>
        public Task<ResultOfVerifySignature> VerifySignature(ParamsOfVerifySignature @params, CancellationToken cancellationToken = default);

        /// <summary>
        /// Calculates SHA256 hash of the specified data.
        /// </summary>
        public Task<ResultOfHash> Sha256(ParamsOfHash @params, CancellationToken cancellationToken = default);

        /// <summary>
        /// Calculates SHA512 hash of the specified data.
        /// </summary>
        public Task<ResultOfHash> Sha512(ParamsOfHash @params, CancellationToken cancellationToken = default);

        /// <summary>
        /// <para>Perform `scrypt` encryption</para>
        /// <para>Derives key from `password` and `key` using `scrypt` algorithm.</para>
        /// <para>See [https://en.wikipedia.org/wiki/Scrypt].</para>
        /// <para># Arguments</para>
        /// <para>- `log_n` - The log2 of the Scrypt parameter `N`</para>
        /// <para>- `r` - The Scrypt parameter `r`</para>
        /// <para>- `p` - The Scrypt parameter `p`</para>
        /// <para># Conditions</para>
        /// <para>- `log_n` must be less than `64`</para>
        /// <para>- `r` must be greater than `0` and less than or equal to `4294967295`</para>
        /// <para>- `p` must be greater than `0` and less than `4294967295`</para>
        /// <para># Recommended values sufficient for most use-cases</para>
        /// <para>- `log_n = 15` (`n = 32768`)</para>
        /// <para>- `r = 8`</para>
        /// <para>- `p = 1`</para>
        /// </summary>
        public Task<ResultOfScrypt> Scrypt(ParamsOfScrypt @params, CancellationToken cancellationToken = default);

        /// <summary>
        /// Generates a key pair for signing from the secret key
        /// </summary>
        public Task<KeyPair> NaclSignKeypairFromSecretKey(ParamsOfNaclSignKeyPairFromSecret @params, CancellationToken cancellationToken = default);

        /// <summary>
        /// Signs data using the signer's secret key.
        /// </summary>
        public Task<ResultOfNaclSign> NaclSign(ParamsOfNaclSign @params, CancellationToken cancellationToken = default);

        /// <summary>
        /// <para>Verifies the signature and returns the unsigned message</para>
        /// <para>Verifies the signature in `signed` using the signer's public key `public`</para>
        /// <para>and returns the message `unsigned`.</para>
        /// <para>If the signature fails verification, crypto_sign_open raises an exception.</para>
        /// </summary>
        public Task<ResultOfNaclSignOpen> NaclSignOpen(ParamsOfNaclSignOpen @params, CancellationToken cancellationToken = default);

        /// <summary>
        /// <para>Signs the message using the secret key and returns a signature.</para>
        /// <para>Signs the message `unsigned` using the secret key `secret`</para>
        /// <para>and returns a signature `signature`.</para>
        /// </summary>
        public Task<ResultOfNaclSignDetached> NaclSignDetached(ParamsOfNaclSign @params, CancellationToken cancellationToken = default);

        /// <summary>
        /// Verifies the signature with public key and `unsigned` data.
        /// </summary>
        public Task<ResultOfNaclSignDetachedVerify> NaclSignDetachedVerify(ParamsOfNaclSignDetachedVerify @params, CancellationToken cancellationToken = default);

        /// <summary>
        /// Generates a random NaCl key pair
        /// </summary>
        public Task<KeyPair> NaclBoxKeypair(CancellationToken cancellationToken = default);

        /// <summary>
        /// Generates key pair from a secret key
        /// </summary>
        public Task<KeyPair> NaclBoxKeypairFromSecretKey(ParamsOfNaclBoxKeyPairFromSecret @params, CancellationToken cancellationToken = default);

        /// <summary>
        /// <para>Public key authenticated encryption</para>
        /// <para>Encrypt and authenticate a message using the senders secret key, the receivers public</para>
        /// <para>key, and a nonce.</para>
        /// </summary>
        public Task<ResultOfNaclBox> NaclBox(ParamsOfNaclBox @params, CancellationToken cancellationToken = default);

        /// <summary>
        /// Decrypt and verify the cipher text using the receivers secret key, the senders public key, and the nonce.
        /// </summary>
        public Task<ResultOfNaclBoxOpen> NaclBoxOpen(ParamsOfNaclBoxOpen @params, CancellationToken cancellationToken = default);

        /// <summary>
        /// Encrypt and authenticate message using nonce and secret key.
        /// </summary>
        public Task<ResultOfNaclBox> NaclSecretBox(ParamsOfNaclSecretBox @params, CancellationToken cancellationToken = default);

        /// <summary>
        /// Decrypts and verifies cipher text using `nonce` and secret `key`.
        /// </summary>
        public Task<ResultOfNaclBoxOpen> NaclSecretBoxOpen(ParamsOfNaclSecretBoxOpen @params, CancellationToken cancellationToken = default);

        /// <summary>
        /// Prints the list of words from the specified dictionary
        /// </summary>
        public Task<ResultOfMnemonicWords> MnemonicWords(ParamsOfMnemonicWords @params, CancellationToken cancellationToken = default);

        /// <summary>
        /// <para>Generates a random mnemonic</para>
        /// <para>Generates a random mnemonic from the specified dictionary and word count</para>
        /// </summary>
        public Task<ResultOfMnemonicFromRandom> MnemonicFromRandom(ParamsOfMnemonicFromRandom @params, CancellationToken cancellationToken = default);

        /// <summary>
        /// Generates mnemonic from pre-generated entropy
        /// </summary>
        public Task<ResultOfMnemonicFromEntropy> MnemonicFromEntropy(ParamsOfMnemonicFromEntropy @params, CancellationToken cancellationToken = default);

        /// <summary>
        /// <para>Validates a mnemonic phrase</para>
        /// <para>The phrase supplied will be checked for word length and validated according to the checksum</para>
        /// <para>specified in BIP0039.</para>
        /// </summary>
        public Task<ResultOfMnemonicVerify> MnemonicVerify(ParamsOfMnemonicVerify @params, CancellationToken cancellationToken = default);

        /// <summary>
        /// <para>Derives a key pair for signing from the seed phrase</para>
        /// <para>Validates the seed phrase, generates master key and then derives</para>
        /// <para>the key pair from the master key and the specified path</para>
        /// </summary>
        public Task<KeyPair> MnemonicDeriveSignKeys(ParamsOfMnemonicDeriveSignKeys @params, CancellationToken cancellationToken = default);

        /// <summary>
        /// Generates an extended master private key that will be the root for all the derived keys
        /// </summary>
        public Task<ResultOfHDKeyXPrvFromMnemonic> HdkeyXprvFromMnemonic(ParamsOfHDKeyXPrvFromMnemonic @params, CancellationToken cancellationToken = default);

        /// <summary>
        /// Returns extended private key derived from the specified extended private key and child index
        /// </summary>
        public Task<ResultOfHDKeyDeriveFromXPrv> HdkeyDeriveFromXprv(ParamsOfHDKeyDeriveFromXPrv @params, CancellationToken cancellationToken = default);

        /// <summary>
        /// Derives the extended private key from the specified key and path
        /// </summary>
        public Task<ResultOfHDKeyDeriveFromXPrvPath> HdkeyDeriveFromXprvPath(ParamsOfHDKeyDeriveFromXPrvPath @params, CancellationToken cancellationToken = default);

        /// <summary>
        /// Extracts the private key from the serialized extended private key
        /// </summary>
        public Task<ResultOfHDKeySecretFromXPrv> HdkeySecretFromXprv(ParamsOfHDKeySecretFromXPrv @params, CancellationToken cancellationToken = default);

        /// <summary>
        /// Extracts the public key from the serialized extended private key
        /// </summary>
        public Task<ResultOfHDKeyPublicFromXPrv> HdkeyPublicFromXprv(ParamsOfHDKeyPublicFromXPrv @params, CancellationToken cancellationToken = default);

        /// <summary>
        /// Performs symmetric `chacha20` encryption.
        /// </summary>
        public Task<ResultOfChaCha20> Chacha20(ParamsOfChaCha20 @params, CancellationToken cancellationToken = default);

        /// <summary>
        /// Register an application implemented signing box.
        /// </summary>
        public Task<RegisteredSigningBox> RegisterSigningBox(Action<JsonElement,uint> appObject = null, CancellationToken cancellationToken = default);

        /// <summary>
        /// Creates a default signing box implementation.
        /// </summary>
        public Task<RegisteredSigningBox> GetSigningBox(KeyPair @params, CancellationToken cancellationToken = default);

        /// <summary>
        /// Returns public key of signing key pair.
        /// </summary>
        public Task<ResultOfSigningBoxGetPublicKey> SigningBoxGetPublicKey(RegisteredSigningBox @params, CancellationToken cancellationToken = default);

        /// <summary>
        /// Returns signed user data.
        /// </summary>
        public Task<ResultOfSigningBoxSign> SigningBoxSign(ParamsOfSigningBoxSign @params, CancellationToken cancellationToken = default);

        /// <summary>
        /// Removes signing box from SDK.
        /// </summary>
        public Task RemoveSigningBox(RegisteredSigningBox @params, CancellationToken cancellationToken = default);

        /// <summary>
        /// Register an application implemented encryption box.
        /// </summary>
        public Task<RegisteredEncryptionBox> RegisterEncryptionBox(Action<JsonElement,uint> appObject = null, CancellationToken cancellationToken = default);

        /// <summary>
        /// Removes encryption box from SDK
        /// </summary>
        public Task RemoveEncryptionBox(RegisteredEncryptionBox @params, CancellationToken cancellationToken = default);

        /// <summary>
        /// Queries info from the given encryption box
        /// </summary>
        public Task<ResultOfEncryptionBoxGetInfo> EncryptionBoxGetInfo(ParamsOfEncryptionBoxGetInfo @params, CancellationToken cancellationToken = default);

        /// <summary>
        /// Encrypts data using given encryption box
        /// </summary>
        public Task<ResultOfEncryptionBoxEncrypt> EncryptionBoxEncrypt(ParamsOfEncryptionBoxEncrypt @params, CancellationToken cancellationToken = default);

        /// <summary>
        /// Decrypts data using given encryption box
        /// </summary>
        public Task<ResultOfEncryptionBoxDecrypt> EncryptionBoxDecrypt(ParamsOfEncryptionBoxDecrypt @params, CancellationToken cancellationToken = default);
    }
}