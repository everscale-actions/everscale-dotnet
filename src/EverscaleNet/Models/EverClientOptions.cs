using System.Text.Json.Serialization;
using EverscaleNet.Client.Models;

// ReSharper disable UnusedMember.Global

namespace EverscaleNet.Models;

/// <summary>
///     Everscale client options. See details https://docs.everos.dev/ever-sdk/reference/types-and-methods/mod_client#config
/// </summary>
public class EverClientOptions : ClientConfig {
	/// <inheritdoc />
	public EverClientOptions() {
		Binding = new BindingConfig();
		Network = new NetworkConfig();
		Crypto = new CryptoConfig();
		Abi = new AbiConfig();
		Boc = new BocConfig();
		Proofs = new ProofsConfig();
	}

	/// <summary>
	/// 
	/// </summary>
	[JsonPropertyName("binding")]
	public new BindingConfig Binding { get; }
}