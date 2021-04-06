using System;
using System.Text.Json;
using System.Threading.Tasks;
using ch1seL.TonNet.Abstract;
using ch1seL.TonNet.Client.Models;
using ch1seL.TonNet.Client.Tests.Modules.Debot;
using ch1seL.TonNet.Client.Tests.Utils;
using ch1seL.TonNet.Debot;
using ch1seL.TonNet.Debot.Models;
using ch1seL.TonNet.Serialization;
using ch1seL.TonNet.TestsShared;
using Xunit.Abstractions;

namespace ch1seL.TonNet.Client.Tests
{
    public class DebotTestsFixture : TonClientTestsFixture
    {
        private IDebotBrowser _debotBrowser;
        private Lazy<Task<DebotData>> _initDebotDataLazy;
        private ITonClient _tonClient;

        protected internal (Lazy<Task<DebotData>>, IDebotBrowser) Init(ITestOutputHelper output, bool useNodeSe = false)
        {
            _tonClient ??= CreateClient(output, useNodeSe);
            _initDebotDataLazy ??= new Lazy<Task<DebotData>>(() => InitDebot(_tonClient));
            _debotBrowser ??= new DebotTestsBrowser(_tonClient);

            return (_initDebotDataLazy, _debotBrowser);
        }

        private static async Task<DebotData> InitDebot(ITonClient tonClient)
        {
            KeyPair keys = await tonClient.Crypto.GenerateRandomSignKeys();

            Abi targetAbi = TestsEnv.Packages.TestDebotTarget.Abi;
            var targetTvc = TestsEnv.Packages.TestDebotTarget.Tvc;
            Abi debotAbi = TestsEnv.Packages.TestDebot.Abi;
            var debotTvc = TestsEnv.Packages.TestDebot.Tvc;

            var targetFuture = await tonClient.DeployWithGiver(new ParamsOfEncodeMessage
            {
                Abi = targetAbi,
                DeploySet = new DeploySet {Tvc = targetTvc},
                Signer = new Signer.Keys {KeysAccessor = keys},
                CallSet = new CallSet {FunctionName = "constructor"}
            });

            var debotFuture = await tonClient.DeployWithGiver(new ParamsOfEncodeMessage
            {
                Abi = debotAbi,
                DeploySet = new DeploySet {Tvc = debotTvc},
                Signer = new Signer.Keys {KeysAccessor = keys},
                CallSet = new CallSet
                {
                    FunctionName = "constructor",
                    Input = new
                    {
                        debotAbi = JsonSerializer.Serialize(((Abi.Contract) debotAbi).Value, JsonOptionsProvider.JsonSerializerOptions).ToHexString(),
                        targetAbi = JsonSerializer.Serialize(((Abi.Contract) targetAbi).Value, JsonOptionsProvider.JsonSerializerOptions).ToHexString(),
                        targetAddr = targetFuture
                    }.ToJsonElement()
                }
            });

            return new DebotData
            {
                DebotAddr = debotFuture,
                TargetAddr = targetFuture,
                Keys = keys
            };
        }
    }
}