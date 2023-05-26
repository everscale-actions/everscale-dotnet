namespace EverscaleNet.Abstract;

/// <inheritdoc cref="IEverPackageManager" />
public static class EverPackageManagerExtensions {
	/// <summary>
	///     Load package from abi and tvm files. Default path is _contracts/abi_v{AbiVersion}/
	/// </summary>
	public static async Task<Package> LoadPackage(this IEverPackageManager packageManager, string name, CancellationToken cancellationToken = default) {
		Task<Abi?> getAbiTask = packageManager.LoadAbi(name, cancellationToken);
		Task<string?> getTvcTask = packageManager.LoadTvc(name, cancellationToken);
		Task<string?> getCode = packageManager.LoadCode(name, cancellationToken);
		Task<KeyPair?> getKeyPar = packageManager.LoadKeyPair(name, cancellationToken);

		await Task.WhenAll(getAbiTask, getTvcTask, getCode, getKeyPar);

		return new Package(getAbiTask.Result, getTvcTask.Result, getKeyPar.Result, getCode.Result);
	}
}
