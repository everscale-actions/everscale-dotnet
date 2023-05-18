using EverscaleNet.Client.Models;

namespace EverscaleNet;

/// <summary>
/// </summary>
public interface IMultisigAccount {
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
	Task<ResultOfProcessMessage> SubmitTransaction(string dest, decimal coins, bool bounce, bool allBalance, string payload, string? stateInit = null, CancellationToken cancellationToken = default);

	/// <summary>
	/// </summary>
	/// <param name="dest"></param>
	/// <param name="coins"></param>
	/// <param name="bounce"></param>
	/// <param name="flags"></param>
	/// <param name="payload"></param>
	/// <param name="cancellationToken"></param>
	/// <returns></returns>
	Task<ResultOfProcessMessage> SendTransaction(string dest, decimal coins, bool bounce, byte flags, string payload, CancellationToken cancellationToken = default);
}
