using System.Threading.Tasks;

namespace ch1seL.TonClientDotnet
{
    internal interface IRustTonClientCore
    {
        Task<string> Request(string method, string paramsJson);
    }
}