using System;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.Extensions.DependencyInjection;

namespace BlazorWasm
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebAssemblyHostBuilder.CreateDefault(args);
            builder.RootComponents.Add<App>("#app");

            ConfigureServices(builder.Services, builder.HostEnvironment.BaseAddress);

            await builder.Build().RunAsync();
        }

        private static void ConfigureServices(IServiceCollection services, string hostEnvironmentBaseAddress)
        {
            services
                .AddScoped(sp => new HttpClient { BaseAddress = new Uri(hostEnvironmentBaseAddress) })
                .AddTonClient(options => options.Network.Endpoints = new[] { "net5.ton.dev" })
                .AddTransient<MessageSender>();
        }
    }
}