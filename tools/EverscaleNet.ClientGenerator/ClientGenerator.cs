using System.Text.Json;
using System.Text.Json.Serialization;
using EverscaleNet.ClientGenerator.Helpers;
using Type = EverscaleNet.ClientGenerator.Models.Type;

namespace EverscaleNet.ClientGenerator;

internal static class ClientGenerator {
	public const string NamespaceAbstract = "EverscaleNet.Abstract";
	public const string NamespaceAbstractModules = "EverscaleNet.Abstract.Modules";
	public const string Namespace = "EverscaleNet.Client";
	public const string NamespaceModules = "EverscaleNet.Client.Modules";
	public const string NamespaceModels = "EverscaleNet.Client.Models";

	private static readonly string[] ModulesNamespaces =
		{ "System", "System.Text.Json", "System.Threading", "System.Threading.Tasks", "EverscaleNet.Client.Models" };

	private static readonly string[] ModelsNamespaces =
		{ "System", "System.Numerics", "System.Text.Json", "System.Text.Json.Serialization" };

	private static readonly JsonSerializerOptions Options = new() { Converters = { new JsonStringEnumConverterWithAttributeSupport() } };

	public static async Task GenerateClient(string repositoryLocation) {
		string output = Path.Combine(repositoryLocation, "src", "EverscaleNet", "Generated");

		PrepareDirectory(output);

		string apiFilePath = Path.Combine(repositoryLocation, "tools", "EverscaleNet.ClientGenerator", "Resources", "api.json");
		await using FileStream apiFileStream = File.OpenRead(apiFilePath);
		var everApi = await JsonSerializer.DeserializeAsync<EverApi>(apiFileStream, Options);

		//Create IEverClient
		UnitHelpers.CreateUnit("IEverClient", unitName =>
			                       ClientClassHelpers.CreateClientInterface(unitName, everApi), Path.Combine(output, "IEverClient.Generated.cs"), "EverscaleNet.Abstract.Modules");

		//Create EverClient
		UnitHelpers.CreateUnit("EverClient", unitName =>
			                       ClientClassHelpers.CreateClientClass(unitName, everApi), Path.Combine(output, "EverClient.Generated.cs"),
		                       "System", "EverscaleNet.Abstract", "EverscaleNet.Abstract.Modules", "EverscaleNet.Client.Modules");

		//Save all used types
		Dictionary<string, Type> allTypes = everApi!.Modules
		                                            .SelectMany(m => m.Types)
		                                            .Select(t => new { name = NamingConventions.Normalize(t.Name), type = t.Type })
		                                            .ToDictionary(t => t.name, t => t.type);

		IReadOnlyDictionary<string, string> numberTypesMapping = NumberUtils.MapNumericTypes(everApi!.Modules);

		foreach (Module module in everApi!.Modules) {
			//Create Interface for Modules
			UnitHelpers.CreateUnit(module.Name, unitName => ModulesClassHelpers.CreateModuleInterface(unitName, module),
			                       Path.Combine(output, nameof(EverApi.Modules), $"I{NamingConventions.Normalize(module.Name)}Module.Generated.cs"), ModulesNamespaces);

			//Create Modules
			UnitHelpers.CreateUnit(module.Name, unitName => ModulesClassHelpers.CreateModuleClass(unitName, module),
			                       Path.Combine(output, nameof(EverApi.Modules), $"{NamingConventions.Normalize(module.Name)}Module.Generated.cs"),
			                       ModulesNamespaces.Union(new[] { "EverscaleNet.Abstract", "EverscaleNet.Abstract.Modules" }).ToArray());

			//Create Models
			var modelClassBuilder = new ModelsClassHelpers(numberTypesMapping, allTypes);
			foreach (TypeElement typeElement in module.Types.Where(t => t.Type != Type.None && t.Type != Type.Number)) {
				UnitHelpers.CreateUnit(module.Name, _ => modelClassBuilder.CreateModelClass(typeElement),
				                       Path.Combine(output, "Models", $"{NamingConventions.Normalize(typeElement.Name)}.Generated.cs"), ModelsNamespaces);
			}
		}
	}

	private static void PrepareDirectory(string path) {
		if (Directory.Exists(path)) {
			Directory.Delete(path, true);
		}
	}
}
