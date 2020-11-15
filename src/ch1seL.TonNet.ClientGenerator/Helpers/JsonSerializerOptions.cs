using System.Text.Json;
using System.Text.Json.Serialization;

namespace ch1seL.TonNet.ClientGenerator.Helpers
{
    public static class TonModelSerializationOptions
    {
        public static JsonSerializerOptions Options = new JsonSerializerOptions
        {
            WriteIndented = true,
            Converters = {new JsonStringEnumConverterWithAttributeSupport()}
        };
    }
}