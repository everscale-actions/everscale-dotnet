using ch1seL.TonNet.Abstract;
using ch1seL.TonNet.Abstract.Modules;
using ch1seL.TonNet.Client.Models;
using System;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;

namespace ch1seL.TonNet.Client.Modules
{
    public class CryptoModule : ICryptoModule
    {
        private readonly ITonClientAdapter _tonClientAdapter;

        public CryptoModule(ITonClientAdapter tonClientAdapter)
        {
            _tonClientAdapter = tonClientAdapter;
        }

        /// <summary>
        /// Not described yet..
        /// </summary>
        public async Task<ResultOfFactorize> Factorize(ParamsOfFactorize @params, CancellationToken cancellationToken = default)
        {
            return await _tonClientAdapter.Request<ParamsOfFactorize, ResultOfFactorize>("crypto.factorize", @params, cancellationToken);
        }

        /// <summary>
        /// Not described yet..
        /// </summary>
        public async Task<ResultOfModularPower> ModularPower(ParamsOfModularPower @params, CancellationToken cancellationToken = default)
        {
            return await _tonClientAdapter.Request<ParamsOfModularPower, ResultOfModularPower>("crypto.modular_power", @params, cancellationToken);
        }

        /// <summary>
        /// Not described yet..
        /// </summary>
        public async Task<ResultOfTonCrc16> TonCrc16(ParamsOfTonCrc16 @params, CancellationToken cancellationToken = default)
        {
            return await _tonClientAdapter.Request<ParamsOfTonCrc16, ResultOfTonCrc16>("crypto.ton_crc16", @params, cancellationToken);
        }

        /// <summary>
        /// Not described yet..
        /// </summary>
        public async Task<ResultOfGenerateRandomBytes> GenerateRandomBytes(ParamsOfGenerateRandomBytes @params, CancellationToken cancellationToken = default)
        {
            return await _tonClientAdapter.Request<ParamsOfGenerateRandomBytes, ResultOfGenerateRandomBytes>("crypto.generate_random_bytes", @params, cancellationToken);
        }

        /// <summary>
        /// Not described yet..
        /// </summary>
        public async Task<ResultOfConvertPublicKeyToTonSafeFormat> ConvertPublicKeyToTonSafeFormat(ParamsOfConvertPublicKeyToTonSafeFormat @params, CancellationToken cancellationToken = default)
        {
            return await _tonClientAdapter.Request<ParamsOfConvertPublicKeyToTonSafeFormat, ResultOfConvertPublicKeyToTonSafeFormat>("crypto.convert_public_key_to_ton_safe_format", @params, cancellationToken);
        }

        /// <summary>
        /// Not described yet..
        /// </summary>
        public async Task<KeyPair> GenerateRandomSignKeys(CancellationToken cancellationToken = default)
        {
            return await _tonClientAdapter.Request<KeyPair>("crypto.generate_random_sign_keys", cancellationToken);
        }

        /// <summary>
        /// Not described yet..
        /// </summary>
        public async Task<ResultOfSign> Sign(ParamsOfSign @params, CancellationToken cancellationToken = default)
        {
            return await _tonClientAdapter.Request<ParamsOfSign, ResultOfSign>("crypto.sign", @params, cancellationToken);
        }

        /// <summary>
        /// Not described yet..
        /// </summary>
        public async Task<ResultOfVerifySignature> VerifySignature(ParamsOfVerifySignature @params, CancellationToken cancellationToken = default)
        {
            return await _tonClientAdapter.Request<ParamsOfVerifySignature, ResultOfVerifySignature>("crypto.verify_signature", @params, cancellationToken);
        }

        /// <summary>
        /// Not described yet..
        /// </summary>
        public async Task<ResultOfHash> Sha256(ParamsOfHash @params, CancellationToken cancellationToken = default)
        {
            return await _tonClientAdapter.Request<ParamsOfHash, ResultOfHash>("crypto.sha256", @params, cancellationToken);
        }

        /// <summary>
        /// Not described yet..
        /// </summary>
        public async Task<ResultOfHash> Sha512(ParamsOfHash @params, CancellationToken cancellationToken = default)
        {
            return await _tonClientAdapter.Request<ParamsOfHash, ResultOfHash>("crypto.sha512", @params, cancellationToken);
        }

        /// <summary>
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
            return await _tonClientAdapter.Request<ParamsOfScrypt, ResultOfScrypt>("crypto.scrypt", @params, cancellationToken);
        }

        /// <summary>
        /// Not described yet..
        /// </summary>
        public async Task<KeyPair> NaclSignKeypairFromSecretKey(ParamsOfNaclSignKeyPairFromSecret @params, CancellationToken cancellationToken = default)
        {
            return await _tonClientAdapter.Request<ParamsOfNaclSignKeyPairFromSecret, KeyPair>("crypto.nacl_sign_keypair_from_secret_key", @params, cancellationToken);
        }

        /// <summary>
        /// Not described yet..
        /// </summary>
        public async Task<ResultOfNaclSign> NaclSign(ParamsOfNaclSign @params, CancellationToken cancellationToken = default)
        {
            return await _tonClientAdapter.Request<ParamsOfNaclSign, ResultOfNaclSign>("crypto.nacl_sign", @params, cancellationToken);
        }

        /// <summary>
        /// Not described yet..
        /// </summary>
        public async Task<ResultOfNaclSignOpen> NaclSignOpen(ParamsOfNaclSignOpen @params, CancellationToken cancellationToken = default)
        {
            return await _tonClientAdapter.Request<ParamsOfNaclSignOpen, ResultOfNaclSignOpen>("crypto.nacl_sign_open", @params, cancellationToken);
        }

        /// <summary>
        /// Not described yet..
        /// </summary>
        public async Task<ResultOfNaclSignDetached> NaclSignDetached(ParamsOfNaclSign @params, CancellationToken cancellationToken = default)
        {
            return await _tonClientAdapter.Request<ParamsOfNaclSign, ResultOfNaclSignDetached>("crypto.nacl_sign_detached", @params, cancellationToken);
        }

        /// <summary>
        /// Not described yet..
        /// </summary>
        public async Task<KeyPair> NaclBoxKeypair(CancellationToken cancellationToken = default)
        {
            return await _tonClientAdapter.Request<KeyPair>("crypto.nacl_box_keypair", cancellationToken);
        }

        /// <summary>
        /// Not described yet..
        /// </summary>
        public async Task<KeyPair> NaclBoxKeypairFromSecretKey(ParamsOfNaclBoxKeyPairFromSecret @params, CancellationToken cancellationToken = default)
        {
            return await _tonClientAdapter.Request<ParamsOfNaclBoxKeyPairFromSecret, KeyPair>("crypto.nacl_box_keypair_from_secret_key", @params, cancellationToken);
        }

        /// <summary>
        /// <para>Encrypt and authenticate a message using the senders secret key, the recievers public</para>
        /// <para>key, and a nonce.</para>
        /// </summary>
        public async Task<ResultOfNaclBox> NaclBox(ParamsOfNaclBox @params, CancellationToken cancellationToken = default)
        {
            return await _tonClientAdapter.Request<ParamsOfNaclBox, ResultOfNaclBox>("crypto.nacl_box", @params, cancellationToken);
        }

        /// <summary>
        /// Not described yet..
        /// </summary>
        public async Task<ResultOfNaclBoxOpen> NaclBoxOpen(ParamsOfNaclBoxOpen @params, CancellationToken cancellationToken = default)
        {
            return await _tonClientAdapter.Request<ParamsOfNaclBoxOpen, ResultOfNaclBoxOpen>("crypto.nacl_box_open", @params, cancellationToken);
        }

        /// <summary>
        /// Not described yet..
        /// </summary>
        public async Task<ResultOfNaclBox> NaclSecretBox(ParamsOfNaclSecretBox @params, CancellationToken cancellationToken = default)
        {
            return await _tonClientAdapter.Request<ParamsOfNaclSecretBox, ResultOfNaclBox>("crypto.nacl_secret_box", @params, cancellationToken);
        }

        /// <summary>
        /// Not described yet..
        /// </summary>
        public async Task<ResultOfNaclBoxOpen> NaclSecretBoxOpen(ParamsOfNaclSecretBoxOpen @params, CancellationToken cancellationToken = default)
        {
            return await _tonClientAdapter.Request<ParamsOfNaclSecretBoxOpen, ResultOfNaclBoxOpen>("crypto.nacl_secret_box_open", @params, cancellationToken);
        }

        /// <summary>
        /// Not described yet..
        /// </summary>
        public async Task<ResultOfMnemonicWords> MnemonicWords(ParamsOfMnemonicWords @params, CancellationToken cancellationToken = default)
        {
            return await _tonClientAdapter.Request<ParamsOfMnemonicWords, ResultOfMnemonicWords>("crypto.mnemonic_words", @params, cancellationToken);
        }

        /// <summary>
        /// Not described yet..
        /// </summary>
        public async Task<ResultOfMnemonicFromRandom> MnemonicFromRandom(ParamsOfMnemonicFromRandom @params, CancellationToken cancellationToken = default)
        {
            return await _tonClientAdapter.Request<ParamsOfMnemonicFromRandom, ResultOfMnemonicFromRandom>("crypto.mnemonic_from_random", @params, cancellationToken);
        }

        /// <summary>
        /// Not described yet..
        /// </summary>
        public async Task<ResultOfMnemonicFromEntropy> MnemonicFromEntropy(ParamsOfMnemonicFromEntropy @params, CancellationToken cancellationToken = default)
        {
            return await _tonClientAdapter.Request<ParamsOfMnemonicFromEntropy, ResultOfMnemonicFromEntropy>("crypto.mnemonic_from_entropy", @params, cancellationToken);
        }

        /// <summary>
        /// Not described yet..
        /// </summary>
        public async Task<ResultOfMnemonicVerify> MnemonicVerify(ParamsOfMnemonicVerify @params, CancellationToken cancellationToken = default)
        {
            return await _tonClientAdapter.Request<ParamsOfMnemonicVerify, ResultOfMnemonicVerify>("crypto.mnemonic_verify", @params, cancellationToken);
        }

        /// <summary>
        /// Not described yet..
        /// </summary>
        public async Task<KeyPair> MnemonicDeriveSignKeys(ParamsOfMnemonicDeriveSignKeys @params, CancellationToken cancellationToken = default)
        {
            return await _tonClientAdapter.Request<ParamsOfMnemonicDeriveSignKeys, KeyPair>("crypto.mnemonic_derive_sign_keys", @params, cancellationToken);
        }

        /// <summary>
        /// Not described yet..
        /// </summary>
        public async Task<ResultOfHDKeyXPrvFromMnemonic> HdkeyXprvFromMnemonic(ParamsOfHDKeyXPrvFromMnemonic @params, CancellationToken cancellationToken = default)
        {
            return await _tonClientAdapter.Request<ParamsOfHDKeyXPrvFromMnemonic, ResultOfHDKeyXPrvFromMnemonic>("crypto.hdkey_xprv_from_mnemonic", @params, cancellationToken);
        }

        /// <summary>
        /// Not described yet..
        /// </summary>
        public async Task<ResultOfHDKeyDeriveFromXPrv> HdkeyDeriveFromXprv(ParamsOfHDKeyDeriveFromXPrv @params, CancellationToken cancellationToken = default)
        {
            return await _tonClientAdapter.Request<ParamsOfHDKeyDeriveFromXPrv, ResultOfHDKeyDeriveFromXPrv>("crypto.hdkey_derive_from_xprv", @params, cancellationToken);
        }

        /// <summary>
        /// Not described yet..
        /// </summary>
        public async Task<ResultOfHDKeyDeriveFromXPrvPath> HdkeyDeriveFromXprvPath(ParamsOfHDKeyDeriveFromXPrvPath @params, CancellationToken cancellationToken = default)
        {
            return await _tonClientAdapter.Request<ParamsOfHDKeyDeriveFromXPrvPath, ResultOfHDKeyDeriveFromXPrvPath>("crypto.hdkey_derive_from_xprv_path", @params, cancellationToken);
        }

        /// <summary>
        /// Not described yet..
        /// </summary>
        public async Task<ResultOfHDKeySecretFromXPrv> HdkeySecretFromXprv(ParamsOfHDKeySecretFromXPrv @params, CancellationToken cancellationToken = default)
        {
            return await _tonClientAdapter.Request<ParamsOfHDKeySecretFromXPrv, ResultOfHDKeySecretFromXPrv>("crypto.hdkey_secret_from_xprv", @params, cancellationToken);
        }

        /// <summary>
        /// Not described yet..
        /// </summary>
        public async Task<ResultOfHDKeyPublicFromXPrv> HdkeyPublicFromXprv(ParamsOfHDKeyPublicFromXPrv @params, CancellationToken cancellationToken = default)
        {
            return await _tonClientAdapter.Request<ParamsOfHDKeyPublicFromXPrv, ResultOfHDKeyPublicFromXPrv>("crypto.hdkey_public_from_xprv", @params, cancellationToken);
        }

        /// <summary>
        /// Not described yet..
        /// </summary>
        public async Task<ResultOfChaCha20> Chacha20(ParamsOfChaCha20 @params, CancellationToken cancellationToken = default)
        {
            return await _tonClientAdapter.Request<ParamsOfChaCha20, ResultOfChaCha20>("crypto.chacha20", @params, cancellationToken);
        }

        /// <summary>
        /// Not described yet..
        /// </summary>
        public async Task<RegisteredSigningBox> RegisterSigningBox(Action<JsonElement,uint> appObject = null, CancellationToken cancellationToken = default)
        {
            return await _tonClientAdapter.Request<RegisteredSigningBox, JsonElement>("crypto.register_signing_box", appObject, cancellationToken);
        }

        /// <summary>
        /// Not described yet..
        /// </summary>
        public async Task<RegisteredSigningBox> GetSigningBox(KeyPair @params, CancellationToken cancellationToken = default)
        {
            return await _tonClientAdapter.Request<KeyPair, RegisteredSigningBox>("crypto.get_signing_box", @params, cancellationToken);
        }

        /// <summary>
        /// Not described yet..
        /// </summary>
        public async Task<ResultOfSigningBoxGetPublicKey> SigningBoxGetPublicKey(RegisteredSigningBox @params, CancellationToken cancellationToken = default)
        {
            return await _tonClientAdapter.Request<RegisteredSigningBox, ResultOfSigningBoxGetPublicKey>("crypto.signing_box_get_public_key", @params, cancellationToken);
        }

        /// <summary>
        /// Not described yet..
        /// </summary>
        public async Task<ResultOfSigningBoxSign> SigningBoxSign(ParamsOfSigningBoxSign @params, CancellationToken cancellationToken = default)
        {
            return await _tonClientAdapter.Request<ParamsOfSigningBoxSign, ResultOfSigningBoxSign>("crypto.signing_box_sign", @params, cancellationToken);
        }

        /// <summary>
        /// Not described yet..
        /// </summary>
        public async Task RemoveSigningBox(RegisteredSigningBox @params, CancellationToken cancellationToken = default)
        {
            await _tonClientAdapter.Request<RegisteredSigningBox>("crypto.remove_signing_box", @params, cancellationToken);
        }
    }
}