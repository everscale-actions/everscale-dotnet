namespace EverscaleNet.Abstract;

/// <summary>
///     Load package from abi and tvm files. Default path is _contracts/abi_v{AbiVersion}/
/// </summary>
public interface IEverPackageManager {
	/// <summary>
	///     Load whole package
	/// </summary>
	/// <param name="name"></param>
	/// <param name="cancellationToken"></param>
	/// <returns></returns>
	public async Task<IPackage> LoadPackage(string name, CancellationToken cancellationToken) {
		return new Package(
			await LoadAbi(name, cancellationToken),
			await LoadTvc(name, cancellationToken),
			await LoadKeyPair(name, cancellationToken),
			await LoadCode(name, cancellationToken),
			await LoadBase64(name, cancellationToken)
		);
	}

	/// <summary>
	///     Load Abi from abi file. Default path is _contracts/{PackageName}.abi.json
	/// </summary>
	/// <param name="name">Package name</param>
	/// <param name="cancellationToken"></param>
	/// <returns>
	///     <see cref="Abi" />
	///     Contract interface, the methods and parameters used to interact with it.
	/// </returns>
	public Task<Abi?> LoadAbi(string name, CancellationToken cancellationToken = default);

	/// <summary>
	///     Load Tvc from tvc file. Default path is _contracts/{PackageName}.tvc
	/// </summary>
	/// <param name="name">Package name</param>
	/// <param name="cancellationToken"></param>
	/// <returns>
	///     The compiled smart contract data. Used only when generating contract address and deploying contract code to the blockchain.
	/// </returns>
	public Task<string?> LoadTvc(string name, CancellationToken cancellationToken = default);

	/// <summary>
	///     Load KeyPair from json keys file. Default path is _contracts/{PackageName}.keys
	/// </summary>
	/// <param name="name">Package name</param>
	/// <param name="cancellationToken"></param>
	/// <returns>
	///     The KeyPair
	/// </returns>
	public Task<KeyPair?> LoadKeyPair(string name, CancellationToken cancellationToken = default);

	/// <summary>
	///     Load Code from code file. Default path is _contracts/{PackageName}.code
	/// </summary>
	/// <param name="name">Package name</param>
	/// <param name="cancellationToken"></param>
	/// <returns>
	///     The compiled smart contract data. Used only when generating contract address and deploying contract code to the blockchain.
	/// </returns>
	public Task<string?> LoadCode(string name, CancellationToken cancellationToken = default);

	/// <summary>
	///     Load Code in base64 from base64 file. Default path is _contracts/{PackageName}.base64
	/// </summary>
	/// <param name="name">Package name</param>
	/// <param name="cancellationToken"></param>
	/// <returns>
	///     The base64 code of contract. Used only when injecting code to another contracts.
	/// </returns>
	Task<string?> LoadBase64(string name, CancellationToken cancellationToken);
}
