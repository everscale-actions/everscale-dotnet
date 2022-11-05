using EverscaleNet.Abstract;
using EverscaleNet.Abstract.Modules;
using EverscaleNet.Client.Models;
using System;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;

namespace EverscaleNet.Client.Modules
{
    /// <summary>
    /// <para>Crypto Module</para>
    /// </summary>
    public class CryptoModule : ICryptoModule
    {
        private readonly IEverClientAdapter _everClientAdapter;

        /// <summary>
        /// <para>.ctor</para>
        /// </summary>
        public CryptoModule(IEverClientAdapter everClientAdapter)
        {
            _everClientAdapter = everClientAdapter;
        }

        /// <summary>
        /// <para>Integer factorization</para>
        /// <para>Performs prime factorization – decomposition of a composite number</para>
        /// <para>into a product of smaller prime integers (factors).</para>
        /// <para>See [https://en.wikipedia.org/wiki/Integer_factorization]</para>
        /// </summary>
        public async Task<ResultOfFactorize> Factorize(ParamsOfFactorize @params, CancellationToken cancellationToken = default)
        {
            return await _everClientAdapter.Request<ParamsOfFactorize, ResultOfFactorize>("crypto.factorize", @params, cancellationToken);
        }

        /// <summary>
        /// <para>Modular exponentiation</para>
        /// <para>Performs modular exponentiation for big integers (`base`^`exponent` mod `modulus`).</para>
        /// <para>See [https://en.wikipedia.org/wiki/Modular_exponentiation]</para>
        /// </summary>
        public async Task<ResultOfModularPower> ModularPower(ParamsOfModularPower @params, CancellationToken cancellationToken = default)
        {
            return await _everClientAdapter.Request<ParamsOfModularPower, ResultOfModularPower>("crypto.modular_power", @params, cancellationToken);
        }

        /// <summary>
        /// <para>Calculates CRC16 using TON algorithm.</para>
        /// </summary>
        public async Task<ResultOfTonCrc16> TonCrc16(ParamsOfTonCrc16 @params, CancellationToken cancellationToken = default)
        {
            return await _everClientAdapter.Request<ParamsOfTonCrc16, ResultOfTonCrc16>("crypto.ton_crc16", @params, cancellationToken);
        }

        /// <summary>
        /// <para>Generates random byte array of the specified length and returns it in `base64` format</para>
        /// </summary>
        public async Task<ResultOfGenerateRandomBytes> GenerateRandomBytes(ParamsOfGenerateRandomBytes @params, CancellationToken cancellationToken = default)
        {
            return await _everClientAdapter.Request<ParamsOfGenerateRandomBytes, ResultOfGenerateRandomBytes>("crypto.generate_random_bytes", @params, cancellationToken);
        }

        /// <summary>
        /// <para>Converts public key to ton safe_format</para>
        /// </summary>
        public async Task<ResultOfConvertPublicKeyToTonSafeFormat> ConvertPublicKeyToTonSafeFormat(ParamsOfConvertPublicKeyToTonSafeFormat @params, CancellationToken cancellationToken = default)
        {
            return await _everClientAdapter.Request<ParamsOfConvertPublicKeyToTonSafeFormat, ResultOfConvertPublicKeyToTonSafeFormat>("crypto.convert_public_key_to_ton_safe_format", @params, cancellationToken);
        }

        /// <summary>
        /// <para>Generates random ed25519 key pair.</para>
        /// </summary>
        public async Task<KeyPair> GenerateRandomSignKeys(CancellationToken cancellationToken = default)
        {
            return await _everClientAdapter.Request<KeyPair>("crypto.generate_random_sign_keys", cancellationToken);
        }

        /// <summary>
        /// <para>Signs a data using the provided keys.</para>
        /// </summary>
        public async Task<ResultOfSign> Sign(ParamsOfSign @params, CancellationToken cancellationToken = default)
        {
            return await _everClientAdapter.Request<ParamsOfSign, ResultOfSign>("crypto.sign", @params, cancellationToken);
        }

        /// <summary>
        /// <para>Verifies signed data using the provided public key. Raises error if verification is failed.</para>
        /// </summary>
        public async Task<ResultOfVerifySignature> VerifySignature(ParamsOfVerifySignature @params, CancellationToken cancellationToken = default)
        {
            return await _everClientAdapter.Request<ParamsOfVerifySignature, ResultOfVerifySignature>("crypto.verify_signature", @params, cancellationToken);
        }

        /// <summary>
        /// <para>Calculates SHA256 hash of the specified data.</para>
        /// </summary>
        public async Task<ResultOfHash> Sha256(ParamsOfHash @params, CancellationToken cancellationToken = default)
        {
            return await _everClientAdapter.Request<ParamsOfHash, ResultOfHash>("crypto.sha256", @params, cancellationToken);
        }

        /// <summary>
        /// <para>Calculates SHA512 hash of the specified data.</para>
        /// </summary>
        public async Task<ResultOfHash> Sha512(ParamsOfHash @params, CancellationToken cancellationToken = default)
        {
            return await _everClientAdapter.Request<ParamsOfHash, ResultOfHash>("crypto.sha512", @params, cancellationToken);
        }

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
        public async Task<ResultOfScrypt> Scrypt(ParamsOfScrypt @params, CancellationToken cancellationToken = default)
        {
            return await _everClientAdapter.Request<ParamsOfScrypt, ResultOfScrypt>("crypto.scrypt", @params, cancellationToken);
        }

        /// <summary>
        /// <para>Generates a key pair for signing from the secret key</para>
        /// <para>**NOTE:** In the result the secret key is actually the concatenation</para>
        /// <para>of secret and public keys (128 symbols hex string) by design of [NaCL](http://nacl.cr.yp.to/sign.html).</para>
        /// <para>See also [the stackexchange question](https://crypto.stackexchange.com/questions/54353/).</para>
        /// </summary>
        public async Task<KeyPair> NaclSignKeypairFromSecretKey(ParamsOfNaclSignKeyPairFromSecret @params, CancellationToken cancellationToken = default)
        {
            return await _everClientAdapter.Request<ParamsOfNaclSignKeyPairFromSecret, KeyPair>("crypto.nacl_sign_keypair_from_secret_key", @params, cancellationToken);
        }

        /// <summary>
        /// <para>Signs data using the signer's secret key.</para>
        /// </summary>
        public async Task<ResultOfNaclSign> NaclSign(ParamsOfNaclSign @params, CancellationToken cancellationToken = default)
        {
            return await _everClientAdapter.Request<ParamsOfNaclSign, ResultOfNaclSign>("crypto.nacl_sign", @params, cancellationToken);
        }

        /// <summary>
        /// <para>Verifies the signature and returns the unsigned message</para>
        /// <para>Verifies the signature in `signed` using the signer's public key `public`</para>
        /// <para>and returns the message `unsigned`.</para>
        /// <para>If the signature fails verification, crypto_sign_open raises an exception.</para>
        /// </summary>
        public async Task<ResultOfNaclSignOpen> NaclSignOpen(ParamsOfNaclSignOpen @params, CancellationToken cancellationToken = default)
        {
            return await _everClientAdapter.Request<ParamsOfNaclSignOpen, ResultOfNaclSignOpen>("crypto.nacl_sign_open", @params, cancellationToken);
        }

        /// <summary>
        /// <para>Signs the message using the secret key and returns a signature.</para>
        /// <para>Signs the message `unsigned` using the secret key `secret`</para>
        /// <para>and returns a signature `signature`.</para>
        /// </summary>
        public async Task<ResultOfNaclSignDetached> NaclSignDetached(ParamsOfNaclSign @params, CancellationToken cancellationToken = default)
        {
            return await _everClientAdapter.Request<ParamsOfNaclSign, ResultOfNaclSignDetached>("crypto.nacl_sign_detached", @params, cancellationToken);
        }

        /// <summary>
        /// <para>Verifies the signature with public key and `unsigned` data.</para>
        /// </summary>
        public async Task<ResultOfNaclSignDetachedVerify> NaclSignDetachedVerify(ParamsOfNaclSignDetachedVerify @params, CancellationToken cancellationToken = default)
        {
            return await _everClientAdapter.Request<ParamsOfNaclSignDetachedVerify, ResultOfNaclSignDetachedVerify>("crypto.nacl_sign_detached_verify", @params, cancellationToken);
        }

        /// <summary>
        /// <para>Generates a random NaCl key pair</para>
        /// </summary>
        public async Task<KeyPair> NaclBoxKeypair(CancellationToken cancellationToken = default)
        {
            return await _everClientAdapter.Request<KeyPair>("crypto.nacl_box_keypair", cancellationToken);
        }

        /// <summary>
        /// <para>Generates key pair from a secret key</para>
        /// </summary>
        public async Task<KeyPair> NaclBoxKeypairFromSecretKey(ParamsOfNaclBoxKeyPairFromSecret @params, CancellationToken cancellationToken = default)
        {
            return await _everClientAdapter.Request<ParamsOfNaclBoxKeyPairFromSecret, KeyPair>("crypto.nacl_box_keypair_from_secret_key", @params, cancellationToken);
        }

        /// <summary>
        /// <para>Public key authenticated encryption</para>
        /// <para>Encrypt and authenticate a message using the senders secret key, the receivers public</para>
        /// <para>key, and a nonce.</para>
        /// </summary>
        public async Task<ResultOfNaclBox> NaclBox(ParamsOfNaclBox @params, CancellationToken cancellationToken = default)
        {
            return await _everClientAdapter.Request<ParamsOfNaclBox, ResultOfNaclBox>("crypto.nacl_box", @params, cancellationToken);
        }

        /// <summary>
        /// <para>Decrypt and verify the cipher text using the receivers secret key, the senders public key, and the nonce.</para>
        /// </summary>
        public async Task<ResultOfNaclBoxOpen> NaclBoxOpen(ParamsOfNaclBoxOpen @params, CancellationToken cancellationToken = default)
        {
            return await _everClientAdapter.Request<ParamsOfNaclBoxOpen, ResultOfNaclBoxOpen>("crypto.nacl_box_open", @params, cancellationToken);
        }

        /// <summary>
        /// <para>Encrypt and authenticate message using nonce and secret key.</para>
        /// </summary>
        public async Task<ResultOfNaclBox> NaclSecretBox(ParamsOfNaclSecretBox @params, CancellationToken cancellationToken = default)
        {
            return await _everClientAdapter.Request<ParamsOfNaclSecretBox, ResultOfNaclBox>("crypto.nacl_secret_box", @params, cancellationToken);
        }

        /// <summary>
        /// <para>Decrypts and verifies cipher text using `nonce` and secret `key`.</para>
        /// </summary>
        public async Task<ResultOfNaclBoxOpen> NaclSecretBoxOpen(ParamsOfNaclSecretBoxOpen @params, CancellationToken cancellationToken = default)
        {
            return await _everClientAdapter.Request<ParamsOfNaclSecretBoxOpen, ResultOfNaclBoxOpen>("crypto.nacl_secret_box_open", @params, cancellationToken);
        }

        /// <summary>
        /// <para>Prints the list of words from the specified dictionary</para>
        /// </summary>
        public async Task<ResultOfMnemonicWords> MnemonicWords(ParamsOfMnemonicWords @params, CancellationToken cancellationToken = default)
        {
            return await _everClientAdapter.Request<ParamsOfMnemonicWords, ResultOfMnemonicWords>("crypto.mnemonic_words", @params, cancellationToken);
        }

        /// <summary>
        /// <para>Generates a random mnemonic</para>
        /// <para>Generates a random mnemonic from the specified dictionary and word count</para>
        /// </summary>
        public async Task<ResultOfMnemonicFromRandom> MnemonicFromRandom(ParamsOfMnemonicFromRandom @params, CancellationToken cancellationToken = default)
        {
            return await _everClientAdapter.Request<ParamsOfMnemonicFromRandom, ResultOfMnemonicFromRandom>("crypto.mnemonic_from_random", @params, cancellationToken);
        }

        /// <summary>
        /// <para>Generates mnemonic from pre-generated entropy</para>
        /// </summary>
        public async Task<ResultOfMnemonicFromEntropy> MnemonicFromEntropy(ParamsOfMnemonicFromEntropy @params, CancellationToken cancellationToken = default)
        {
            return await _everClientAdapter.Request<ParamsOfMnemonicFromEntropy, ResultOfMnemonicFromEntropy>("crypto.mnemonic_from_entropy", @params, cancellationToken);
        }

        /// <summary>
        /// <para>Validates a mnemonic phrase</para>
        /// <para>The phrase supplied will be checked for word length and validated according to the checksum</para>
        /// <para>specified in BIP0039.</para>
        /// </summary>
        public async Task<ResultOfMnemonicVerify> MnemonicVerify(ParamsOfMnemonicVerify @params, CancellationToken cancellationToken = default)
        {
            return await _everClientAdapter.Request<ParamsOfMnemonicVerify, ResultOfMnemonicVerify>("crypto.mnemonic_verify", @params, cancellationToken);
        }

        /// <summary>
        /// <para>Derives a key pair for signing from the seed phrase</para>
        /// <para>Validates the seed phrase, generates master key and then derives</para>
        /// <para>the key pair from the master key and the specified path</para>
        /// </summary>
        public async Task<KeyPair> MnemonicDeriveSignKeys(ParamsOfMnemonicDeriveSignKeys @params, CancellationToken cancellationToken = default)
        {
            return await _everClientAdapter.Request<ParamsOfMnemonicDeriveSignKeys, KeyPair>("crypto.mnemonic_derive_sign_keys", @params, cancellationToken);
        }

        /// <summary>
        /// <para>Generates an extended master private key that will be the root for all the derived keys</para>
        /// </summary>
        public async Task<ResultOfHDKeyXPrvFromMnemonic> HdkeyXprvFromMnemonic(ParamsOfHDKeyXPrvFromMnemonic @params, CancellationToken cancellationToken = default)
        {
            return await _everClientAdapter.Request<ParamsOfHDKeyXPrvFromMnemonic, ResultOfHDKeyXPrvFromMnemonic>("crypto.hdkey_xprv_from_mnemonic", @params, cancellationToken);
        }

        /// <summary>
        /// <para>Returns extended private key derived from the specified extended private key and child index</para>
        /// </summary>
        public async Task<ResultOfHDKeyDeriveFromXPrv> HdkeyDeriveFromXprv(ParamsOfHDKeyDeriveFromXPrv @params, CancellationToken cancellationToken = default)
        {
            return await _everClientAdapter.Request<ParamsOfHDKeyDeriveFromXPrv, ResultOfHDKeyDeriveFromXPrv>("crypto.hdkey_derive_from_xprv", @params, cancellationToken);
        }

        /// <summary>
        /// <para>Derives the extended private key from the specified key and path</para>
        /// </summary>
        public async Task<ResultOfHDKeyDeriveFromXPrvPath> HdkeyDeriveFromXprvPath(ParamsOfHDKeyDeriveFromXPrvPath @params, CancellationToken cancellationToken = default)
        {
            return await _everClientAdapter.Request<ParamsOfHDKeyDeriveFromXPrvPath, ResultOfHDKeyDeriveFromXPrvPath>("crypto.hdkey_derive_from_xprv_path", @params, cancellationToken);
        }

        /// <summary>
        /// <para>Extracts the private key from the serialized extended private key</para>
        /// </summary>
        public async Task<ResultOfHDKeySecretFromXPrv> HdkeySecretFromXprv(ParamsOfHDKeySecretFromXPrv @params, CancellationToken cancellationToken = default)
        {
            return await _everClientAdapter.Request<ParamsOfHDKeySecretFromXPrv, ResultOfHDKeySecretFromXPrv>("crypto.hdkey_secret_from_xprv", @params, cancellationToken);
        }

        /// <summary>
        /// <para>Extracts the public key from the serialized extended private key</para>
        /// </summary>
        public async Task<ResultOfHDKeyPublicFromXPrv> HdkeyPublicFromXprv(ParamsOfHDKeyPublicFromXPrv @params, CancellationToken cancellationToken = default)
        {
            return await _everClientAdapter.Request<ParamsOfHDKeyPublicFromXPrv, ResultOfHDKeyPublicFromXPrv>("crypto.hdkey_public_from_xprv", @params, cancellationToken);
        }

        /// <summary>
        /// <para>Performs symmetric `chacha20` encryption.</para>
        /// </summary>
        public async Task<ResultOfChaCha20> Chacha20(ParamsOfChaCha20 @params, CancellationToken cancellationToken = default)
        {
            return await _everClientAdapter.Request<ParamsOfChaCha20, ResultOfChaCha20>("crypto.chacha20", @params, cancellationToken);
        }

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
        public async Task<RegisteredCryptoBox> CreateCryptoBox(ParamsOfCreateCryptoBox @params, Action<JsonElement,uint> appObject = null, CancellationToken cancellationToken = default)
        {
            return await _everClientAdapter.Request<ParamsOfCreateCryptoBox, RegisteredCryptoBox, JsonElement>("crypto.create_crypto_box", @params, appObject, cancellationToken);
        }

        /// <summary>
        /// <para>Removes Crypto Box. Clears all secret data.</para>
        /// </summary>
        public async Task RemoveCryptoBox(RegisteredCryptoBox @params, CancellationToken cancellationToken = default)
        {
            await _everClientAdapter.Request<RegisteredCryptoBox>("crypto.remove_crypto_box", @params, cancellationToken);
        }

        /// <summary>
        /// <para>Get Crypto Box Info. Used to get `encrypted_secret` that should be used for all the cryptobox initializations except the first one.</para>
        /// </summary>
        public async Task<ResultOfGetCryptoBoxInfo> GetCryptoBoxInfo(RegisteredCryptoBox @params, CancellationToken cancellationToken = default)
        {
            return await _everClientAdapter.Request<RegisteredCryptoBox, ResultOfGetCryptoBoxInfo>("crypto.get_crypto_box_info", @params, cancellationToken);
        }

        /// <summary>
        /// <para>Get Crypto Box Seed Phrase.</para>
        /// <para>Attention! Store this data in your application for a very short period of time and overwrite it with zeroes ASAP.</para>
        /// </summary>
        public async Task<ResultOfGetCryptoBoxSeedPhrase> GetCryptoBoxSeedPhrase(RegisteredCryptoBox @params, CancellationToken cancellationToken = default)
        {
            return await _everClientAdapter.Request<RegisteredCryptoBox, ResultOfGetCryptoBoxSeedPhrase>("crypto.get_crypto_box_seed_phrase", @params, cancellationToken);
        }

        /// <summary>
        /// <para>Get handle of Signing Box derived from Crypto Box.</para>
        /// </summary>
        public async Task<RegisteredSigningBox> GetSigningBoxFromCryptoBox(ParamsOfGetSigningBoxFromCryptoBox @params, CancellationToken cancellationToken = default)
        {
            return await _everClientAdapter.Request<ParamsOfGetSigningBoxFromCryptoBox, RegisteredSigningBox>("crypto.get_signing_box_from_crypto_box", @params, cancellationToken);
        }

        /// <summary>
        /// <para>Gets Encryption Box from Crypto Box.</para>
        /// <para>Derives encryption keypair from cryptobox secret and hdpath and</para>
        /// <para>stores it in cache for `secret_lifetime`</para>
        /// <para>or until explicitly cleared by `clear_crypto_box_secret_cache` method.</para>
        /// <para>If `secret_lifetime` is not specified - overwrites encryption secret with zeroes immediately after</para>
        /// <para>encryption operation.</para>
        /// </summary>
        public async Task<RegisteredEncryptionBox> GetEncryptionBoxFromCryptoBox(ParamsOfGetEncryptionBoxFromCryptoBox @params, CancellationToken cancellationToken = default)
        {
            return await _everClientAdapter.Request<ParamsOfGetEncryptionBoxFromCryptoBox, RegisteredEncryptionBox>("crypto.get_encryption_box_from_crypto_box", @params, cancellationToken);
        }

        /// <summary>
        /// <para>Removes cached secrets (overwrites with zeroes) from all signing and encryption boxes, derived from crypto box.</para>
        /// </summary>
        public async Task ClearCryptoBoxSecretCache(RegisteredCryptoBox @params, CancellationToken cancellationToken = default)
        {
            await _everClientAdapter.Request<RegisteredCryptoBox>("crypto.clear_crypto_box_secret_cache", @params, cancellationToken);
        }

        /// <summary>
        /// <para>Register an application implemented signing box.</para>
        /// </summary>
        public async Task<RegisteredSigningBox> RegisterSigningBox(Action<JsonElement,uint> appObject = null, CancellationToken cancellationToken = default)
        {
            return await _everClientAdapter.Request<RegisteredSigningBox, JsonElement>("crypto.register_signing_box", appObject, cancellationToken);
        }

        /// <summary>
        /// <para>Creates a default signing box implementation.</para>
        /// </summary>
        public async Task<RegisteredSigningBox> GetSigningBox(KeyPair @params, CancellationToken cancellationToken = default)
        {
            return await _everClientAdapter.Request<KeyPair, RegisteredSigningBox>("crypto.get_signing_box", @params, cancellationToken);
        }

        /// <summary>
        /// <para>Returns public key of signing key pair.</para>
        /// </summary>
        public async Task<ResultOfSigningBoxGetPublicKey> SigningBoxGetPublicKey(RegisteredSigningBox @params, CancellationToken cancellationToken = default)
        {
            return await _everClientAdapter.Request<RegisteredSigningBox, ResultOfSigningBoxGetPublicKey>("crypto.signing_box_get_public_key", @params, cancellationToken);
        }

        /// <summary>
        /// <para>Returns signed user data.</para>
        /// </summary>
        public async Task<ResultOfSigningBoxSign> SigningBoxSign(ParamsOfSigningBoxSign @params, CancellationToken cancellationToken = default)
        {
            return await _everClientAdapter.Request<ParamsOfSigningBoxSign, ResultOfSigningBoxSign>("crypto.signing_box_sign", @params, cancellationToken);
        }

        /// <summary>
        /// <para>Removes signing box from SDK.</para>
        /// </summary>
        public async Task RemoveSigningBox(RegisteredSigningBox @params, CancellationToken cancellationToken = default)
        {
            await _everClientAdapter.Request<RegisteredSigningBox>("crypto.remove_signing_box", @params, cancellationToken);
        }

        /// <summary>
        /// <para>Register an application implemented encryption box.</para>
        /// </summary>
        public async Task<RegisteredEncryptionBox> RegisterEncryptionBox(Action<JsonElement,uint> appObject = null, CancellationToken cancellationToken = default)
        {
            return await _everClientAdapter.Request<RegisteredEncryptionBox, JsonElement>("crypto.register_encryption_box", appObject, cancellationToken);
        }

        /// <summary>
        /// <para>Removes encryption box from SDK</para>
        /// </summary>
        public async Task RemoveEncryptionBox(RegisteredEncryptionBox @params, CancellationToken cancellationToken = default)
        {
            await _everClientAdapter.Request<RegisteredEncryptionBox>("crypto.remove_encryption_box", @params, cancellationToken);
        }

        /// <summary>
        /// <para>Queries info from the given encryption box</para>
        /// </summary>
        public async Task<ResultOfEncryptionBoxGetInfo> EncryptionBoxGetInfo(ParamsOfEncryptionBoxGetInfo @params, CancellationToken cancellationToken = default)
        {
            return await _everClientAdapter.Request<ParamsOfEncryptionBoxGetInfo, ResultOfEncryptionBoxGetInfo>("crypto.encryption_box_get_info", @params, cancellationToken);
        }

        /// <summary>
        /// <para>Encrypts data using given encryption box Note.</para>
        /// <para>Block cipher algorithms pad data to cipher block size so encrypted data can be longer then original data. Client should store the original data size after encryption and use it after</para>
        /// <para>decryption to retrieve the original data from decrypted data.</para>
        /// </summary>
        public async Task<ResultOfEncryptionBoxEncrypt> EncryptionBoxEncrypt(ParamsOfEncryptionBoxEncrypt @params, CancellationToken cancellationToken = default)
        {
            return await _everClientAdapter.Request<ParamsOfEncryptionBoxEncrypt, ResultOfEncryptionBoxEncrypt>("crypto.encryption_box_encrypt", @params, cancellationToken);
        }

        /// <summary>
        /// <para>Decrypts data using given encryption box Note.</para>
        /// <para>Block cipher algorithms pad data to cipher block size so encrypted data can be longer then original data. Client should store the original data size after encryption and use it after</para>
        /// <para>decryption to retrieve the original data from decrypted data.</para>
        /// </summary>
        public async Task<ResultOfEncryptionBoxDecrypt> EncryptionBoxDecrypt(ParamsOfEncryptionBoxDecrypt @params, CancellationToken cancellationToken = default)
        {
            return await _everClientAdapter.Request<ParamsOfEncryptionBoxDecrypt, ResultOfEncryptionBoxDecrypt>("crypto.encryption_box_decrypt", @params, cancellationToken);
        }

        /// <summary>
        /// <para>Creates encryption box with specified algorithm</para>
        /// </summary>
        public async Task<RegisteredEncryptionBox> CreateEncryptionBox(ParamsOfCreateEncryptionBox @params, CancellationToken cancellationToken = default)
        {
            return await _everClientAdapter.Request<ParamsOfCreateEncryptionBox, RegisteredEncryptionBox>("crypto.create_encryption_box", @params, cancellationToken);
        }
    }
}