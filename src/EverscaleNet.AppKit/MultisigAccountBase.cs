using EverscaleNet.Abstract;
using EverscaleNet.Client.Models;
using EverscaleNet.Serialization;
using EverscaleNet.Utils;

namespace EverscaleNet;

/// <inheritdoc cref="EverscaleNet.IMultisigAccount" />
public abstract class MultisigAccountBase : AccountBase, IMultisigAccount {
	/// <summary>
	/// </summary>
	/// <param name="client"></param>
	/// <param name="packageManager"></param>
	protected MultisigAccountBase(IEverClient client, IEverPackageManager packageManager) : base(client, packageManager) { }

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
	public async Task<ResultOfProcessMessage> SubmitTransaction(string dest, decimal coins, bool bounce, bool allBalance, string payload, string? stateInit = null,
	                                                            CancellationToken cancellationToken = default) {
		var value = $"{coins.CoinsToNano():0}";
		ResultOfProcessMessage result = await Run(new CallSet {
			FunctionName = "submitTransaction",
			Input = new {
				dest,
				value,
				bounce,
				allBalance,
				payload,
				stateInit
			}.ToJsonElement()
		}, cancellationToken);
		return result;
	}

	/// <summary>
	/// </summary>
	/// <param name="dest"></param>
	/// <param name="coins"></param>
	/// <param name="bounce"></param>
	/// <param name="flags"></param>
	/// <param name="payload"></param>
	/// <param name="cancellationToken"></param>
	/// <returns></returns>
	public async Task<ResultOfProcessMessage> SendTransaction(string dest, decimal coins, bool bounce, SendTransactionFlags flags, string payload, CancellationToken cancellationToken = default) {
		var value = $"{coins.CoinsToNano():0}";
		ResultOfProcessMessage result = await Run(new CallSet {
			FunctionName = "sendTransaction",
			Input = new {
				dest,
				value,
				bounce,
				flags = (byte)flags,
				payload
			}.ToJsonElement()
		}, cancellationToken);
		return result;
	}

	/// <summary>
	/// </summary>
	/// <param name="owners"></param>
	/// <param name="reqConfirms"></param>
	/// <param name="lifetime"></param>
	/// <param name="cancellationToken"></param>
	/// <returns></returns>
	public async Task<ResultOfProcessMessage> Deploy(IEnumerable<string> owners, short reqConfirms, TimeSpan lifetime, CancellationToken cancellationToken = default) {
		return await base.Deploy(new {
			owners = owners.Select(key => $"0x{key}"),
			reqConfirms,
			lifetime = (int)lifetime.TotalSeconds
		}, cancellationToken);
	}

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
	public async Task<ResultOfProcessMessage> Send(string dest, decimal coins, bool bounce, bool allBalance, string payload, string? stateInit = null, CancellationToken cancellationToken = default) {
		if (stateInit is not null) {
			return await SubmitTransaction(dest, coins, bounce, allBalance, payload, stateInit, cancellationToken);
		}

		SendTransactionFlags flags = SendTransactionFlags.IgnoreSomeErrors | SendTransactionFlags.SenderWantsToPayTransferFeesSeparately;
		if (allBalance) {
			flags |= SendTransactionFlags.CarryAllRemainingBalance;
		}
		return await SendTransaction(dest, coins, bounce, flags, payload, cancellationToken);
	}
}
