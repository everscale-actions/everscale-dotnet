using ch1seL.TonNet.Abstract;
using ch1seL.TonNet.Client.Abstract;
using ch1seL.TonNet.Client.Models;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace ch1seL.TonNet.Client
{
    public class Abi : IAbi
    {
        private readonly ITonClientAdapter _tonClientAdapter;

        public Abi(ITonClientAdapter tonClientAdapter)
        {
            _tonClientAdapter = tonClientAdapter;
        }

        public async Task<EncodeMessageBodyResponse> EncodeMessageBody(EncodeMessageBodyRequest @params, CancellationToken cancellationToken = default)
        {
            return await _tonClientAdapter.Request<EncodeMessageBodyRequest, EncodeMessageBodyResponse>("abi.encode_message_body", @params, cancellationToken);
        }

        public async Task<AttachSignatureToMessageBodyResponse> AttachSignatureToMessageBody(AttachSignatureToMessageBodyRequest @params, CancellationToken cancellationToken = default)
        {
            return await _tonClientAdapter.Request<AttachSignatureToMessageBodyRequest, AttachSignatureToMessageBodyResponse>("abi.attach_signature_to_message_body", @params, cancellationToken);
        }

        public async Task<EncodeMessageResponse> EncodeMessage(EncodeMessageRequest @params, CancellationToken cancellationToken = default)
        {
            return await _tonClientAdapter.Request<EncodeMessageRequest, EncodeMessageResponse>("abi.encode_message", @params, cancellationToken);
        }

        public async Task<AttachSignatureResponse> AttachSignature(AttachSignatureRequest @params, CancellationToken cancellationToken = default)
        {
            return await _tonClientAdapter.Request<AttachSignatureRequest, AttachSignatureResponse>("abi.attach_signature", @params, cancellationToken);
        }

        public async Task<DecodedMessageBody> DecodeMessage(DecodeMessageRequest @params, CancellationToken cancellationToken = default)
        {
            return await _tonClientAdapter.Request<DecodeMessageRequest, DecodedMessageBody>("abi.decode_message", @params, cancellationToken);
        }

        public async Task<DecodedMessageBody> DecodeMessageBody(DecodeMessageBodyRequest @params, CancellationToken cancellationToken = default)
        {
            return await _tonClientAdapter.Request<DecodeMessageBodyRequest, DecodedMessageBody>("abi.decode_message_body", @params, cancellationToken);
        }

        public async Task<EncodeAccountResponse> EncodeAccount(EncodeAccountRequest @params, CancellationToken cancellationToken = default)
        {
            return await _tonClientAdapter.Request<EncodeAccountRequest, EncodeAccountResponse>("abi.encode_account", @params, cancellationToken);
        }
    }
}