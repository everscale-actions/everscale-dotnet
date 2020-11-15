using System.Threading.Tasks;

namespace ch1seL.TonNet.RustClient
{
    internal interface IRustTonClientCore
    {
        Task<string> Request(string method, string paramsJson);
    }
}