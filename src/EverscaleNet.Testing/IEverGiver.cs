using System.Threading;
using System.Threading.Tasks;

namespace EverscaleNet.Testing;

/// <summary>
///     The giver is used to send coins to some address
/// </summary>
public interface IEverGiver : IContract {
	/// <summary>
	///     Send coins from giver to destination address
	/// </summary>
	/// <param name="dest">Address</param>
	/// <param name="value">Coins value</param>
	/// <param name="bounce">Return money if address doesn't exist</param>
	/// <param name="cancellationToken"></param>
	/// <returns></returns>
	Task SendCoins(string dest, decimal value, bool bounce = false, CancellationToken cancellationToken = default);
}
