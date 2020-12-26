using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.Text.Json;
using System.Text.Json.Serialization;
using ch1seL.TonNet.Serialization;

namespace ch1seL.TonNet.TestsShared
{
    [SuppressMessage("ReSharper", "UnusedAutoPropertyAccessor.Global")]
    [SuppressMessage("ReSharper", "ClassNeverInstantiated.Global")]
    internal class Elector
    {
        public static readonly Elector Instance = GetElector();

        [JsonPropertyName("id")] public string Id { get; set; }

        [JsonPropertyName("code")] public string Code { get; set; }

        [JsonPropertyName("data")] public string Data { get; set; }

        private static Elector GetElector()
        {
            using FileStream fileStream = File.OpenRead(Path.Join("_contracts", "elector.json"));
            JsonDocument jsonDoc = JsonDocument.Parse(fileStream);
            return jsonDoc.RootElement.ToObject<Elector>();
        }
    }
}