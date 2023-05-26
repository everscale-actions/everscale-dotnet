namespace EverscaleNet;

/// <summary>
/// </summary>
public static class EverClientExtensions {
	/// <summary>
	///     Check that all destination transactions for out messages is ok
	/// </summary>
	/// <param name="everClient"></param>
	/// <param name="resultOfProcessMessage"></param>
	/// <param name="cancellationToken"></param>
	/// <exception cref="NoOutMessagesException"></exception>
	/// <exception cref="EverClientException"></exception>
	public static async Task EnsureThatTransactionsIsOk(this IEverClient everClient, ResultOfProcessMessage resultOfProcessMessage, CancellationToken cancellationToken) {
		if (resultOfProcessMessage.OutMessages.Length == 0) {
			throw new NoOutMessagesException(resultOfProcessMessage);
		}
		string[] outMessages = await Task.WhenAll(resultOfProcessMessage
		                                          .OutMessages
		                                          .Select(async m =>
			                                                  (await everClient.Boc.ParseMessage(new ParamsOfParse { Boc = m }, cancellationToken))
			                                                  .Parsed!.Get<string>("id")));
		ResultOfQueryCollection resultOfQueryCollection = await everClient.Net.QueryCollection(new ParamsOfQueryCollection {
			Collection = "messages",
			Filter = new { id = new { @in = outMessages } }.ToJsonElement(),
			Result = "dst_transaction{aborted compute{success exit_code}}"
		}, cancellationToken);

		bool failed = resultOfQueryCollection.Result.Select(r => r.ToPrototype(new {
			dst_transaction = new {
				aborted = default(bool),
				compute = new {
					success = (bool?)null,
					exit_code = (uint?)null
				}
			}
		})).Any(r => r.dst_transaction.aborted || !(r.dst_transaction.compute.success ?? false));
		if (failed) {
			//todo: throw more detailed exception
			throw new EverClientException("Transaction aborted or failed");
		}
	}
}
