using ch1seL.TonNet.Abstract;
using ch1seL.TonNet.Client.Abstract;
using ch1seL.TonNet.Client.Models;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace ch1seL.TonNet.Client
{
    public class Processing : IProcessing
    {
        private readonly ITonClientAdapter _tonClientAdapter;

        public Processing(ITonClientAdapter tonClientAdapter)
        {
            _tonClientAdapter = tonClientAdapter;
        }

        public async Task<SendMessageResponse> SendMessage(SendMessageRequest @params, Action<ProcessingEvent> callback, CancellationToken cancellationToken = default)
        {
            return await _tonClientAdapter.Request<SendMessageRequest, SendMessageResponse, ProcessingEvent>("processing.send_message", @params, callback, cancellationToken);
        }

        public async Task<ProcessMessageResponse> WaitForTransaction(WaitForTransactionRequest @params, Action<ProcessingEvent> callback, CancellationToken cancellationToken = default)
        {
            return await _tonClientAdapter.Request<WaitForTransactionRequest, ProcessMessageResponse, ProcessingEvent>("processing.wait_for_transaction", @params, callback, cancellationToken);
        }

        public async Task<ProcessMessageResponse> ProcessMessage(ProcessMessageRequest @params, Action<ProcessingEvent> request, CancellationToken cancellationToken = default)
        {
            return await _tonClientAdapter.Request<ProcessMessageRequest, ProcessMessageResponse, ProcessingEvent>("processing.process_message", @params, request, cancellationToken);
        }
    }
}