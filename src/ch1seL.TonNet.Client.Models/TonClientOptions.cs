﻿using System.Text.Json.Serialization;
using ch1seL.TonNet.Client.Models;

namespace ch1seL.TonNet.Client; 

public class TonClientOptions {
	[JsonPropertyName("network")]
	public NetworkConfig Network { get; set; } = new NetworkConfig();

	[JsonPropertyName("abi")]
	public AbiConfig Abi { get; set; } = new AbiConfig();

	[JsonPropertyName("crypto")]
	public CryptoConfig Crypto { get; set; } = new CryptoConfig();
}