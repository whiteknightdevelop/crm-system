using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using InforuPullService;
using InforuService;
using Petadmin.Core.Models;
using SmsSender.Models;

namespace Petadmin.Brokers.Interfaces
{
    public interface ISmsBroker
    {
        Task<SendSmsDetailedResponse> SendAsync(InforuSms sms);
        Task<IEnumerable<SmsTemplate>> GetAllTemplates();
        Task<PullClientDLRResponse> PullClientDlrAsync(int batchSize);
        Task<IEnumerable<SmsPreventiveReminder>> GetPreventiveReminderByDateInterval(DateTime from, DateTime to);
    }
}
