using System.Threading;
using System.Threading.Tasks;

namespace EverscaleNet;

/// <summary>
///     The giver is used to send coins to some address
/// </summary>
public interface IEverGiver {
	/// <summary>
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
	Task SendTransaction(string dest, decimal coins, bool bounce = false, CancellationToken cancellationToken = default);
}
