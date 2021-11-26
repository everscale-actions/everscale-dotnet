using System.Threading.Tasks;
using ch1seL.TonNet.Client;
using ch1seL.TonNet.Client.Models;

namespace ch1seL.TonNet.Abstract; 

/// <summary>
///     Load package from abi and tvm files. Default path is _contracts/abi_v{AbiVersion}/
/// </summary>
public interface ITonPackageManager {
	/// <summary>
	///     Load package from abi and tvm files. Default path is _contracts/abi_v{AbiVersion}/
	/// </summary>
	public Task<Package> LoadPackage(string name);

	/// <summary>
	///     Load Abi from abi file. Default path is _contracts/abi_v{AbiVersion}/{PackageName}.abi
	/// </summary>
	/// <param name="name">Package name</param>
	/// <returns>
	///     <see cref="Abi" />
	///     Contract interface, the methods and parameters used to interact with it.
	/// </returns>
	public Task<Abi> LoadAbi(string name);

	/// <summary>
	///     Load Tvc from tvc file. Default path is _contracts/abi_v{AbiVersion}/{PackageName}.tvc
	/// </summary>
	/// <param name="name">Package name</param>
	/// <returns>
	///     The compiled smart contract data. Used only when generating contract address and deploying contract code to the blockchain.
	/// </returns>
	public Task<string> LoadTvc(string name);
}