using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Petadmin.Core.Models;
using Petadmin.Repository.DbContext;
using Petadmin.Repository.Repositories.Interfaces;

namespace Petadmin.Repository.Repositories
{
    public class SmsRepository : ISmsRepository
    {
        #region Class Initialization
        private readonly IPetadminDbContext _context;
        public SmsRepository(IPetadminDbContext context)
        {
            _context = context;
        }
        #endregion

        public Task<IEnumerable<SmsTemplate>> GetAllSmsTemplates()
        {
            return _context.GetAllSmsTemplates();
        }

        public Task<IEnumerable<SmsPreventiveReminder>> GetPreventiveReminderByDateInterval(DateTime from, DateTime to)
        {
            return _context.GetPreventiveReminderByDateInterval(from, to);
        }
    }
}
