using System;

namespace Petadmin.Core.Exceptions
{
    public class GetFailedException : Exception
    {
        public GetFailedException()
            : base("System error occurred, contact support.") { }
    }
}
