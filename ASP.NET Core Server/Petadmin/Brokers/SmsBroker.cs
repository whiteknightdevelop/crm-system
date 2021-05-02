using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using InforuPullService;
using InforuService;
using Microsoft.Extensions.Configuration;
using Petadmin.Brokers.Interfaces;
using Petadmin.Core.Models;
using Petadmin.Repository.Interfaces;
using SmsSender.Models;

namespace Petadmin.Brokers
{
    public class SmsBroker : ISmsBroker
    {
        #region Class Initialization
        private readonly IUnitOfWork _unitOfWork;
        private readonly IConfiguration _configuration;
        public SmsBroker(IUnitOfWork unitOfWork, IConfiguration configuration)
        {
            _unitOfWork = unitOfWork;
            _configuration = configuration;
        }
        #endregion

        public async Task<SendSmsDetailedResponse> SendAsync(InforuSms sms)
        {
            var client = new SendMessageSoapClient(SendMessageSoapClient.EndpointConfiguration.SendMessageSoap);
            SendSmsDetailedResponse result = await client.SendSmsDetailedAsync(
                _configuration["SmsSender:Username"], _configuration["SmsSender:ApiToken"], 
                sms.Message, "", 
                "", "", "",
                sms.Recipients, sms.Category,
                sms.Id, sms.MessageInterval,
                new DateTime(1900,01,01,00,00,00),
                _configuration["SmsSender:SenderName"], "0000", 0
                );

            return result;
        }

        public Task<IEnumerable<SmsTemplate>> GetAllTemplates()
        {
            return _unitOfWork.Sms.GetAllSmsTemplates();
        }

        public async Task<PullClientDLRResponse> PullClientDlrAsync(int batchSize)
        {
            var client = new ClientServicesSoapClient(ClientServicesSoapClient.EndpointConfiguration.ClientServicesSoap);
            PullClientDLRResponse result = await client.PullClientDLRAsync(
                _configuration["SmsSender:Username"],
                _configuration["SmsSender:Password"],
                batchSize);

            return result;
        }

        public Task<IEnumerable<SmsPreventiveReminder>> GetPreventiveReminderByDateInterval(DateTime from, DateTime to)
        {
            return _unitOfWork.Sms.GetPreventiveReminderByDateInterval(from, to);
        }
    }
}
