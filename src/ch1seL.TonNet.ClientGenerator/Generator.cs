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
        private static readonly string OutputPath = Path.Combine(Directory.GetCurrentDirectory(), "../ch1seL.TonNet.Client/Generated");

        private static readonly string[] ModulesNamespaces =
            {"System", "System.Threading", "System.Threading.Tasks", "ch1seL.TonNet.Client.Models", "ch1seL.TonNet.Abstract", "ch1seL.TonNet.Client.Abstract"};

        private static readonly string[] ModelsNamespaces = {"System", "System.Numerics", "System.Text.Json", "System.Text.Json.Serialization"};

        public static async Task GenerateClient()
        {
            if (Directory.Exists(OutputPath)) Directory.Delete(OutputPath, true);
            
            
            var tonApi = await JsonSerializer.DeserializeAsync<TonApi>(File.OpenRead(ApiFilePath), TonModelSerializationOptions.Options);

            //Create ITonClient
            UnitHelpers.CreateUnit("ITonClient", unitName => 
                    ClientClassHelpers.CreateTonClientInterface(unitName, tonApi), Path.Combine(OutputPath, "ITonClient.cs"));
            
            //Create TonClient
            UnitHelpers.CreateUnit("TonClient", unitName => 
                    ClientClassHelpers.CreateTonClientClass(unitName, tonApi), Path.Combine(OutputPath, "TonClient.cs"),
                "System", "Microsoft.Extensions.DependencyInjection");

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
                    Path.Combine(OutputPath, nameof(TonApi.Modules), $"I{NamingConventions.Normalize(module.Name)}.cs"), ModulesNamespaces);
             
                //Create Modules
                UnitHelpers.CreateUnit(module.Name, unitName => ModulesClassHelpers.CreateTonModuleClass(unitName, module),
                    Path.Combine(OutputPath, nameof(TonApi.Modules), $"{NamingConventions.Normalize(module.Name)}.cs"),
                    ModulesNamespaces.ToArray());
                
                //Create classes for each module
                var modelClassBuilder = new ModelsClassHelpers(numberTypesMapping, allTypes);
                foreach (TypeElement typeElement in module.Types.Where(t => t.Type != TypeType.None && t.Type != TypeType.Number))
                    UnitHelpers.CreateUnit(module.Name, _ => modelClassBuilder.CreateTonModelClass(typeElement),
                        Path.Combine(OutputPath, "Models", $"{NamingConventions.Normalize(typeElement.Name)}.cs"), ModelsNamespaces);
            }
        }
    }
}