using EverscaleNet.Client.Models;

namespace EverscaleNet.Models;

/// <summary>
///     Package contains Abi and Tvc of contract
/// </summary>
public class Package {
	/// <summary>
	///     Package .ctor
	/// </summary>
	/// <param name="abi"></param>
	/// <param name="tvc"></param>
	public Package(Abi abi, string tvc) {
		Abi = abi;
		Tvc = tvc;
	}

	/// <summary>
	///     Abi of contract
	/// </summary>
	public Abi Abi { get; }

	/// <summary>
	///     Tvc of contract
	/// </summary>
	public string Tvc { get; }
}
