using ch1seL.TonNet.Abstract;
using ch1seL.TonNet.Abstract.Modules;
using ch1seL.TonNet.Client.Models;
using System;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;

namespace ch1seL.TonNet.Client.Modules
{
    public class AbiModule : IAbiModule
    {
        private readonly ITonClientAdapter _tonClientAdapter;

        public AbiModule(ITonClientAdapter tonClientAdapter)
        {
            _tonClientAdapter = tonClientAdapter;
        }

        /// <summary>
        /// Encodes message body according to ABI function call.
        /// </summary>
        public async Task<ResultOfEncodeMessageBody> EncodeMessageBody(ParamsOfEncodeMessageBody @params, CancellationToken cancellationToken = default)
        {
            return await _tonClientAdapter.Request<ParamsOfEncodeMessageBody, ResultOfEncodeMessageBody>("abi.encode_message_body", @params, cancellationToken);
        }

        /// <summary>
        /// Not described yet..
        /// </summary>
        public async Task<ResultOfAttachSignatureToMessageBody> AttachSignatureToMessageBody(ParamsOfAttachSignatureToMessageBody @params, CancellationToken cancellationToken = default)
        {
            return await _tonClientAdapter.Request<ParamsOfAttachSignatureToMessageBody, ResultOfAttachSignatureToMessageBody>("abi.attach_signature_to_message_body", @params, cancellationToken);
        }

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
        public async Task<ResultOfEncodeMessage> EncodeMessage(ParamsOfEncodeMessage @params, CancellationToken cancellationToken = default)
        {
            return await _tonClientAdapter.Request<ParamsOfEncodeMessage, ResultOfEncodeMessage>("abi.encode_message", @params, cancellationToken);
        }

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
        public async Task<ResultOfEncodeInternalMessage> EncodeInternalMessage(ParamsOfEncodeInternalMessage @params, CancellationToken cancellationToken = default)
        {
            return await _tonClientAdapter.Request<ParamsOfEncodeInternalMessage, ResultOfEncodeInternalMessage>("abi.encode_internal_message", @params, cancellationToken);
        }

        /// <summary>
        /// Combines `hex`-encoded `signature` with `base64`-encoded `unsigned_message`. Returns signed message encoded in `base64`.
        /// </summary>
        public async Task<ResultOfAttachSignature> AttachSignature(ParamsOfAttachSignature @params, CancellationToken cancellationToken = default)
        {
            return await _tonClientAdapter.Request<ParamsOfAttachSignature, ResultOfAttachSignature>("abi.attach_signature", @params, cancellationToken);
        }

        /// <summary>
        /// Decodes message body using provided message BOC and ABI.
        /// </summary>
        public async Task<DecodedMessageBody> DecodeMessage(ParamsOfDecodeMessage @params, CancellationToken cancellationToken = default)
        {
            return await _tonClientAdapter.Request<ParamsOfDecodeMessage, DecodedMessageBody>("abi.decode_message", @params, cancellationToken);
        }

        /// <summary>
        /// Decodes message body using provided body BOC and ABI.
        /// </summary>
        public async Task<DecodedMessageBody> DecodeMessageBody(ParamsOfDecodeMessageBody @params, CancellationToken cancellationToken = default)
        {
            return await _tonClientAdapter.Request<ParamsOfDecodeMessageBody, DecodedMessageBody>("abi.decode_message_body", @params, cancellationToken);
        }

        /// <summary>
        /// <para>Creates account state BOC</para>
        /// <para>Creates account state provided with one of these sets of data :</para>
        /// <para>1. BOC of code, BOC of data, BOC of library</para>
        /// <para>2. TVC (string in `base64`), keys, init params</para>
        /// </summary>
        public async Task<ResultOfEncodeAccount> EncodeAccount(ParamsOfEncodeAccount @params, CancellationToken cancellationToken = default)
        {
            return await _tonClientAdapter.Request<ParamsOfEncodeAccount, ResultOfEncodeAccount>("abi.encode_account", @params, cancellationToken);
        }
    }
}