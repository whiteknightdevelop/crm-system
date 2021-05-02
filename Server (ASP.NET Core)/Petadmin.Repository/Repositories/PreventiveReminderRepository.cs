using Petadmin.Core.Models;
using Petadmin.Repository.DbContext;
using Petadmin.Repository.Repositories.Interfaces;

namespace Petadmin.Repository.Repositories
{
    /// <summary>
    /// Contains methods for interacting with the preventive reminder backend using 
    /// MySQL via Stored Procedures
    /// </summary>
    public class PreventiveReminderRepository : Repository<PreventiveReminder>, IPreventiveReminderRepository
    {
        private readonly IPetadminDbContext _context;
        public PreventiveReminderRepository(IPetadminDbContext context) : base(context)
        {
            _context = context;
        }
    }
}
