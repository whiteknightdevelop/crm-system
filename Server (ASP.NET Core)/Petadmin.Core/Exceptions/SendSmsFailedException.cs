using System;

namespace Petadmin.Core.Exceptions
{
    public class SendSmsFailedException : Exception
    {
        public SendSmsFailedException()
            : base("System error occurred, contact support.") { }
    }
}
