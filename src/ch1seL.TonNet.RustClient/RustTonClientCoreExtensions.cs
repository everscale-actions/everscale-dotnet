using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;

namespace ch1seL.TonNet.RustClient
{
    public static class RustTonClientCoreExtensions
    {
        internal static async Task<TResponse> Request<TRequest, TResponse>(this IRustTonClientCore rustTonClientCore, string method, TRequest request,
                CancellationToken cancellationToken = default)
            // where TRequest : ITonClientRequest
            // where TResponse : ITonClientResponse
        {
            var paramsJson = JsonSerializer.Serialize(request, RustTonClientCore.JsonSerializerOptions);

            var responseJson = await rustTonClientCore.Request(method, paramsJson, cancellationToken);

            return JsonSerializer.Deserialize<TResponse>(responseJson, RustTonClientCore.JsonSerializerOptions);
        }

        internal static async Task<TResponse> Request<TResponse>(this IRustTonClientCore rustTonClientCore, string method,
                CancellationToken cancellationToken = default)
            // where TRequest : ITonClientRequest
            // where TResponse : ITonClientResponse
        {
            var responseJson = await rustTonClientCore.Request(method, string.Empty, cancellationToken);

            return JsonSerializer.Deserialize<TResponse>(responseJson, RustTonClientCore.JsonSerializerOptions);
        }
    }
}