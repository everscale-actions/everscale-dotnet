using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Serilog;

namespace MessageSenderService
{
    public static class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        private static IHostBuilder CreateHostBuilder(string[] args)
        {
            return Host.CreateDefaultBuilder(args)
                .ConfigureServices((_, services) =>
                {
                    services.AddHostedService<Worker>();
                    services.AddTonClient(config =>
                    {
                        config.Network.Endpoints = new[] { "http://localhost:8080" };
                        config.Network.WaitForTimeout = 5000;
                    });
                }).UseSerilog((_, configuration) =>
                {
                    configuration
                        .MinimumLevel.Verbose()
                        .Enrich.FromLogContext()
                        .WriteTo.Console();
                });
        }
    }
}