using BlazorApp;
using BlazorApp.Contracts;
using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.Extensions.Options;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.Logging.SetMinimumLevel(LogLevel.Trace);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

ConfigureServices(builder.Services, builder.HostEnvironment.BaseAddress);
await builder.Build().RunAsync();

void ConfigureServices(IServiceCollection services, string hostEnvironmentBaseAddress) {
	services
		.AddScoped(_ => new HttpClient { BaseAddress = new Uri(hostEnvironmentBaseAddress) })
		.AddSingleton<IConfigureOptions<EverClientOptions>>(provider => {
			using IServiceScope scope = provider.CreateScope();
			var localStorage = scope.ServiceProvider.GetRequiredService<ISyncLocalStorageService>();
			var options = new ConfigureNamedOptions<EverClientOptions>(null, clientOptions => { clientOptions.Network.Endpoints = localStorage.GetItem<string[]>(Static.EndpointsStorageKey); });
			return options;
		})
		.AddEverClient(options => { options.Network.QueriesProtocol = NetworkQueriesProtocol.WS; })
		.AddTransient<SafeMultisigWallet>()
		.AddBlazoredLocalStorage();
}
