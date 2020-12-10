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
        /// Not described yet..
        /// </summary>
        public Task<ResultOfFactorize> Factorize(ParamsOfFactorize @params, CancellationToken cancellationToken = default);

        /// <summary>
        /// Not described yet..
        /// </summary>
        public Task<ResultOfModularPower> ModularPower(ParamsOfModularPower @params, CancellationToken cancellationToken = default);

        /// <summary>
        /// Not described yet..
        /// </summary>
        public Task<ResultOfTonCrc16> TonCrc16(ParamsOfTonCrc16 @params, CancellationToken cancellationToken = default);

        /// <summary>
        /// Not described yet..
        /// </summary>
        public Task<ResultOfGenerateRandomBytes> GenerateRandomBytes(ParamsOfGenerateRandomBytes @params, CancellationToken cancellationToken = default);

        /// <summary>
        /// Not described yet..
        /// </summary>
        public Task<ResultOfConvertPublicKeyToTonSafeFormat> ConvertPublicKeyToTonSafeFormat(ParamsOfConvertPublicKeyToTonSafeFormat @params, CancellationToken cancellationToken = default);

        /// <summary>
        /// Not described yet..
        /// </summary>
        public Task<KeyPair> GenerateRandomSignKeys(CancellationToken cancellationToken = default);

        /// <summary>
        /// Not described yet..
        /// </summary>
        public Task<ResultOfSign> Sign(ParamsOfSign @params, CancellationToken cancellationToken = default);

        /// <summary>
        /// Not described yet..
        /// </summary>
        public Task<ResultOfVerifySignature> VerifySignature(ParamsOfVerifySignature @params, CancellationToken cancellationToken = default);

        /// <summary>
        /// Not described yet..
        /// </summary>
        public Task<ResultOfHash> Sha256(ParamsOfHash @params, CancellationToken cancellationToken = default);

        /// <summary>
        /// Not described yet..
        /// </summary>
        public Task<ResultOfHash> Sha512(ParamsOfHash @params, CancellationToken cancellationToken = default);

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
        public Task<ResultOfScrypt> Scrypt(ParamsOfScrypt @params, CancellationToken cancellationToken = default);

        /// <summary>
        /// Not described yet..
        /// </summary>
        public Task<KeyPair> NaclSignKeypairFromSecretKey(ParamsOfNaclSignKeyPairFromSecret @params, CancellationToken cancellationToken = default);

        /// <summary>
        /// Not described yet..
        /// </summary>
        public Task<ResultOfNaclSign> NaclSign(ParamsOfNaclSign @params, CancellationToken cancellationToken = default);

        /// <summary>
        /// Not described yet..
        /// </summary>
        public Task<ResultOfNaclSignOpen> NaclSignOpen(ParamsOfNaclSignOpen @params, CancellationToken cancellationToken = default);

        /// <summary>
        /// Not described yet..
        /// </summary>
        public Task<ResultOfNaclSignDetached> NaclSignDetached(ParamsOfNaclSign @params, CancellationToken cancellationToken = default);

        /// <summary>
        /// Not described yet..
        /// </summary>
        public Task<KeyPair> NaclBoxKeypair(CancellationToken cancellationToken = default);

        /// <summary>
        /// Not described yet..
        /// </summary>
        public Task<KeyPair> NaclBoxKeypairFromSecretKey(ParamsOfNaclBoxKeyPairFromSecret @params, CancellationToken cancellationToken = default);

        /// <summary>
        /// <para>Encrypt and authenticate a message using the senders secret key, the recievers public</para>
        /// <para>key, and a nonce.</para>
        /// </summary>
        public Task<ResultOfNaclBox> NaclBox(ParamsOfNaclBox @params, CancellationToken cancellationToken = default);

        /// <summary>
        /// Not described yet..
        /// </summary>
        public Task<ResultOfNaclBoxOpen> NaclBoxOpen(ParamsOfNaclBoxOpen @params, CancellationToken cancellationToken = default);

        /// <summary>
        /// Not described yet..
        /// </summary>
        public Task<ResultOfNaclBox> NaclSecretBox(ParamsOfNaclSecretBox @params, CancellationToken cancellationToken = default);

        /// <summary>
        /// Not described yet..
        /// </summary>
        public Task<ResultOfNaclBoxOpen> NaclSecretBoxOpen(ParamsOfNaclSecretBoxOpen @params, CancellationToken cancellationToken = default);

        /// <summary>
        /// Not described yet..
        /// </summary>
        public Task<ResultOfMnemonicWords> MnemonicWords(ParamsOfMnemonicWords @params, CancellationToken cancellationToken = default);

        /// <summary>
        /// Not described yet..
        /// </summary>
        public Task<ResultOfMnemonicFromRandom> MnemonicFromRandom(ParamsOfMnemonicFromRandom @params, CancellationToken cancellationToken = default);

        /// <summary>
        /// Not described yet..
        /// </summary>
        public Task<ResultOfMnemonicFromEntropy> MnemonicFromEntropy(ParamsOfMnemonicFromEntropy @params, CancellationToken cancellationToken = default);

        /// <summary>
        /// Not described yet..
        /// </summary>
        public Task<ResultOfMnemonicVerify> MnemonicVerify(ParamsOfMnemonicVerify @params, CancellationToken cancellationToken = default);

        /// <summary>
        /// Not described yet..
        /// </summary>
        public Task<KeyPair> MnemonicDeriveSignKeys(ParamsOfMnemonicDeriveSignKeys @params, CancellationToken cancellationToken = default);

        /// <summary>
        /// Not described yet..
        /// </summary>
        public Task<ResultOfHDKeyXPrvFromMnemonic> HdkeyXprvFromMnemonic(ParamsOfHDKeyXPrvFromMnemonic @params, CancellationToken cancellationToken = default);

        /// <summary>
        /// Not described yet..
        /// </summary>
        public Task<ResultOfHDKeyDeriveFromXPrv> HdkeyDeriveFromXprv(ParamsOfHDKeyDeriveFromXPrv @params, CancellationToken cancellationToken = default);

        /// <summary>
        /// Not described yet..
        /// </summary>
        public Task<ResultOfHDKeyDeriveFromXPrvPath> HdkeyDeriveFromXprvPath(ParamsOfHDKeyDeriveFromXPrvPath @params, CancellationToken cancellationToken = default);

        /// <summary>
        /// Not described yet..
        /// </summary>
        public Task<ResultOfHDKeySecretFromXPrv> HdkeySecretFromXprv(ParamsOfHDKeySecretFromXPrv @params, CancellationToken cancellationToken = default);

        /// <summary>
        /// Not described yet..
        /// </summary>
        public Task<ResultOfHDKeyPublicFromXPrv> HdkeyPublicFromXprv(ParamsOfHDKeyPublicFromXPrv @params, CancellationToken cancellationToken = default);

        /// <summary>
        /// Not described yet..
        /// </summary>
        public Task<ResultOfChaCha20> Chacha20(ParamsOfChaCha20 @params, CancellationToken cancellationToken = default);

        /// <summary>
        /// Not described yet..
        /// </summary>
        public Task<RegisteredSigningBox> RegisterSigningBox(Action<JsonElement,uint> appObject = null, CancellationToken cancellationToken = default);

        /// <summary>
        /// Not described yet..
        /// </summary>
        public Task<RegisteredSigningBox> GetSigningBox(KeyPair @params, CancellationToken cancellationToken = default);

        /// <summary>
        /// Not described yet..
        /// </summary>
        public Task<ResultOfSigningBoxGetPublicKey> SigningBoxGetPublicKey(RegisteredSigningBox @params, CancellationToken cancellationToken = default);

        /// <summary>
        /// Not described yet..
        /// </summary>
        public Task<ResultOfSigningBoxSign> SigningBoxSign(ParamsOfSigningBoxSign @params, CancellationToken cancellationToken = default);

        /// <summary>
        /// Not described yet..
        /// </summary>
        public Task RemoveSigningBox(RegisteredSigningBox @params, CancellationToken cancellationToken = default);
    }
}