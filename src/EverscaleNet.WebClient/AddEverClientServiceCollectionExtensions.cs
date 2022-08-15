using System;
using EverscaleNet.Abstract;
using EverscaleNet.Adapter.Wasm;
using EverscaleNet.Client;
using EverscaleNet.Models;
using EverscaleNet.WebClient.PackageManager;

// ReSharper disable once CheckNamespace
namespace Microsoft.Extensions.DependencyInjection;

/// <summary>
/// </summary>
public static class AddEverClientServiceCollectionExtensions {
	// ReSharper disable CommentTypo
	/// <summary>
	///     Provide IEverClient and IEverPackageManager in DI
	/// </summary>
	/// <param name="services"></param>
	/// <param name="configureEverClientOptions">
	///     Configure network <see cref="EverClientOptions" />
	///     <see href="https://github.com/tonlabs/ever-sdk/blob/master/docs/mod_client.md#networkconfig"/> 
	///     <see href="https://github.com/tonlabs/ever-sdk/blob/master/docs/mod_client.md#cryptoconfig"/>
	///     <see href="https://github.com/tonlabs/ever-sdk/blob/master/docs/mod_client.md#abiconfig"/>
	/// </param>
	/// <param name="configureLibWebOptions">
	///     Configure lib web <see cref="LibWebOptions" />
	///		<see href="https://github.com/tonlabs/ever-sdk-js#setup-library"/>
	/// </param>
	/// <param name="configurePackageManagerOptions">
	///     Configure package manager, contracts path and etc.
	///     <see cref="WebPackageManager" />
	/// </param>
	/// <returns></returns>
	// ReSharper enable CommentTypo
	public static IServiceCollection AddEverClient(this IServiceCollection services,
	                                               Action<EverClientOptions>? configureEverClientOptions = null,
	                                               Action<LibWebOptions>? configureLibWebOptions = null,
	                                               Action<WebPackageManagerOptions>? configurePackageManagerOptions = null) {
		if (configureEverClientOptions != null) {
			services.Configure(configureEverClientOptions);
		}
		if (configureLibWebOptions != null) {
			services.Configure(configureLibWebOptions);
		}
		if (configurePackageManagerOptions != null) {
			services.Configure(configurePackageManagerOptions);
		}

		return services
		       .AddTransient<IEverClientAdapter, EverClientWasmAdapter>()
		       .AddTransient<IEverClient, EverClient>()
		       .AddTransient<IEverPackageManager, WebPackageManager>();
	}
}
