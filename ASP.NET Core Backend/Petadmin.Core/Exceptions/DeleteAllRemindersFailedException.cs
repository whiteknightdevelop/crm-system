using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Petadmin.Core.Exceptions
{
    public class DeleteAllRemindersFailedException : Exception
    {
        public DeleteAllRemindersFailedException()
            : base("Not all reminders was deleted!") { }
    }
}
