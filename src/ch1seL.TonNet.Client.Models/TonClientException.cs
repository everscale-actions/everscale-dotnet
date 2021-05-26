using System;
using System.Collections.Generic;

namespace ch1seL.TonNet.Client
{
    public class TonClientException : Exception
    {
        public TonClientException(string message = null, Exception inner = null) : base(message, inner)
        {
        }

        public uint Code { get; private set; }

        public static TonClientException CreateExceptionWithCodeWithData(uint code, IDictionary<string, object> data = null, string message = null,
            Exception inner = null)
        {
            var exception = new TonClientException(message, inner) {Code = code};
            if (data == null) return exception;

            foreach (var (key, value) in data) exception.Data.Add(key, value);
            return exception;
        }
    }
}