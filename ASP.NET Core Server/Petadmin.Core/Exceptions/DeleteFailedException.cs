using System;

namespace Petadmin.Core.Exceptions
{
    public class DeleteFailedException : Exception
    {
        public DeleteFailedException()
            : base("System error occurred, contact support.") { }
    }
}
