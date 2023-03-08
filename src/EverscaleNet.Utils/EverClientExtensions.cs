using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using EverscaleNet.Abstract;
using EverscaleNet.Client.Models;
using EverscaleNet.Models;
using EverscaleNet.Serialization;

namespace EverscaleNet.Utils;

/// <summary>
/// </summary>
public static class EverClientExtensions {
	/// <summary>
	/// </summary>
	/// <param name="everClient"></param>
	/// <param name="encodedMessage"></param>
	/// <param name="cancellationToken"></param>
	/// <returns>Total account fees in coins</returns>
	public static async Task<ProcessAndWaitInternalMessagesResult> ProcessAndWaitInternalMessages(this IEverClient everClient, ParamsOfEncodeMessage encodedMessage,
	                                                                                              CancellationToken cancellationToken = default) {
		ResultOfProcessMessage resultOfProcessMessage =
			await everClient.Processing.ProcessMessage(new ParamsOfProcessMessage { MessageEncodeParams = encodedMessage, SendEvents = true }, cancellationToken: cancellationToken);
		(string transctionId, decimal value, decimal fees)[] transactions = await WaitAndGetInternalMessageTransactions(everClient, resultOfProcessMessage.OutMessages, cancellationToken);
		return new ProcessAndWaitInternalMessagesResult {
			ProcessingFees = resultOfProcessMessage.Fees,
			ChildValue = transactions.Sum(r => r.value),
			ChildTransactionFees = transactions.Sum(r => r.fees)
		};
	}

	private static async Task<(string transctionId, decimal value, decimal fees)[]> WaitAndGetInternalMessageTransactions(IEverClient everClient, IEnumerable<string> messages,
	                                                                                                                      CancellationToken cancellationToken) {
		var parsedMessages = await Task.WhenAll(messages.Select(async message => {
			ResultOfParse parseResult = await everClient.Boc.ParseMessage(new ParamsOfParse {
				Boc = message
			}, cancellationToken);
			var parsedPrototype = new { id = default(string), msg_type = default(MessageType) };
			var parsedMessage = parseResult.Parsed!.ToPrototype(parsedPrototype);
			return new { messageId = parsedMessage.id, messageType = parsedMessage.msg_type };
		}));

		return await Task.WhenAll(parsedMessages
		                          .Where(message => message.messageType == MessageType.Internal)
		                          .Select(async parsedMessage => {
			                          ResultOfWaitForCollection result = await everClient.Net.WaitForCollection(new ParamsOfWaitForCollection {
				                          Collection = "transactions",
				                          Filter = new { in_msg = new { eq = parsedMessage.messageId } }.ToJsonElement(),
				                          Result = "id in_message{value(format:DEC)} total_fees(format: DEC)",
			                          }, cancellationToken);
			                          var proto = new {
				                          id = default(string),
				                          in_message = new { value = default(string) },
				                          total_fees = default(string)
			                          };
			                          var res = result.Result!.ToPrototype(proto);
			                          decimal value = decimal.Parse(res.in_message.value!);
			                          decimal childFees = decimal.Parse(res.total_fees!);
			                          return (res.id!, value, childFees);
		                          }));
	}
}

/// <summary>
/// </summary>
[SuppressMessage("ReSharper", "UnusedAutoPropertyAccessor.Global")]
public class ProcessAndWaitInternalMessagesResult {
	/// <summary>
	/// </summary>
	public TransactionFees? ProcessingFees { get; set; }
	/// <summary>
	/// </summary>
	public decimal ChildValue { get; set; }
	/// <summary>
	/// </summary>
	public decimal ChildTransactionFees { get; set; }
}
