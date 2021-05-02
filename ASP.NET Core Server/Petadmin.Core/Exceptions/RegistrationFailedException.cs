using System;

namespace Petadmin.Core.Exceptions
{
    public class RegistrationFailedException : Exception
    {
        public RegistrationFailedException()
            : base("Failed to register new user!") { }
    }
}
