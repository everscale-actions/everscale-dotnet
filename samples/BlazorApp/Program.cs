using BlazorApp;
using BlazorApp.Contracts;
using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.Logging.SetMinimumLevel(LogLevel.Trace);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

ConfigureServices(builder.Services, builder.HostEnvironment.BaseAddress);
await builder.Build().RunAsync();

void ConfigureServices(IServiceCollection services, string hostEnvironmentBaseAddress) {
	services
		.AddScoped(_ => new HttpClient { BaseAddress = new Uri(hostEnvironmentBaseAddress) })
		.AddEverClient((provider, options) => {
			using IServiceScope scope = provider.CreateScope();
			var localStorage = scope.ServiceProvider.GetRequiredService<ISyncLocalStorageService>();
			options.Network.Endpoints = localStorage.GetItem<string[]>(Static.EndpointsStorageKey);
			options.Network.QueriesProtocol = NetworkQueriesProtocol.WS;
		})
		.AddTransient<SafeMultisigWallet>()
		.AddBlazoredLocalStorage();
}
