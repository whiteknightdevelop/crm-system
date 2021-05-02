using System;

namespace Petadmin.Core.Exceptions
{
    public class NotExpectedException : Exception
    {
        public NotExpectedException(Exception innerException)
            : base("System error occurred, contact support.", innerException) { }
    }
}
