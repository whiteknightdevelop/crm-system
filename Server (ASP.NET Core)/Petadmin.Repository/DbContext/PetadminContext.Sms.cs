using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Petadmin.Core.Models;

namespace Petadmin.Repository.DbContext
{
    public partial class PetadminContext
    {
        public Task<IEnumerable<SmsTemplate>> GetAllSmsTemplates()
        {
            return Task.Run(() => 
                GetListFromDbAsync("get_all_sms_templates", _dbMappers.SmsTemplateMapper)
                    .Cast<SmsTemplate>()
            );
        }

        public Task<IEnumerable<SmsPreventiveReminder>> GetPreventiveReminderByDateInterval(DateTime from, DateTime to)
        {
            var mySqlCommandParameters = new Dictionary<string, object>
            {
                {"in_from_date", from},
                {"in_to_date", to}
            };
            return Task.Run(() => 
                GetListFromDbAsync("get_sms_preventive_reminders_by_date_interval",
                    _dbMappers.SmsPreventiveReminderMapper, mySqlCommandParameters)
                    .Cast<SmsPreventiveReminder>()
            );
        }
    }
}
