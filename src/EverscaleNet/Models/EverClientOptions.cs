﻿using System.Text.Json.Serialization;
using EverscaleNet.Client.Models;

namespace EverscaleNet.Models;

/// <summary>
///     Everscale client options. See details https://tonlabs.gitbook.io/ton-sdk/guides/installation/configure_sdk#configure-client
/// </summary>
public class EverClientOptions {
	/// <summary>
	///     https://tonlabs.gitbook.io/ton-sdk/guides/installation/configure_sdk#network-config
	///     see available endpoint URLs https://tonlabs.gitbook.io/ton-sdk/reference/ton-os-api/networks#networks
	/// </summary>
	[JsonPropertyName("network")]
	public NetworkConfig Network { get; set; } = new();

	/// <summary>
	///     https://tonlabs.gitbook.io/ton-sdk/guides/installation/configure_sdk#crypto-config
	/// </summary>
	[JsonPropertyName("crypto")]
	public CryptoConfig Crypto { get; set; } = new();

	/// <summary>
	///     https://tonlabs.gitbook.io/ton-sdk/guides/installation/configure_sdk#abi-config
	/// </summary>
	[JsonPropertyName("abi")]
	public AbiConfig Abi { get; set; } = new();
}