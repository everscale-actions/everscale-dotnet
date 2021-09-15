using System;
using System.Text.Json;
using System.Threading.Tasks;
using ch1seL.TonNet.Abstract;
using ch1seL.TonNet.RustAdapter;
using ch1seL.TonNet.Serialization;
using FluentAssertions;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Serilog;
using Serilog.Core;
using Xunit;
using Xunit.Abstractions;

namespace ch1seL.TonNet.Client.Tests
{
    public class DisposingTests : IDisposable
    {
        private readonly ServiceProvider _serviceProvider;

        public DisposingTests(ITestOutputHelper output)
        {
            Logger logger = new LoggerConfiguration()
                .MinimumLevel.Verbose()
                .WriteTo.TestOutput(output)
                .CreateLogger();

            _serviceProvider = new ServiceCollection()
                .AddLogging(builder => builder.AddSerilog(logger))
                .AddSingleton<ITonClientAdapter>(provider =>
                {
                    var configJson = JsonSerializer.Serialize(new TonClientOptions(), JsonOptionsProvider.JsonSerializerOptions);
                    return new TonClientRustAdapter(configJson, provider.GetRequiredService<ILogger<TonClientRustAdapter>>());
                })
                .BuildServiceProvider();
        }

        public void Dispose()
        {
            _serviceProvider?.Dispose();
        }

        [Fact]
        public async Task TonClientDisposing()
        {
            var act = new Func<Task>(async () =>
            {
                var tonClient = new TonClient(_serviceProvider.GetRequiredService<ITonClientAdapter>());

                await tonClient.Client.GetApiReference();

                tonClient.Dispose();
            });

            await act.Should().NotThrowAsync();
        }
    }
}