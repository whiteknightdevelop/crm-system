using System;

namespace Petadmin.Core.Exceptions
{
    public class SearchFailedException : Exception
    {
        public SearchFailedException()
            : base("System error occurred, contact support.") { }
    }
}
