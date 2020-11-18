using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Xunit.Abstractions;

namespace ch1seL.TonNet.Client.Tests
{
    public abstract class TonClientTestsBase //: IDisposable todo: ??  freeze on linux WTF
    {
        private readonly ServiceProvider _serviceProvider;

        protected TonClientTestsBase(ITestOutputHelper testOutputHelper)
        {
            _serviceProvider = new ServiceCollection()
                .AddLogging(builder => builder.AddXUnit(testOutputHelper)
                    .AddFilter(level => level == LogLevel.Trace))
                .AddSingleton<ITonClient, TonClient>()
                .BuildServiceProvider();

            TonClient = _serviceProvider.GetRequiredService<ITonClient>();
        }

        protected ITonClient TonClient { get; }

        public void Dispose()
        {
            _serviceProvider?.Dispose();
        }
    }
}