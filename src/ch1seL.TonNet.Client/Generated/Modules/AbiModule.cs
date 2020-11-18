using ch1seL.TonNet.Abstract;
using ch1seL.TonNet.Client.Abstract;
using ch1seL.TonNet.Client.Models;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace ch1seL.TonNet.Client
{
    public class AbiModule : IAbiModule
    {
        private readonly ITonClientAdapter _tonClientAdapter;

        public AbiModule(ITonClientAdapter tonClientAdapter)
        {
            _tonClientAdapter = tonClientAdapter;
        }

        /// <summary>
        ///  Encodes message body according to ABI function call.
        /// </summary>
        public async Task<EncodeMessageBodyResponse> EncodeMessageBody(EncodeMessageBodyRequest @params, CancellationToken cancellationToken = default)
        {
            return await _tonClientAdapter.Request<EncodeMessageBodyRequest, EncodeMessageBodyResponse>("abi.encode_message_body", @params, cancellationToken);
        }

        /// <summary>
        /// Not described yet..
        /// </summary>
        public async Task<AttachSignatureToMessageBodyResponse> AttachSignatureToMessageBody(AttachSignatureToMessageBodyRequest @params, CancellationToken cancellationToken = default)
        {
            return await _tonClientAdapter.Request<AttachSignatureToMessageBodyRequest, AttachSignatureToMessageBodyResponse>("abi.attach_signature_to_message_body", @params, cancellationToken);
        }

        /// <summary>
        /// <para> Encodes an ABI-compatible message</para>
        /// <para> Allows to encode deploy and function call messages,</para>
        /// <para> both signed and unsigned.</para>
        /// <para> Use cases include messages of any possible type:</para>
        /// <para> - deploy with initial function call (i.e. `constructor` or any other function that is used for some kind</para>
        /// <para> of initialization);</para>
        /// <para> - deploy without initial function call;</para>
        /// <para> - signed/unsigned + data for signing.</para>
        /// <para> `Signer` defines how the message should or shouldn't be signed:</para>
        /// <para> `Signer::None` creates an unsigned message. This may be needed in case of some public methods,</para>
        /// <para> that do not require authorization by pubkey.</para>
        /// <para> `Signer::External` takes public key and returns `data_to_sign` for later signing.</para>
        /// <para> Use `attach_signature` method with the result signature to get the signed message.</para>
        /// <para> `Signer::Keys` creates a signed message with provided key pair.</para>
        /// <para> [SOON] `Signer::SigningBox` Allows using a special interface to imlepement signing</para>
        /// <para> without private key disclosure to SDK. For instance, in case of using a cold wallet or HSM,</para>
        /// <para> when application calls some API to sign data.</para>
        /// </summary>
        public async Task<EncodeMessageResponse> EncodeMessage(EncodeMessageRequest @params, CancellationToken cancellationToken = default)
        {
            return await _tonClientAdapter.Request<EncodeMessageRequest, EncodeMessageResponse>("abi.encode_message", @params, cancellationToken);
        }

        /// <summary>
        /// <para> Combines `hex`-encoded `signature` with `base64`-encoded `unsigned_message`.</para>
        /// <para> Returns signed message encoded in `base64`.</para>
        /// </summary>
        public async Task<AttachSignatureResponse> AttachSignature(AttachSignatureRequest @params, CancellationToken cancellationToken = default)
        {
            return await _tonClientAdapter.Request<AttachSignatureRequest, AttachSignatureResponse>("abi.attach_signature", @params, cancellationToken);
        }

        /// <summary>
        ///  Decodes message body using provided message BOC and ABI.
        /// </summary>
        public async Task<DecodedMessageBody> DecodeMessage(DecodeMessageRequest @params, CancellationToken cancellationToken = default)
        {
            return await _tonClientAdapter.Request<DecodeMessageRequest, DecodedMessageBody>("abi.decode_message", @params, cancellationToken);
        }

        /// <summary>
        ///  Decodes message body using provided body BOC and ABI.
        /// </summary>
        public async Task<DecodedMessageBody> DecodeMessageBody(DecodeMessageBodyRequest @params, CancellationToken cancellationToken = default)
        {
            return await _tonClientAdapter.Request<DecodeMessageBodyRequest, DecodedMessageBody>("abi.decode_message_body", @params, cancellationToken);
        }

        /// <summary>
        /// <para> Creates account state BOC</para>
        /// <para> </para>
        /// <para> Creates account state provided with one of these sets of data :</para>
        /// <para> 1. BOC of code, BOC of data, BOC of library</para>
        /// <para> 2. TVC (string in `base64`), keys, init params</para>
        /// </summary>
        public async Task<EncodeAccountResponse> EncodeAccount(EncodeAccountRequest @params, CancellationToken cancellationToken = default)
        {
            return await _tonClientAdapter.Request<EncodeAccountRequest, EncodeAccountResponse>("abi.encode_account", @params, cancellationToken);
        }
    }
}