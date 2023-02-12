using System.Threading;
using System.Threading.Tasks;
using EverscaleNet.Client.Models;

namespace EverscaleNet.Abstract;

/// <summary>
///     Load package from abi and tvm files. Default path is _contracts/abi_v{AbiVersion}/
/// </summary>
public interface IEverPackageManager {
	/// <summary>
	///     Abi file name template
	/// </summary>
	protected const string AbiFileTemplate = "{0}.abi.json";
	/// <summary>
	///     Tvc file name template
	/// </summary>
	protected const string TvcFileTemplate = "{0}.tvc";
	/// <summary>
	///     Tvc file name template
	/// </summary>
	protected const string KeyPairFileTemplate = "{0}.keys.json";

	/// <summary>
	///     Load Abi from abi file. Default path is _contracts/abi_v{AbiVersion}/{PackageName}.abi
	/// </summary>
	/// <param name="name">Package name</param>
	/// <param name="cancellationToken"></param>
	/// <returns>
	///     <see cref="Abi" />
	///     Contract interface, the methods and parameters used to interact with it.
	/// </returns>
	public Task<Abi> LoadAbi(string name, CancellationToken cancellationToken = default);

	/// <summary>
	///     Load Tvc from tvc file. Default path is _contracts/abi_v{AbiVersion}/{PackageName}.tvc
	/// </summary>
	/// <param name="name">Package name</param>
	/// <param name="cancellationToken"></param>
	/// <returns>
	///     The compiled smart contract data. Used only when generating contract address and deploying contract code to the blockchain.
	/// </returns>
	public Task<string> LoadTvc(string name, CancellationToken cancellationToken = default);

	/// <summary>
	///     Load Tvc from tvc file. Default path is _contracts/abi_v{AbiVersion}/{PackageName}.tvc
	/// </summary>
	/// <param name="name">Package name</param>
	/// <param name="cancellationToken"></param>
	/// <returns>
	///     The compiled smart contract data. Used only when generating contract address and deploying contract code to the blockchain.
	/// </returns>
	public Task<KeyPair> LoadKeyPair(string name, CancellationToken cancellationToken = default);
}
