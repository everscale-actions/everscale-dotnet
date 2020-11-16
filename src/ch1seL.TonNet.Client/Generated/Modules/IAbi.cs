using ch1seL.TonNet.Abstract;
using ch1seL.TonNet.Client.Abstract;
using ch1seL.TonNet.Client.Models;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace ch1seL.TonNet.Client
{
    public interface IAbi : ITonModule
    {
        public Task<EncodeMessageBodyResponse> EncodeMessageBody(EncodeMessageBodyRequest @params, CancellationToken cancellationToken = default);
        public Task<AttachSignatureToMessageBodyResponse> AttachSignatureToMessageBody(AttachSignatureToMessageBodyRequest @params, CancellationToken cancellationToken = default);
        public Task<EncodeMessageResponse> EncodeMessage(EncodeMessageRequest @params, CancellationToken cancellationToken = default);
        public Task<AttachSignatureResponse> AttachSignature(AttachSignatureRequest @params, CancellationToken cancellationToken = default);
        public Task<DecodedMessageBody> DecodeMessage(DecodeMessageRequest @params, CancellationToken cancellationToken = default);
        public Task<DecodedMessageBody> DecodeMessageBody(DecodeMessageBodyRequest @params, CancellationToken cancellationToken = default);
        public Task<EncodeAccountResponse> EncodeAccount(EncodeAccountRequest @params, CancellationToken cancellationToken = default);
    }
}