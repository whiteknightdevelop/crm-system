using System;

namespace Petadmin.Core.Exceptions
{
    public class AddFailedException : Exception
    {
        public AddFailedException()
            : base("System error occurred, contact support.") { }
    }
}
