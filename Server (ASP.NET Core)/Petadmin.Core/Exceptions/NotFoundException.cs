using System;

namespace Petadmin.Core.Exceptions
{
    public class NotFoundException : Exception
    {
        public NotFoundException(int id)
            : base($"Could not find object with id: {id}") { }
    }
}
