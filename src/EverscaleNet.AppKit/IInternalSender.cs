using EverscaleNet.Client.Models;

namespace EverscaleNet;

/// <summary>
/// </summary>
public interface IInternalSender {
	/// <summary>
	/// </summary>
	/// <param name="dest"></param>
	/// <param name="coins"></param>
	/// <param name="bounce"></param>
	/// <param name="allBalance"></param>
	/// <param name="payload"></param>
	/// <param name="stateInit"></param>
	/// <param name="cancellationToken"></param>
	/// <returns></returns>
	Task<ResultOfProcessMessage> Send(string dest, decimal coins, bool bounce, bool allBalance, string payload, string? stateInit = null, CancellationToken cancellationToken = default);
}
