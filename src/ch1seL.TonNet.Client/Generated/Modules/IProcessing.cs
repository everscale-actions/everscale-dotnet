using ch1seL.TonNet.Abstract;
using ch1seL.TonNet.Client.Abstract;
using ch1seL.TonNet.Client.Models;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace ch1seL.TonNet.Client
{
    public interface IProcessing : ITonModule
    {
        public Task<SendMessageResponse> SendMessage(SendMessageRequest @params, Action<ProcessingEvent> callback, CancellationToken cancellationToken = default);
        public Task<ProcessMessageResponse> WaitForTransaction(WaitForTransactionRequest @params, Action<ProcessingEvent> callback, CancellationToken cancellationToken = default);
        public Task<ProcessMessageResponse> ProcessMessage(ProcessMessageRequest @params, Action<ProcessingEvent> request, CancellationToken cancellationToken = default);
    }
}