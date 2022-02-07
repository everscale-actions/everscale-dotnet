using System;
using EverscaleNet.Abstract;
using EverscaleNet.Adapter.Rust;
using EverscaleNet.Client;
using EverscaleNet.Client.PackageManager;
using EverscaleNet.Models;

// ReSharper disable once CheckNamespace
namespace Microsoft.Extensions.DependencyInjection;

/// <summary>
/// </summary>
public static class AddEverClientServiceCollectionExtensions {
	/// <summary>
	///     Provide IEverClient and IEverPackageManager in DI
	/// </summary>
	/// <param name="services"></param>
	/// <param name="configureEverClientOptions">
	///     Configure network <see cref="EverClientOptions" />
	///     https://github.com/tonlabs/TON-SDK/blob/master/docs/mod_client.md#networkconfig
	///     https://github.com/tonlabs/TON-SDK/blob/master/docs/mod_client.md#cryptoconfig
	///     https://github.com/tonlabs/TON-SDK/blob/master/docs/mod_client.md#abiconfig
	/// </param>
	/// <param name="configurePackageManagerOptions">
	///     Configure package manager, contracts path and etc.
	///     <see cref="FilePackageManagerOptions" />
	/// </param>
	/// <returns></returns>
	public static IServiceCollection AddEverClient(this IServiceCollection services,
	                                               Action<EverClientOptions>? configureEverClientOptions = null,
	                                               Action<FilePackageManagerOptions>? configurePackageManagerOptions = null) {
		if (configureEverClientOptions != null) {
			services.Configure(configureEverClientOptions);
		}
		if (configurePackageManagerOptions != null) {
			services.Configure(configurePackageManagerOptions);
		}

		return services
		       .AddTransient<IEverClientAdapter, EverClientRustAdapter>()
		       .AddTransient<IEverClient, EverClient>()
		       .AddTransient<IEverPackageManager, FilePackageManager>();
	}
}
