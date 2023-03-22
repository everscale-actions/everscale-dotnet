using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using EverscaleNet.Abstract;
using EverscaleNet.Client.Models;
using EverscaleNet.Serialization;
using EverscaleNet.Utils;

namespace EverscaleNet;

/// <summary>
/// </summary>
public class SafeMultisigAccount : AccountBase, IMultisigAccount {
	/// <summary>
	/// </summary>
	/// <param name="client"></param>
	/// <param name="packageManager"></param>
	/// <param name="keyPair"></param>
	public SafeMultisigAccount(IEverClient client, IEverPackageManager packageManager, KeyPair keyPair) : base(client, packageManager, new Signer.Keys { KeysAccessor = keyPair }) { }

	/// <summary>
	/// </summary>
	protected override string Name => "SafeMultisig";

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
	public async Task<ResultOfProcessMessage> SendTransaction(string dest, decimal coins, bool bounce, byte flags, string payload, CancellationToken cancellationToken = default) {
		var value = $"{coins.CoinsToNano():0}";
		ResultOfProcessMessage result = await Run(new CallSet {
			FunctionName = "sendTransaction",
			Input = new {
				dest,
				value,
				bounce,
				flags,
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
}
