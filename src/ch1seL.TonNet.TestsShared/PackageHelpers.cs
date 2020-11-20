using System;
using System.IO;
using System.Text.Json;
using System.Threading.Tasks;
using ch1seL.TonNet.Client.Models;
using ch1seL.TonNet.Serialization;

namespace ch1seL.TonNet.TestsShared
{
    internal class PackageHelpers
    {
        private const string AbiPath = "_contracts/abi_v{1}/{0}.abi.json";
        private const string TvcPath = "_contracts/abi_v{1}/{0}.tvc";

        private PackageHelpers(Abi abi, string tvc)
        {
            Abi = abi;
            Tvc = tvc;
        }

        public Abi Abi { get; }
        public string Tvc { get; }

        public static async Task<PackageHelpers> GetPackage(string name, int? abiVersion = null)
        {
            var version = abiVersion ?? TestsEnvironment.CurrentAbiVersion;
            
            var getAbiContractTask = GetAbiContract(name, version);
            var getTvcTask = GetTvc(name, version);
            // do it parallel 
            await Task.WhenAll(getAbiContractTask, getTvcTask);

            AbiContract abiContract = await GetAbiContract(name, version);
            var tvc = await GetTvc(name, version);

            var abi = new Abi.Contract {Value = abiContract};
            return new PackageHelpers(abi, tvc);
        }

        public static async Task<Abi> GetAbi(string name, int? abiVersion = null)
        {
            var version = abiVersion ?? TestsEnvironment.CurrentAbiVersion;
            
            var getAbiContractTask = GetAbiContract(name, version);

            AbiContract abiContract = await GetAbiContract(name, version);

            return new Abi.Contract {Value = abiContract};
        }

        private static async Task<AbiContract> GetAbiContract(string name, int? abiVersion = null)
        {
            var version = abiVersion ?? TestsEnvironment.CurrentAbiVersion;
            
            var filePath = string.Format(AbiPath, name, version);
            var fileInfo = new FileInfo(filePath);
            await using FileStream fs = fileInfo.OpenRead();
            return await JsonSerializer.DeserializeAsync<AbiContract>(fs, JsonOptionsProvider.JsonSerializerOptions);
        }

        private static async Task<string> GetTvc(string name, int? abiVersion = null)
        {
            var version = abiVersion ?? TestsEnvironment.CurrentAbiVersion;
            
            var filePath = string.Format(TvcPath, name, version);
            var bytes = await File.ReadAllBytesAsync(filePath);
            return Convert.ToBase64String(bytes);
        }
    }
}