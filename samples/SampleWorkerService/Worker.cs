using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using ch1seL.TonNet.Client;
using ch1seL.TonNet.Client.Models;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace SampleWorkerService
{
    public class Worker : BackgroundService
    {
        private readonly ILogger<Worker> _logger;
        private readonly ITonClient _tonClient;

        public Worker(ILogger<Worker> logger, ITonClient tonClient)
        {
            _logger = logger;
            _tonClient = tonClient;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            VersionResponse version = await _tonClient.Client.Version(stoppingToken);
            _logger.LogInformation("Ton client version: {version}", version.Version);
            BuildInfoResponse buildInfo = await _tonClient.Client.BuildInfo(stoppingToken);
            _logger.LogInformation("Ton build number: {buildNumber}", buildInfo.BuildNumber);
            
            while (!stoppingToken.IsCancellationRequested)
            {
                _logger.LogInformation("Worker running at: {time}", DateTimeOffset.Now);

                MnemonicFromRandomResponse mnemonic = await _tonClient.Crypto.MnemonicFromRandom(new MnemonicFromRandomRequest(), stoppingToken);
                _logger.LogInformation("Generated mnemonic phrase: {mnemonic}", mnemonic.Phrase);

                await Task.Delay(TimeSpan.FromSeconds(10), stoppingToken);
            }
        }
    }
}