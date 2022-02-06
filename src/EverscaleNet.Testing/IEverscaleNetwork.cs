using EverscaleNet.Client.Models;

namespace EverscaleNet.Testing;

/// <summary>
/// Network
/// </summary>
public interface IEverscaleNetwork {
	/// <summary>
	/// 
	/// </summary>
	string[] Endpoints { get; }
	
	/// <summary>
	/// 
	/// </summary>
	KeyPair GiverKeyPair { get; }
}
