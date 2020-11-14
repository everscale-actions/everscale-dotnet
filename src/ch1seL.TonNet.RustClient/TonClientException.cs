using System;

namespace ch1seL.TonClientDotnet
{
    public class TonClientException : Exception
    {
        public TonClientException(string message):base(message)
        {
        }
    }
}