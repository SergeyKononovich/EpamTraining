using System;

namespace Task3_АТS.Exceptions
{
    public class StateException : Exception
    {
        public StateException() { }
        public StateException(string message)
            : base(message) { }
        public StateException(string message, Exception innerException)
            : base(message, innerException) { }
    }
}
