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
	/// <param name="abi"></param>
	/// <param name="callSet"></param>
	/// <param name="stateInit"></param>
	/// <param name="cancellationToken"></param>
	/// <returns></returns>
	Task<ResultOfProcessMessage> Send(string dest, decimal coins, bool bounce, bool allBalance, Abi abi, CallSet callSet, string? stateInit = null, CancellationToken cancellationToken = default);
}
