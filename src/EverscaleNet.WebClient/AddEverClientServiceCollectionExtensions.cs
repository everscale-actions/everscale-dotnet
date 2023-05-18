using EverscaleNet;
using EverscaleNet.Abstract;
using EverscaleNet.Adapter.Wasm;
using EverscaleNet.Client;
using EverscaleNet.Models;
using EverscaleNet.WebClient.PackageManager;
using Microsoft.Extensions.Options;

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
	///     Configure client <see cref="EverClientOptions" />
	///     https://docs.everos.dev/ever-sdk/reference/types-and-methods/mod_client#config
	/// </param>
	/// <param name="configureLibWebOptions">
	///     Configure lib web <see cref="LibWebOptions" />
	///     <see href="https://github.com/tonlabs/ever-sdk-js#setup-library" />
	/// </param>
	/// <param name="configurePackageManagerOptions">
	///     Configure package manager, contracts path and etc.
	///     <see cref="PackageManagerOptions" />
	/// </param>
	/// <returns></returns>
	// ReSharper enable CommentTypo
	public static IServiceCollection AddEverClient(this IServiceCollection services,
	                                               Action<EverClientOptions>? configureEverClientOptions = null,
	                                               Action<LibWebOptions>? configureLibWebOptions = null,
	                                               Action<PackageManagerOptions>? configurePackageManagerOptions = null) {
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

	// ReSharper disable CommentTypo
	/// <summary>
	///     Provide IEverClient and IEverPackageManager in DI
	/// </summary>
	/// <param name="services"></param>
	/// <param name="configureEverClientOptions">
	///     Configure client <see cref="EverClientOptions" />
	///     https://docs.everos.dev/ever-sdk/reference/types-and-methods/mod_client#config
	/// </param>
	/// <param name="configureLibWebOptions">
	///     Configure lib web <see cref="LibWebOptions" />
	///     <see href="https://github.com/tonlabs/ever-sdk-js#setup-library" />
	/// </param>
	/// <param name="configurePackageManagerOptions">
	///     Configure package manager, contracts path and etc.
	///     <see cref="PackageManagerOptions" />
	/// </param>
	/// <returns></returns>
	// ReSharper enable CommentTypo
	public static IServiceCollection AddEverClient(this IServiceCollection services,
	                                               Action<IServiceProvider, EverClientOptions>? configureEverClientOptions,
	                                               Action<IServiceProvider, LibWebOptions>? configureLibWebOptions = null,
	                                               Action<IServiceProvider, PackageManagerOptions>? configurePackageManagerOptions = null) {
		if (configureEverClientOptions != null) {
			services.AddOptions();
			services.AddSingleton<IConfigureOptions<EverClientOptions>>(
				provider => new ConfigureOptions<EverClientOptions>(options => configureEverClientOptions(provider, options)));
		}
		if (configureLibWebOptions != null) {
			services.AddOptions();
			services.AddSingleton<IConfigureOptions<LibWebOptions>>(
				provider => new ConfigureOptions<LibWebOptions>(options => configureLibWebOptions(provider, options)));
		}
		if (configurePackageManagerOptions != null) {
			services.AddOptions();
			services.AddSingleton<IConfigureOptions<PackageManagerOptions>>(
				provider => new ConfigureOptions<PackageManagerOptions>(options => configurePackageManagerOptions(provider, options)));
		}

		return services
		       .AddTransient<IEverClientAdapter, EverClientWasmAdapter>()
		       .AddTransient<IEverClient, EverClient>()
		       .AddTransient<IEverPackageManager, WebPackageManager>();
	}
}
