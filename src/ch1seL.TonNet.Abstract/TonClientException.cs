using System;

namespace ch1seL.TonNet.Abstract
{
    // todo: seems like needed separate class for general classes  
    public class TonClientException : Exception
    {
        public TonClientException(string message, Exception inner = null):base(message, inner)
        {
        }
    }
}