using System.Net.Http.Json;
using System.Text.Json;
using DotNet.Testcontainers.Configurations;
using DotNet.Testcontainers.Containers;
using Microsoft.Extensions.Logging;

namespace TestingExample.Fixtures;

public class WaitNodeSeFirstBlockStrategy : IWaitUntil {
	// Alternative check inside container
	// private readonly string[] _command = {
	// 	"curl", "http://localhost:4000/graphql",
	// 	"-H", "Content-Type: application/json",
	// 	"--data-raw", "{\"query\":\"query blocks {blocks(limit: 1) {id}}\"}"
	// };
	// ExecResult result = await testcontainers.ExecAsync(_command);
	// return result.ExitCode == 0
	//        && !string.IsNullOrWhiteSpace(result.Stdout)
	//        && JsonDocument.Parse(result.Stdout).RootElement
	//                       .TryGetProperty("data", out JsonElement element)
	//        && element.TryGetProperty("blocks", out JsonElement blocks)
	//        && blocks.GetArrayLength() > 0;

	public async Task<bool> UntilAsync(IContainer container) {
		await Task.Delay(TimeSpan.FromMilliseconds(100));
		using var httpClient = new HttpClient {
			BaseAddress = new Uri($"http://localhost:{container.GetMappedPublicPort(80)}"),
			Timeout = TimeSpan.FromSeconds(5)
		};
		HttpResponseMessage response;
		try {
			response = await httpClient.PostAsJsonAsync("graphql", new { query = "query blocks {blocks(limit: 1) {id}}" });
			response.EnsureSuccessStatusCode();
		} catch (HttpRequestException) {
			container.Logger.LogInformation("Waiting for Node SE will be ready..");
			return false;
		}
		var data = await response.Content.ReadFromJsonAsync<JsonElement>();
		return data.TryGetProperty("data", out JsonElement element)
		       && element.TryGetProperty("blocks", out JsonElement blocks)
		       && blocks.ValueKind == JsonValueKind.Array
		       && blocks.GetArrayLength() > 0;
	}
}
