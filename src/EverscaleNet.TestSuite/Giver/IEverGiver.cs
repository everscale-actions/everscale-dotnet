namespace EverscaleNet.TestSuite.Giver;

/// <summary>
///     The giver is used to send coins to some address
/// </summary>
public interface IEverGiver {
	/// <summary>
	///     Can be used to reverse fund back
	/// </summary>
	string Address { get; }

	/// <summary>
	///     Send coins from giver to destination address
	/// </summary>
	/// <param name="dest">Address</param>
	/// <param name="coins">Coins value</param>
	/// <param name="bounce">Return money if address doesn't exist</param>
	/// <param name="cancellationToken"></param>
	/// <returns></returns>
	Task<ResultOfProcessMessage> SendTransaction(string dest, decimal coins, bool bounce = false, CancellationToken cancellationToken = default);
}
