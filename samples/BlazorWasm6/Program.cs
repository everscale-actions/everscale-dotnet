using BlazorWasm6;
using EverscaleNet.Utils;
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
		.AddEverClient(options => options.Network.Endpoints = EverOS.Endpoints.Development)
		.AddTransient<MessageSender>();
}
