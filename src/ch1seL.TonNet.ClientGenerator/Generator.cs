using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using ch1seL.TonNet.ClientGenerator.Helpers;
using ch1seL.TonNet.ClientGenerator.Models;

namespace ch1seL.TonNet.ClientGenerator
{
    internal static class Generator
    {
        private const string ApiFilePath = "Resources/api.json";
        public const string NameSpace = "ch1seL.TonNet.Client";
        public const string NameSpaceModels = "ch1seL.TonNet.Client.Models";
        private const string OutputPath = @"C:\\Users\\ch1seL\\repos\\ton-actions\\ton-client-dotnet\\ch1seL.TonNet.Client\\Generated\\";
        private static readonly string[] ModulesNamespaces = {"System", "System.Threading", "System.Threading.Tasks", "ch1seL.TonNet.Client.Models"};
        private static readonly string[] ModelsNamespaces = {"System", "System.Numerics", "System.Text.Json", "System.Text.Json.Serialization"};

        public static async Task GenerateClient()
        {
            var tonApi = await JsonSerializer.DeserializeAsync<TonApi>(File.OpenRead(ApiFilePath), TonModelSerializationOptions.Options);

            if (Directory.Exists(OutputPath))
            {
                Directory.Delete(OutputPath,true);
            }

            UnitHelpers.CreateUnit("ITonClient", unitName => ClientClassHelpers.CreateTonClientInterface(unitName, tonApi),
                Path.Combine(OutputPath, "ITonClient.cs"));
            UnitHelpers.CreateUnit("TonClient", unitName => ClientClassHelpers.CreateTonClientClass(unitName, tonApi), Path.Combine(OutputPath, "TonClient.cs"),
                "ch1seL.TonNet.Abstract");

            var allTypes = tonApi!.Modules
                .SelectMany(m => m.Types)
                .Select(t=>NamingConventions.Formatter(t.Name))
                .ToArray();
            
            IReadOnlyDictionary<string, string> numberTypesMapping = NumberUtils.MapNumericTypes(tonApi!.Modules);

            foreach (Module module in tonApi!.Modules)
            {
                UnitHelpers.CreateUnit(module.Name, unitName => ModulesClassHelpers.CreateTonModuleInterface(unitName, module),
                    Path.Combine(OutputPath, nameof(TonApi.Modules), $"I{NamingConventions.Formatter(module.Name)}.cs"), ModulesNamespaces);
                UnitHelpers.CreateUnit(module.Name, unitName => ModulesClassHelpers.CreateTonModuleClass(unitName, module),
                    Path.Combine(OutputPath, nameof(TonApi.Modules), $"{NamingConventions.Formatter(module.Name)}.cs"),
                    ModulesNamespaces.Append("ch1seL.TonNet.Abstract").ToArray());
                
                IReadOnlyCollection<string> moduleTypes = module.Types.Select(t => NamingConventions.Formatter(t.Name)).ToArray();
                var modelClassBuilder = new ModelsClassHelpers(moduleTypes, numberTypesMapping, allTypes);
                
                foreach (TypeElement typeElement in module.Types.Where(t => t.Type != TypeType.None && t.Type != TypeType.Number))
                    UnitHelpers.CreateUnit(module.Name, _ => modelClassBuilder.CreateTonModelClass(typeElement),
                        Path.Combine(OutputPath, "Models", $"{NamingConventions.Formatter(typeElement.Name)}.cs"), ModelsNamespaces);
            }
        }
    }
}