using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using EverscaleNet.Abstract;
using EverscaleNet.Client.Models;
using EverscaleNet.Serialization;

namespace EverscaleNet.Testing;

/// <summary>
/// 
/// </summary>
public static class EverClientExtensions {
	/// <summary>
	/// 
	/// </summary>
	/// <param name="everClient"></param>
	/// <param name="encodedMessage"></param>
	/// <param name="cancellationToken"></param>
	public static async Task ProcessEncodeMessageAndWaitTransaction(this IEverClient everClient, ParamsOfEncodeMessage encodedMessage, CancellationToken cancellationToken = default) {
		ResultOfProcessMessage resultOfProcessMessage = await everClient.Processing.ProcessMessage(
			                                                new ParamsOfProcessMessage {
				                                                MessageEncodeParams = encodedMessage
			                                                }, cancellationToken: cancellationToken);
		IEnumerable<Task> tasks = resultOfProcessMessage
		                          .OutMessages
		                          .Select(async message => {
			                          ResultOfParse parseResult = await everClient.Boc.ParseMessage(new ParamsOfParse { Boc = message }, cancellationToken);
			                          var parsedPrototype = new { type = default(int), id = default(string) };
			                          var parsedMessage = parseResult.Parsed!.ToAnonymous(parsedPrototype);
			                          if (parsedMessage!.type == 0) {
				                          await everClient.Net.WaitForCollection(new ParamsOfWaitForCollection {
					                          Collection = "transactions",
					                          Filter = new { in_msg = new { eq = parsedMessage.id } }.ToJsonElement(),
					                          Result = "id"
				                          }, cancellationToken);
			                          }
		                          });
		await Task.WhenAll(tasks);
	}
}
