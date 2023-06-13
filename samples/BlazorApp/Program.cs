using BlazorApp;
using BlazorApp.Contracts;
using Blazored.LocalStorage;
using EverscaleNet.Client.Models;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.Logging.SetMinimumLevel(LogLevel.Trace);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services
       .AddEverWebClient((provider, options) => {
	       using IServiceScope scope = provider.CreateScope();
	       var localStorage = scope.ServiceProvider.GetRequiredService<ISyncLocalStorageService>();
	       options.Network.Endpoints = localStorage.GetItem<string[]>(Static.EndpointsStorageKey);
	       options.Network.QueriesProtocol = NetworkQueriesProtocol.WS;
       }, (_, options) => { options.BasePath = builder.HostEnvironment.BaseAddress; })
       .AddTransient<SafeMultisigWallet>()
       .AddBlazoredLocalStorage();

await builder.Build().RunAsync();
