using EverscaleNet.Client.Models;

namespace EverscaleNet;

/// <summary>
/// </summary>
public interface IMultisigAccount : IInternalSender {
	/// <summary>
	/// </summary>
	string Address { get; }

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
	Task<ResultOfProcessMessage> SendTransaction(string dest, decimal coins, bool bounce, SendTransactionFlags flags, string payload, CancellationToken cancellationToken = default);

	/// <summary>
	/// </summary>
	/// <param name="owners"></param>
	/// <param name="reqConfirms"></param>
	/// <param name="lifetime"></param>
	/// <param name="cancellationToken"></param>
	/// <returns></returns>
	Task<ResultOfProcessMessage> Deploy(IEnumerable<string> owners, short reqConfirms, TimeSpan lifetime, CancellationToken cancellationToken = default);

	/// <summary>
	/// </summary>
	/// <param name="cancellationToken"></param>
	/// <returns></returns>
	Task<decimal> GetBalance(CancellationToken cancellationToken = default);

	/// <summary>
	/// </summary>
	/// <param name="publicKey"></param>
	/// <param name="initialData"></param>
	/// <param name="cancellationToken"></param>
	/// <returns></returns>
	Task Init(string? publicKey = null, object? initialData = null, CancellationToken cancellationToken = default);
}
