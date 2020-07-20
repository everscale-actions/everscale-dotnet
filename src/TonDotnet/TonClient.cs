using System;
using System.Text.Json;
using System.Threading.Tasks;
using TonDotnet.Models;
using TonDotnet.Rust;
using TonDotnet.Utils;

namespace TonDotnet
{
    public class TonClient : IDisposable
    {
        private static readonly JsonSerializerOptions JsonSerializerOptions = new JsonSerializerOptions
            {IgnoreNullValues = true, MaxDepth = int.MaxValue};

        private readonly int _context;

        public TonClient()
        {
            _context = RustInterface.tc_create_context();
        }

        public void Dispose()
        {
            RustInterface.tc_destroy_context(_context);
        }

        public async Task<Response> Setup(TonClientConfig config)
        {
            var configJson = JsonSerializer.Serialize(config, JsonSerializerOptions);
            return await Request(TonConstants.Methods.Setup, configJson);
        }

        public async Task<Response> Request(string methodName, string paramsJson)
        {
            var methodNameInteropString = MarshalHelpers.StringToInteropString(methodName);
            var paramsJsonInteropString = MarshalHelpers.StringToInteropString(paramsJson);

            string resultJson = null;
            string errorJson = null;

            await Task.Run(() =>
            {
                var resultPtr =
                    RustInterface.tc_json_request(_context, methodNameInteropString, paramsJsonInteropString);
                var jsonResult = RustInterface.tc_read_json_response(resultPtr);
                resultJson = MarshalHelpers.InteropStringToString(jsonResult.ResultJson);
                errorJson = MarshalHelpers.InteropStringToString(jsonResult.ErrorJson);
                RustInterface.tc_destroy_json_response(resultPtr);
            });

            return new Response
            {
                Result = JsonSerializer.Deserialize<JsonElement>(resultJson, JsonSerializerOptions),
                Error = JsonSerializer.Deserialize<JsonElement>(errorJson, JsonSerializerOptions)
            };
        }
    }
}