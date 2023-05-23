using EverscaleNet.Client.Models;

namespace EverscaleNet.Models;

/// <summary>
///     Package contains Abi, Tvc, KeyPair and Code of contract
/// </summary>
public record Package(Abi? Abi = null, string? Tvc = null, KeyPair? KeyPair = null, string? Code = null) : IPackage {
	/// <summary>
	///     Abi
	/// </summary>
	public Abi? Abi { get; } = Abi;

	/// <summary>
	///     Tvc
	/// </summary>
	public string? Tvc { get; } = Tvc;

	/// <summary>
	///     KeyPair
	/// </summary>
	public KeyPair? KeyPair { get; } = KeyPair;

	/// <summary>
	///     Code
	/// </summary>
	public string? Code { get; } = Code;
}
