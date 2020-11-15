using System;

namespace ch1seL.TonNet.RustClient
{
    public class TonClientException : Exception
    {
        public TonClientException(string message):base(message)
        {
        }
    }
}