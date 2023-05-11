using EverscaleNet.Client.Models;
using System;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;

namespace EverscaleNet.Abstract.Modules
{
    /// <summary>
    /// <para>Crypto Module</para>
    /// </summary>
    public interface ICryptoModule : IEverModule
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
        /// <para>Calculates CRC16 using TON algorithm.</para>
        /// </summary>
        public Task<ResultOfTonCrc16> TonCrc16(ParamsOfTonCrc16 @params, CancellationToken cancellationToken = default);

        /// <summary>
        /// <para>Generates random byte array of the specified length and returns it in `base64` format</para>
        /// </summary>
        public Task<ResultOfGenerateRandomBytes> GenerateRandomBytes(ParamsOfGenerateRandomBytes @params, CancellationToken cancellationToken = default);

        /// <summary>
        /// <para>Converts public key to ton safe_format</para>
        /// </summary>
        public Task<ResultOfConvertPublicKeyToTonSafeFormat> ConvertPublicKeyToTonSafeFormat(ParamsOfConvertPublicKeyToTonSafeFormat @params, CancellationToken cancellationToken = default);

        /// <summary>
        /// <para>Generates random ed25519 key pair.</para>
        /// </summary>
        public Task<KeyPair> GenerateRandomSignKeys(CancellationToken cancellationToken = default);

        /// <summary>
        /// <para>Signs a data using the provided keys.</para>
        /// </summary>
        public Task<ResultOfSign> Sign(ParamsOfSign @params, CancellationToken cancellationToken = default);

        /// <summary>
        /// <para>Verifies signed data using the provided public key. Raises error if verification is failed.</para>
        /// </summary>
        public Task<ResultOfVerifySignature> VerifySignature(ParamsOfVerifySignature @params, CancellationToken cancellationToken = default);

        /// <summary>
        /// <para>Calculates SHA256 hash of the specified data.</para>
        /// </summary>
        public Task<ResultOfHash> Sha256(ParamsOfHash @params, CancellationToken cancellationToken = default);

        /// <summary>
        /// <para>Calculates SHA512 hash of the specified data.</para>
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
        /// <para>Generates a key pair for signing from the secret key</para>
        /// <para>**NOTE:** In the result the secret key is actually the concatenation</para>
        /// <para>of secret and public keys (128 symbols hex string) by design of [NaCL](http://nacl.cr.yp.to/sign.html).</para>
        /// <para>See also [the stackexchange question](https://crypto.stackexchange.com/questions/54353/).</para>
        /// </summary>
        public Task<KeyPair> NaclSignKeypairFromSecretKey(ParamsOfNaclSignKeyPairFromSecret @params, CancellationToken cancellationToken = default);

        /// <summary>
        /// <para>Signs data using the signer's secret key.</para>
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
        /// <para>Verifies the signature with public key and `unsigned` data.</para>
        /// </summary>
        public Task<ResultOfNaclSignDetachedVerify> NaclSignDetachedVerify(ParamsOfNaclSignDetachedVerify @params, CancellationToken cancellationToken = default);

        /// <summary>
        /// <para>Generates a random NaCl key pair</para>
        /// </summary>
        public Task<KeyPair> NaclBoxKeypair(CancellationToken cancellationToken = default);

        /// <summary>
        /// <para>Generates key pair from a secret key</para>
        /// </summary>
        public Task<KeyPair> NaclBoxKeypairFromSecretKey(ParamsOfNaclBoxKeyPairFromSecret @params, CancellationToken cancellationToken = default);

        /// <summary>
        /// <para>Public key authenticated encryption</para>
        /// <para>Encrypt and authenticate a message using the senders secret key, the receivers public</para>
        /// <para>key, and a nonce.</para>
        /// </summary>
        public Task<ResultOfNaclBox> NaclBox(ParamsOfNaclBox @params, CancellationToken cancellationToken = default);

        /// <summary>
        /// <para>Decrypt and verify the cipher text using the receivers secret key, the senders public key, and the nonce.</para>
        /// </summary>
        public Task<ResultOfNaclBoxOpen> NaclBoxOpen(ParamsOfNaclBoxOpen @params, CancellationToken cancellationToken = default);

        /// <summary>
        /// <para>Encrypt and authenticate message using nonce and secret key.</para>
        /// </summary>
        public Task<ResultOfNaclBox> NaclSecretBox(ParamsOfNaclSecretBox @params, CancellationToken cancellationToken = default);

        /// <summary>
        /// <para>Decrypts and verifies cipher text using `nonce` and secret `key`.</para>
        /// </summary>
        public Task<ResultOfNaclBoxOpen> NaclSecretBoxOpen(ParamsOfNaclSecretBoxOpen @params, CancellationToken cancellationToken = default);

        /// <summary>
        /// <para>Prints the list of words from the specified dictionary</para>
        /// </summary>
        public Task<ResultOfMnemonicWords> MnemonicWords(ParamsOfMnemonicWords @params, CancellationToken cancellationToken = default);

        /// <summary>
        /// <para>Generates a random mnemonic</para>
        /// <para>Generates a random mnemonic from the specified dictionary and word count</para>
        /// </summary>
        public Task<ResultOfMnemonicFromRandom> MnemonicFromRandom(ParamsOfMnemonicFromRandom @params, CancellationToken cancellationToken = default);

        /// <summary>
        /// <para>Generates mnemonic from pre-generated entropy</para>
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
        /// <para>Generates an extended master private key that will be the root for all the derived keys</para>
        /// </summary>
        public Task<ResultOfHDKeyXPrvFromMnemonic> HdkeyXprvFromMnemonic(ParamsOfHDKeyXPrvFromMnemonic @params, CancellationToken cancellationToken = default);

        /// <summary>
        /// <para>Returns extended private key derived from the specified extended private key and child index</para>
        /// </summary>
        public Task<ResultOfHDKeyDeriveFromXPrv> HdkeyDeriveFromXprv(ParamsOfHDKeyDeriveFromXPrv @params, CancellationToken cancellationToken = default);

        /// <summary>
        /// <para>Derives the extended private key from the specified key and path</para>
        /// </summary>
        public Task<ResultOfHDKeyDeriveFromXPrvPath> HdkeyDeriveFromXprvPath(ParamsOfHDKeyDeriveFromXPrvPath @params, CancellationToken cancellationToken = default);

        /// <summary>
        /// <para>Extracts the private key from the serialized extended private key</para>
        /// </summary>
        public Task<ResultOfHDKeySecretFromXPrv> HdkeySecretFromXprv(ParamsOfHDKeySecretFromXPrv @params, CancellationToken cancellationToken = default);

        /// <summary>
        /// <para>Extracts the public key from the serialized extended private key</para>
        /// </summary>
        public Task<ResultOfHDKeyPublicFromXPrv> HdkeyPublicFromXprv(ParamsOfHDKeyPublicFromXPrv @params, CancellationToken cancellationToken = default);

        /// <summary>
        /// <para>Performs symmetric `chacha20` encryption.</para>
        /// </summary>
        public Task<ResultOfChaCha20> Chacha20(ParamsOfChaCha20 @params, CancellationToken cancellationToken = default);

        /// <summary>
        /// <para>Creates a Crypto Box instance.</para>
        /// <para>Crypto Box is a root crypto object, that encapsulates some secret (seed phrase usually)</para>
        /// <para>in encrypted form and acts as a factory for all crypto primitives used in SDK:</para>
        /// <para>keys for signing and encryption, derived from this secret.</para>
        /// <para>Crypto Box encrypts original Seed Phrase with salt and password that is retrieved</para>
        /// <para>from `password_provider` callback, implemented on Application side.</para>
        /// <para>When used, decrypted secret shows up in core library's memory for a very short period</para>
        /// <para>of time and then is immediately overwritten with zeroes.</para>
        /// </summary>
        public Task<RegisteredCryptoBox> CreateCryptoBox(ParamsOfCreateCryptoBox @params, Func<JsonElement, uint, CancellationToken, Task> appObject = null, CancellationToken cancellationToken = default);

        /// <summary>
        /// <para>Removes Crypto Box. Clears all secret data.</para>
        /// </summary>
        public Task RemoveCryptoBox(RegisteredCryptoBox @params, CancellationToken cancellationToken = default);

        /// <summary>
        /// <para>Get Crypto Box Info. Used to get `encrypted_secret` that should be used for all the cryptobox initializations except the first one.</para>
        /// </summary>
        public Task<ResultOfGetCryptoBoxInfo> GetCryptoBoxInfo(RegisteredCryptoBox @params, CancellationToken cancellationToken = default);

        /// <summary>
        /// <para>Get Crypto Box Seed Phrase.</para>
        /// <para>Attention! Store this data in your application for a very short period of time and overwrite it with zeroes ASAP.</para>
        /// </summary>
        public Task<ResultOfGetCryptoBoxSeedPhrase> GetCryptoBoxSeedPhrase(RegisteredCryptoBox @params, CancellationToken cancellationToken = default);

        /// <summary>
        /// <para>Get handle of Signing Box derived from Crypto Box.</para>
        /// </summary>
        public Task<RegisteredSigningBox> GetSigningBoxFromCryptoBox(ParamsOfGetSigningBoxFromCryptoBox @params, CancellationToken cancellationToken = default);

        /// <summary>
        /// <para>Gets Encryption Box from Crypto Box.</para>
        /// <para>Derives encryption keypair from cryptobox secret and hdpath and</para>
        /// <para>stores it in cache for `secret_lifetime`</para>
        /// <para>or until explicitly cleared by `clear_crypto_box_secret_cache` method.</para>
        /// <para>If `secret_lifetime` is not specified - overwrites encryption secret with zeroes immediately after</para>
        /// <para>encryption operation.</para>
        /// </summary>
        public Task<RegisteredEncryptionBox> GetEncryptionBoxFromCryptoBox(ParamsOfGetEncryptionBoxFromCryptoBox @params, CancellationToken cancellationToken = default);

        /// <summary>
        /// <para>Removes cached secrets (overwrites with zeroes) from all signing and encryption boxes, derived from crypto box.</para>
        /// </summary>
        public Task ClearCryptoBoxSecretCache(RegisteredCryptoBox @params, CancellationToken cancellationToken = default);

        /// <summary>
        /// <para>Register an application implemented signing box.</para>
        /// </summary>
        public Task<RegisteredSigningBox> RegisterSigningBox(Func<JsonElement, uint, CancellationToken, Task> appObject = null, CancellationToken cancellationToken = default);

        /// <summary>
        /// <para>Creates a default signing box implementation.</para>
        /// </summary>
        public Task<RegisteredSigningBox> GetSigningBox(KeyPair @params, CancellationToken cancellationToken = default);

        /// <summary>
        /// <para>Returns public key of signing key pair.</para>
        /// </summary>
        public Task<ResultOfSigningBoxGetPublicKey> SigningBoxGetPublicKey(RegisteredSigningBox @params, CancellationToken cancellationToken = default);

        /// <summary>
        /// <para>Returns signed user data.</para>
        /// </summary>
        public Task<ResultOfSigningBoxSign> SigningBoxSign(ParamsOfSigningBoxSign @params, CancellationToken cancellationToken = default);

        /// <summary>
        /// <para>Removes signing box from SDK.</para>
        /// </summary>
        public Task RemoveSigningBox(RegisteredSigningBox @params, CancellationToken cancellationToken = default);

        /// <summary>
        /// <para>Register an application implemented encryption box.</para>
        /// </summary>
        public Task<RegisteredEncryptionBox> RegisterEncryptionBox(Func<JsonElement, uint, CancellationToken, Task> appObject = null, CancellationToken cancellationToken = default);

        /// <summary>
        /// <para>Removes encryption box from SDK</para>
        /// </summary>
        public Task RemoveEncryptionBox(RegisteredEncryptionBox @params, CancellationToken cancellationToken = default);

        /// <summary>
        /// <para>Queries info from the given encryption box</para>
        /// </summary>
        public Task<ResultOfEncryptionBoxGetInfo> EncryptionBoxGetInfo(ParamsOfEncryptionBoxGetInfo @params, CancellationToken cancellationToken = default);

        /// <summary>
        /// <para>Encrypts data using given encryption box Note.</para>
        /// <para>Block cipher algorithms pad data to cipher block size so encrypted data can be longer then original data. Client should store the original data size after encryption and use it after</para>
        /// <para>decryption to retrieve the original data from decrypted data.</para>
        /// </summary>
        public Task<ResultOfEncryptionBoxEncrypt> EncryptionBoxEncrypt(ParamsOfEncryptionBoxEncrypt @params, CancellationToken cancellationToken = default);

        /// <summary>
        /// <para>Decrypts data using given encryption box Note.</para>
        /// <para>Block cipher algorithms pad data to cipher block size so encrypted data can be longer then original data. Client should store the original data size after encryption and use it after</para>
        /// <para>decryption to retrieve the original data from decrypted data.</para>
        /// </summary>
        public Task<ResultOfEncryptionBoxDecrypt> EncryptionBoxDecrypt(ParamsOfEncryptionBoxDecrypt @params, CancellationToken cancellationToken = default);

        /// <summary>
        /// <para>Creates encryption box with specified algorithm</para>
        /// </summary>
        public Task<RegisteredEncryptionBox> CreateEncryptionBox(ParamsOfCreateEncryptionBox @params, CancellationToken cancellationToken = default);
    }
}