﻿using System;
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
    internal static class ClientGenerator
    {
        public const string NameSpace = "ch1seL.TonNet.Client";
        public const string NameSpaceModules = "ch1seL.TonNet.Client.Modules";
        public const string NameSpaceModels = "ch1seL.TonNet.Client.Models";


        private static readonly string[] ModulesNamespaces =
            {"System", "System.Text.Json", "System.Threading", "System.Threading.Tasks", "ch1seL.TonNet.Client.Models", "ch1seL.TonNet.Abstract"};

        private static readonly string[] ModelsNamespaces =
            {"System", "System.Numerics", "System.Text.Json", "System.Text.Json.Serialization", "Dahomey.Json.Attributes"};

        private static readonly JsonSerializerOptions Options = new JsonSerializerOptions {Converters = {new JsonStringEnumConverterWithAttributeSupport()}};

        public static async Task GenerateClient(string repositoryLocation)
        {
            var outputPathClient = Path.Combine(repositoryLocation, "src", "ch1seL.TonNet.Client", "Generated");
            var outputPathModules = Path.Combine(repositoryLocation, "src", "ch1seL.TonNet.Client.Modules", "Generated");
            var outputPathModels = Path.Combine(repositoryLocation, "src", "ch1seL.TonNet.Client.Models", "Generated");

            if (Directory.Exists(outputPathClient)) Directory.Delete(outputPathClient, true);
            if (Directory.Exists(outputPathModules)) Directory.Delete(outputPathModules, true);
            if (Directory.Exists(outputPathModels)) Directory.Delete(outputPathModels, true);
            
            var apiFilePath = Path.Combine(repositoryLocation, "tools", "ClientGenerator", "Resources", "api.json");
            await using FileStream apiFileStream = File.OpenRead(apiFilePath);
            var tonApi = await JsonSerializer.DeserializeAsync<TonApi>(apiFileStream, Options);

            //Create ITonClient
            UnitHelpers.CreateUnit("ITonClient", unitName =>
                ClientClassHelpers.CreateTonClientInterface(unitName, tonApi), Path.Combine(outputPathClient, "ITonClient.cs"), "ch1seL.TonNet.Client.Modules");

            //Create TonClient
            UnitHelpers.CreateUnit("TonClient", unitName =>
                    ClientClassHelpers.CreateTonClientClass(unitName, tonApi), Path.Combine(outputPathClient, "TonClient.cs"),
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
                    Path.Combine(outputPathModules, nameof(TonApi.Modules), $"I{NamingConventions.Normalize(module.Name)}Module.cs"), ModulesNamespaces);

                //Create Modules
                UnitHelpers.CreateUnit(module.Name, unitName => ModulesClassHelpers.CreateTonModuleClass(unitName, module),
                    Path.Combine(outputPathModules, nameof(TonApi.Modules), $"{NamingConventions.Normalize(module.Name)}Module.cs"),
                    ModulesNamespaces.ToArray());

                //Create Models
                var modelClassBuilder = new ModelsClassHelpers(numberTypesMapping, allTypes);
                foreach (TypeElement typeElement in module.Types.Where(t => t.Type != TypeType.None && t.Type != TypeType.Number))
                    UnitHelpers.CreateUnit(module.Name, _ => modelClassBuilder.CreateTonModelClass(typeElement),
                        Path.Combine(outputPathModels, "Models", $"{NamingConventions.Normalize(typeElement.Name)}.cs"), ModelsNamespaces);
            }
        }
    }
}