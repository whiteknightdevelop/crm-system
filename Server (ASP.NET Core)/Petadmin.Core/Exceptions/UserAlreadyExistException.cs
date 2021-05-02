using System;

namespace Petadmin.Core.Exceptions
{
    public class UserAlreadyExistException : Exception
    {
        public UserAlreadyExistException()
            : base("Conflict - User Already Exist!") { }
    }
}
