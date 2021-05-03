using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using InforuService;
using Petadmin.Core.Models;
using SmsSender.Models;

namespace Petadmin.Services.Interfaces
{
    public interface ISmsService
    {
        Task<SendSmsDetailedResponse> SendAsync(InforuSms sms);
        Task<IEnumerable<Message>> SendAppointments(List<SmsAppointment> appointments);
        Task<IEnumerable<SmsPreventiveReminder>> GetPreventiveReminderByDateInterval(DateTime from, DateTime to);
        Task<IEnumerable<Message>> SendPreventiveReminders(List<SmsPreventiveReminder> reminders);
    }
}
