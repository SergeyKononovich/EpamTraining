using System;

namespace Task3_АТS.ATS.Exceptions
{
    public class RegistrationException : Exception
    {
        public RegistrationException() { }
        public RegistrationException(string message)
            : base(message) { }
        public RegistrationException(string message, Exception innerException)
            : base(message, innerException) { }
    }
}
