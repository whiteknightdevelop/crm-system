using System;

namespace Petadmin.Core.Exceptions
{
    public class NoneFoundException : Exception
    {
        public NoneFoundException()
            : base($"Could not find any!") { }
    }
}
