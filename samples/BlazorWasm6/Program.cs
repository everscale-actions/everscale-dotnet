using BlazorWasm6;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");
ConfigureServices(builder.Services, builder.HostEnvironment.BaseAddress);
await builder.Build().RunAsync();

void ConfigureServices(IServiceCollection services, string hostEnvironmentBaseAddress)
{
    services
        .AddScoped(_ => new HttpClient { BaseAddress = new Uri(hostEnvironmentBaseAddress) })
        .AddTonClient(options => options.Network.Endpoints = new[] { "net5.ton.dev" })
        .AddTransient<MessageSender>();
}