using System.Reflection;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using ch1seL.TonNet.Abstract;

namespace ch1seL.TonClientDotnet
{
    public static class RastTonClientCoreExtensions
    {
        internal static async Task<TResponse> Request<TRequest, TResponse>(this IRustTonClientCore rustTonClientCore, string method, TRequest request,
            CancellationToken cancellationToken) 
            where TRequest : ITonClientRequest
            where TResponse : ITonClientResponse
        {
            var paramsJson = JsonSerializer.Serialize(request, RustTonClientCore.JsonSerializerOptions);

            var responseJson = await rustTonClientCore.Request(method, paramsJson);

            return JsonSerializer.Deserialize<TResponse>(responseJson, RustTonClientCore.JsonSerializerOptions);
        }
    }
}