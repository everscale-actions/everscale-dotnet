using EverscaleNet.Client.Models;
using System;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;

namespace EverscaleNet.Abstract.Modules
{
    /// <summary>
    /// <para>Abi Module</para>
    /// </summary>
    public interface IAbiModule : IEverModule
    {
        /// <summary>
        /// <para>Encodes message body according to ABI function call.</para>
        /// </summary>
        public Task<ResultOfEncodeMessageBody> EncodeMessageBody(ParamsOfEncodeMessageBody @params, CancellationToken cancellationToken = default);

        /// <summary>
        /// <para>Not described yet..</para>
        /// </summary>
        public Task<ResultOfAttachSignatureToMessageBody> AttachSignatureToMessageBody(ParamsOfAttachSignatureToMessageBody @params, CancellationToken cancellationToken = default);

        /// <summary>
        /// <para>Encodes an ABI-compatible message</para>
        /// <para>Allows to encode deploy and function call messages,</para>
        /// <para>both signed and unsigned.</para>
        /// <para>Use cases include messages of any possible type:</para>
        /// <para>- deploy with initial function call (i.e. `constructor` or any other function that is used for some kind</para>
        /// <para>of initialization);</para>
        /// <para>- deploy without initial function call;</para>
        /// <para>- signed/unsigned + data for signing.</para>
        /// <para>`Signer` defines how the message should or shouldn't be signed:</para>
        /// <para>`Signer::None` creates an unsigned message. This may be needed in case of some public methods,</para>
        /// <para>that do not require authorization by pubkey.</para>
        /// <para>`Signer::External` takes public key and returns `data_to_sign` for later signing.</para>
        /// <para>Use `attach_signature` method with the result signature to get the signed message.</para>
        /// <para>`Signer::Keys` creates a signed message with provided key pair.</para>
        /// <para>[SOON] `Signer::SigningBox` Allows using a special interface to implement signing</para>
        /// <para>without private key disclosure to SDK. For instance, in case of using a cold wallet or HSM,</para>
        /// <para>when application calls some API to sign data.</para>
        /// <para>There is an optional public key can be provided in deploy set in order to substitute one</para>
        /// <para>in TVM file.</para>
        /// <para>Public key resolving priority:</para>
        /// <para>1. Public key from deploy set.</para>
        /// <para>2. Public key, specified in TVM file.</para>
        /// <para>3. Public key, provided by signer.</para>
        /// </summary>
        public Task<ResultOfEncodeMessage> EncodeMessage(ParamsOfEncodeMessage @params, CancellationToken cancellationToken = default);

        /// <summary>
        /// <para>Encodes an internal ABI-compatible message</para>
        /// <para>Allows to encode deploy and function call messages.</para>
        /// <para>Use cases include messages of any possible type:</para>
        /// <para>- deploy with initial function call (i.e. `constructor` or any other function that is used for some kind</para>
        /// <para>of initialization);</para>
        /// <para>- deploy without initial function call;</para>
        /// <para>- simple function call</para>
        /// <para>There is an optional public key can be provided in deploy set in order to substitute one</para>
        /// <para>in TVM file.</para>
        /// <para>Public key resolving priority:</para>
        /// <para>1. Public key from deploy set.</para>
        /// <para>2. Public key, specified in TVM file.</para>
        /// </summary>
        public Task<ResultOfEncodeInternalMessage> EncodeInternalMessage(ParamsOfEncodeInternalMessage @params, CancellationToken cancellationToken = default);

        /// <summary>
        /// <para>Combines `hex`-encoded `signature` with `base64`-encoded `unsigned_message`. Returns signed message encoded in `base64`.</para>
        /// </summary>
        public Task<ResultOfAttachSignature> AttachSignature(ParamsOfAttachSignature @params, CancellationToken cancellationToken = default);

        /// <summary>
        /// <para>Decodes message body using provided message BOC and ABI.</para>
        /// </summary>
        public Task<DecodedMessageBody> DecodeMessage(ParamsOfDecodeMessage @params, CancellationToken cancellationToken = default);

        /// <summary>
        /// <para>Decodes message body using provided body BOC and ABI.</para>
        /// </summary>
        public Task<DecodedMessageBody> DecodeMessageBody(ParamsOfDecodeMessageBody @params, CancellationToken cancellationToken = default);

        /// <summary>
        /// <para>Creates account state BOC</para>
        /// <para>Creates account state provided with one of these sets of data :</para>
        /// <para>1. BOC of code, BOC of data, BOC of library</para>
        /// <para>2. TVC (string in `base64`), keys, init params</para>
        /// </summary>
        public Task<ResultOfEncodeAccount> EncodeAccount(ParamsOfEncodeAccount @params, CancellationToken cancellationToken = default);

        /// <summary>
        /// <para>Decodes account data using provided data BOC and ABI.</para>
        /// <para>Note: this feature requires ABI 2.1 or higher.</para>
        /// </summary>
        public Task<ResultOfDecodeAccountData> DecodeAccountData(ParamsOfDecodeAccountData @params, CancellationToken cancellationToken = default);

        /// <summary>
        /// <para>Updates initial account data with initial values for the contract's static variables and owner's public key. This operation is applicable only for initial account data (before deploy). If the contract is already deployed, its data doesn't contain this data section any more.</para>
        /// </summary>
        public Task<ResultOfUpdateInitialData> UpdateInitialData(ParamsOfUpdateInitialData @params, CancellationToken cancellationToken = default);

        /// <summary>
        /// <para>Encodes initial account data with initial values for the contract's static variables and owner's public key into a data BOC that can be passed to `encode_tvc` function afterwards.</para>
        /// <para>This function is analogue of `tvm.buildDataInit` function in Solidity.</para>
        /// </summary>
        public Task<ResultOfEncodeInitialData> EncodeInitialData(ParamsOfEncodeInitialData @params, CancellationToken cancellationToken = default);

        /// <summary>
        /// <para>Decodes initial values of a contract's static variables and owner's public key from account initial data This operation is applicable only for initial account data (before deploy). If the contract is already deployed, its data doesn't contain this data section any more.</para>
        /// </summary>
        public Task<ResultOfDecodeInitialData> DecodeInitialData(ParamsOfDecodeInitialData @params, CancellationToken cancellationToken = default);

        /// <summary>
        /// <para>Decodes BOC into JSON as a set of provided parameters.</para>
        /// <para>Solidity functions use ABI types for [builder encoding](https://github.com/tonlabs/TON-Solidity-Compiler/blob/master/API.md#tvmbuilderstore).</para>
        /// <para>The simplest way to decode such a BOC is to use ABI decoding.</para>
        /// <para>ABI has it own rules for fields layout in cells so manually encoded</para>
        /// <para>BOC can not be described in terms of ABI rules.</para>
        /// <para>To solve this problem we introduce a new ABI type `Ref(&lt;ParamType&gt;)`</para>
        /// <para>which allows to store `ParamType` ABI parameter in cell reference and, thus,</para>
        /// <para>decode manually encoded BOCs. This type is available only in `decode_boc` function</para>
        /// <para>and will not be available in ABI messages encoding until it is included into some ABI revision.</para>
        /// <para>Such BOC descriptions covers most users needs. If someone wants to decode some BOC which</para>
        /// <para>can not be described by these rules (i.e. BOC with TLB containing constructors of flags</para>
        /// <para>defining some parsing conditions) then they can decode the fields up to fork condition,</para>
        /// <para>check the parsed data manually, expand the parsing schema and then decode the whole BOC</para>
        /// <para>with the full schema.</para>
        /// </summary>
        public Task<ResultOfDecodeBoc> DecodeBoc(ParamsOfDecodeBoc @params, CancellationToken cancellationToken = default);

        /// <summary>
        /// <para>Encodes given parameters in JSON into a BOC using param types from ABI.</para>
        /// </summary>
        public Task<ResultOfAbiEncodeBoc> EncodeBoc(ParamsOfAbiEncodeBoc @params, CancellationToken cancellationToken = default);

        /// <summary>
        /// <para>Calculates contract function ID by contract ABI</para>
        /// </summary>
        public Task<ResultOfCalcFunctionId> CalcFunctionId(ParamsOfCalcFunctionId @params, CancellationToken cancellationToken = default);

        /// <summary>
        /// <para>Extracts signature from message body and calculates hash to verify the signature</para>
        /// </summary>
        public Task<ResultOfGetSignatureData> GetSignatureData(ParamsOfGetSignatureData @params, CancellationToken cancellationToken = default);
    }
}