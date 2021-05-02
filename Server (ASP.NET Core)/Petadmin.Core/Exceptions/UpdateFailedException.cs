using System;

namespace Petadmin.Core.Exceptions
{
    public class UpdateFailedException : Exception
    {
        public UpdateFailedException()
            : base("System error occurred, contact support.") { }
    }
}
