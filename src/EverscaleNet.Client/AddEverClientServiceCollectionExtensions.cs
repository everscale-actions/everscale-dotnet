using System;
using EverscaleNet.Abstract;
using EverscaleNet.Adapter.Rust;
using EverscaleNet.Client;
using EverscaleNet.Client.PackageManager;
using EverscaleNet.Models;
using Microsoft.Extensions.Options;

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
	///     Configure client <see cref="EverClientOptions" />
	///     https://docs.everos.dev/ever-sdk/reference/types-and-methods/mod_client#config
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

	/// <summary>
	///     Provide IEverClient and IEverPackageManager in DI
	/// </summary>
	/// <param name="services"></param>
	/// <param name="configureEverClientOptions">
	///     Configure client <see cref="EverClientOptions" />
	///     https://docs.everos.dev/ever-sdk/reference/types-and-methods/mod_client#config
	/// </param>
	/// <param name="configurePackageManagerOptions">
	///     Configure package manager, contracts path and etc.
	///     <see cref="FilePackageManagerOptions" />
	/// </param>
	/// <returns></returns>
	public static IServiceCollection AddEverClient(this IServiceCollection services,
	                                               Action<IServiceProvider, EverClientOptions>? configureEverClientOptions,
	                                               Action<IServiceProvider, FilePackageManagerOptions>? configurePackageManagerOptions = null) {
		if (configureEverClientOptions != null) {
			services.AddOptions();
			services.AddSingleton<IConfigureOptions<EverClientOptions>>(
				provider => new ConfigureOptions<EverClientOptions>(options => configureEverClientOptions(provider, options)));
		}
		if (configurePackageManagerOptions != null) {
			services.AddOptions();
			services.AddSingleton<IConfigureOptions<FilePackageManagerOptions>>(
				provider => new ConfigureOptions<FilePackageManagerOptions>(options => configurePackageManagerOptions(provider, options)));
		}

		return services
		       .AddTransient<IEverClientAdapter, EverClientRustAdapter>()
		       .AddTransient<IEverClient, EverClient>()
		       .AddTransient<IEverPackageManager, FilePackageManager>();
	}
}
