using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using ch1seL.TonNet.ClientGenerator.Helpers;
using ch1seL.TonNet.ClientGenerator.Models;

namespace ch1seL.TonNet.ClientGenerator
{
    internal static class Generator
    {
        private const string ApiFilePath = "Resources/api.json";
        public const string NameSpace = "ch1seL.TonNet.Client";
        public const string NameSpaceModules = "ch1seL.TonNet.Client.Modules";
        public const string NameSpaceModels = "ch1seL.TonNet.Client.Models";
        private static readonly string OutputPathClient = Path.Combine(Directory.GetCurrentDirectory(), "../../src/ch1seL.TonNet.Client/Generated");
        private static readonly string OutputPathModules = Path.Combine(Directory.GetCurrentDirectory(), "../../src/ch1seL.TonNet.Client.Modules/Generated");
        private static readonly string OutputPathModels = Path.Combine(Directory.GetCurrentDirectory(), "../../src/ch1seL.TonNet.Client.Models/Generated");

        private static readonly string[] ModulesNamespaces =
            {"System", "System.Text.Json", "System.Threading", "System.Threading.Tasks", "ch1seL.TonNet.Client.Models", "ch1seL.TonNet.Abstract"};

        private static readonly string[] ModelsNamespaces =
            {"System", "System.Numerics", "System.Text.Json", "System.Text.Json.Serialization", "Dahomey.Json.Attributes"};

        private static readonly JsonSerializerOptions Options = new JsonSerializerOptions {Converters = {new JsonStringEnumConverterWithAttributeSupport()}};

        public static async Task GenerateClient()
        {
            if (Directory.Exists(OutputPathClient)) Directory.Delete(OutputPathClient, true);
            if (Directory.Exists(OutputPathModules)) Directory.Delete(OutputPathModules, true);
            if (Directory.Exists(OutputPathModels)) Directory.Delete(OutputPathModels, true);

            var tonApi = await JsonSerializer.DeserializeAsync<TonApi>(File.OpenRead(ApiFilePath), Options);

            //Create ITonClient
            UnitHelpers.CreateUnit("ITonClient", unitName =>
                ClientClassHelpers.CreateTonClientInterface(unitName, tonApi), Path.Combine(OutputPathClient, "ITonClient.cs"), "ch1seL.TonNet.Client.Modules");

            //Create TonClient
            UnitHelpers.CreateUnit("TonClient", unitName =>
                    ClientClassHelpers.CreateTonClientClass(unitName, tonApi), Path.Combine(OutputPathClient, "TonClient.cs"),
                "System", "Microsoft.Extensions.DependencyInjection", "ch1seL.TonNet.Client.Modules");

            //Save all used types
            var allTypes = tonApi!.Modules
                .SelectMany(m => m.Types)
                .Select(t => NamingConventions.Normalize(t.Name))
                .ToArray();

            IReadOnlyDictionary<string, string> numberTypesMapping = NumberUtils.MapNumericTypes(tonApi!.Modules);

            foreach (Module module in tonApi!.Modules)
            {
                //Create Interface for Modules
                UnitHelpers.CreateUnit(module.Name, unitName => ModulesClassHelpers.CreateTonModuleInterface(unitName, module),
                    Path.Combine(OutputPathModules, nameof(TonApi.Modules), $"I{NamingConventions.Normalize(module.Name)}Module.cs"), ModulesNamespaces);

                //Create Modules
                UnitHelpers.CreateUnit(module.Name, unitName => ModulesClassHelpers.CreateTonModuleClass(unitName, module),
                    Path.Combine(OutputPathModules, nameof(TonApi.Modules), $"{NamingConventions.Normalize(module.Name)}Module.cs"),
                    ModulesNamespaces.ToArray());

                //Create Models
                var modelClassBuilder = new ModelsClassHelpers(numberTypesMapping, allTypes);
                foreach (TypeElement typeElement in module.Types.Where(t => t.Type != TypeType.None && t.Type != TypeType.Number))
                    UnitHelpers.CreateUnit(module.Name, _ => modelClassBuilder.CreateTonModelClass(typeElement),
                        Path.Combine(OutputPathModels, "Models", $"{NamingConventions.Normalize(typeElement.Name)}.cs"), ModelsNamespaces);
            }
        }
    }
}