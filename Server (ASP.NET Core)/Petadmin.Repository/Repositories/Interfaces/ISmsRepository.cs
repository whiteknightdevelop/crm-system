using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Petadmin.Core.Models;

namespace Petadmin.Repository.Repositories.Interfaces
{
    public interface ISmsRepository
    {
        Task<IEnumerable<SmsTemplate>> GetAllSmsTemplates();
        Task<IEnumerable<SmsPreventiveReminder>> GetPreventiveReminderByDateInterval(DateTime from, DateTime to);
    }
}
