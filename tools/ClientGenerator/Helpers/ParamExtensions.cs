using ch1seL.TonNet.ClientGenerator.Models;

namespace ch1seL.TonNet.ClientGenerator.Helpers
{
    internal static class ParamExtensions
    {
        public static bool IsClientContextParam(this Param p)
        {
            return p.Type == ParamType.Generic
                   && p.GenericArgs.Length == 1
                   && p.GenericArgs[0].Type == GenericArgType.Ref
                   && p.GenericArgs[0].RefName == GenericRefNames.ClientContext;
        }
    }
}