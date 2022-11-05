using EverscaleNet.Client.Models;
using System;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;

namespace EverscaleNet.Abstract.Modules
{
    /// <summary>
    /// <para>Processing Module</para>
    /// </summary>
    public interface IProcessingModule : IEverModule
    {
        /// <summary>
        /// <para>Sends message to the network</para>
        /// <para>Sends message to the network and returns the last generated shard block of the destination account</para>
        /// <para>before the message was sent. It will be required later for message processing.</para>
        /// </summary>
        public Task<ResultOfSendMessage> SendMessage(ParamsOfSendMessage @params, Action<ProcessingEvent,uint> callback = null, CancellationToken cancellationToken = default);

        /// <summary>
        /// <para>Performs monitoring of the network for the result transaction of the external inbound message processing.</para>
        /// <para>`send_events` enables intermediate events, such as `WillFetchNextBlock`,</para>
        /// <para>`FetchNextBlockFailed` that may be useful for logging of new shard blocks creation</para>
        /// <para>during message processing.</para>
        /// <para>Note, that presence of the `abi` parameter is critical for ABI</para>
        /// <para>compliant contracts. Message processing uses drastically</para>
        /// <para>different strategy for processing message for contracts which</para>
        /// <para>ABI includes "expire" header.</para>
        /// <para>When the ABI header `expire` is present, the processing uses</para>
        /// <para>`message expiration` strategy:</para>
        /// <para>- The maximum block gen time is set to</para>
        /// <para>  `message_expiration_timeout + transaction_wait_timeout`.</para>
        /// <para>- When maximum block gen time is reached, the processing will</para>
        /// <para>  be finished with `MessageExpired` error.</para>
        /// <para>When the ABI header `expire` isn't present or `abi` parameter</para>
        /// <para>isn't specified, the processing uses `transaction waiting`</para>
        /// <para>strategy:</para>
        /// <para>- The maximum block gen time is set to</para>
        /// <para>  `now() + transaction_wait_timeout`.</para>
        /// <para>- If maximum block gen time is reached and no result transaction is found,</para>
        /// <para>the processing will exit with an error.</para>
        /// </summary>
        public Task<ResultOfProcessMessage> WaitForTransaction(ParamsOfWaitForTransaction @params, Action<ProcessingEvent,uint> callback = null, CancellationToken cancellationToken = default);

        /// <summary>
        /// <para>Creates message, sends it to the network and monitors its processing.</para>
        /// <para>Creates ABI-compatible message,</para>
        /// <para>sends it to the network and monitors for the result transaction.</para>
        /// <para>Decodes the output messages' bodies.</para>
        /// <para>If contract's ABI includes "expire" header, then</para>
        /// <para>SDK implements retries in case of unsuccessful message delivery within the expiration</para>
        /// <para>timeout: SDK recreates the message, sends it and processes it again.</para>
        /// <para>The intermediate events, such as `WillFetchFirstBlock`, `WillSend`, `DidSend`,</para>
        /// <para>`WillFetchNextBlock`, etc - are switched on/off by `send_events` flag</para>
        /// <para>and logged into the supplied callback function.</para>
        /// <para>The retry configuration parameters are defined in the client's `NetworkConfig` and `AbiConfig`.</para>
        /// <para>If contract's ABI does not include "expire" header</para>
        /// <para>then, if no transaction is found within the network timeout (see config parameter ), exits with error.</para>
        /// </summary>
        public Task<ResultOfProcessMessage> ProcessMessage(ParamsOfProcessMessage @params, Action<ProcessingEvent,uint> request = null, CancellationToken cancellationToken = default);
    }
}