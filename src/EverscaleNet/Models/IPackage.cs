namespace EverscaleNet.Models;

/// <summary>
/// </summary>
public interface IPackage {
	/// <summary>
	///     Abi
	/// </summary>
	Abi? Abi { get; }

	/// <summary>
	///     Tvc
	/// </summary>
	string? Tvc { get; }

	/// <summary>
	///     KeyPair
	/// </summary>
	KeyPair? KeyPair { get; }

	/// <summary>
	///     Code
	/// </summary>
	string? Code { get; }
}
