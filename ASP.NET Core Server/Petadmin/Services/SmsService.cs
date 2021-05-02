using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using IdentityServer4.Extensions;
using InforuService;
using Microsoft.Extensions.Logging;
using Petadmin.Brokers.Interfaces;
using Petadmin.Core.Exceptions;
using Petadmin.Core.Models;
using Petadmin.Services.Interfaces;
using SmsSender.Models;
using System.Xml;
using System.Xml.Linq;
using InforuPullService;
using Petadmin.Core.Constants;

namespace Petadmin.Services
{
    public class SmsService : ISmsService
    {
        #region Class Initialization
        private readonly ILogger<SmsService> _logger;
        private readonly ISmsBroker _smsBroker;
        private List<SmsTemplate> _templatesList;
        public SmsService(ISmsBroker smsBroker, ILogger<SmsService> logger)
        {
            _smsBroker = smsBroker;
            _logger = logger;
        }
        #endregion

        public Task<SendSmsDetailedResponse> SendAsync(InforuSms sms)
        {
            return _smsBroker.SendAsync(sms);
        }

        public async Task<IEnumerable<Message>> SendAppointments(List<SmsAppointment> appointments)
        {
            try
            {
                ClientNotification result = null;
                var toSendList = new List<InforuSms>();
                var tasks = new List<Task<SendSmsDetailedResponse>>();

                ClearPullQueue();

                await GetAllSmsTemplatesList();

                foreach (var appointment in appointments)
                {
                    toSendList.Add(BuildAppointmentSms(appointment));
                }

                foreach (var sms in toSendList)
                {
                    tasks.Add(_smsBroker.SendAsync(sms));
                }

                var results = await Task.WhenAll(tasks);
                if (results.All(CheckIfResponseStatusCodeIsSuccess))
                {
                    Thread.Sleep(15000);
                    var pullResponse = await PullClientDlr(results.Length);
                    result = XmlToClientNotificationConverter(pullResponse.Body.PullClientDLRResult);
                }
                return result?.Messages;
            }
            catch (Exception e)
            {
                _logger.LogError(e, e.Message);
                throw new SendSmsFailedException();
            }
        }

        public async Task<IEnumerable<Message>> SendPreventiveReminders(List<SmsPreventiveReminder> reminders)
        {
            try
            {
                ClientNotification result = null;
                var tasks = new List<Task<SendSmsDetailedResponse>>();

                ClearPullQueue(); // Clear queue

                await GetAllSmsTemplatesList();

                List<InforuSms> toSendList = BuildRemindersToSendList(reminders);

                foreach (var sms in toSendList)
                {
                    tasks.Add(_smsBroker.SendAsync(sms));
                }

                var results = await Task.WhenAll(tasks);
                if (results.All(CheckIfResponseStatusCodeIsSuccess))
                {
                    Thread.Sleep(15000);
                    var pullResponse = await PullClientDlr(results.Length);
                    result = XmlToClientNotificationConverter(pullResponse.Body.PullClientDLRResult);
                }
                return result?.Messages;
            }
            catch (Exception e)
            {
                _logger.LogError(e, e.Message);
                throw new SendSmsFailedException();
            }
        }

        private async Task GetAllSmsTemplatesList()
        {
            if (_templatesList.IsNullOrEmpty())
            {
                _templatesList = (await _smsBroker.GetAllTemplates()).ToList();
            }
        }

        private async Task<ClientNotification> ReadPullResponseFromTxtFileToClientNotification()
        {
            var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "sms_appointment_response.txt");
            string buffer = await File.ReadAllTextAsync(path);
            var result = XmlToClientNotificationConverter(buffer);
            return result;
        }

        private async Task WritePullResponseToTxtFile(PullClientDLRResponse pullResponse)
        {
            var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "sms_appointment_response.txt");
            await File.WriteAllTextAsync(path, pullResponse.Body.PullClientDLRResult);
        }

        private ClientNotification XmlToClientNotificationConverter(string xml)
        {
            XDocument doc = XDocument.Parse(xml);
            var result = (from c in doc.Descendants("ClientNotification")
                select new ClientNotification()
                {
                    Status = c.Element("Status")?.Value,
                    BatchSize = int.Parse(c.Element("BatchSize")?.Value ?? string.Empty),
                    Messages = from f in c.Descendants("Message")
                        select new Message
                        {
                            Type = f.Element("Type")?.Value,
                            PhoneNumber = f.Element("PhoneNumber")?.Value,
                            Network = f.Element("Network")?.Value,
                            Status = int.Parse(f.Element("Status")?.Value ?? string.Empty),
                            StatusDescription = f.Element("StatusDescription")?.Value,
                            CustomerMessageId = int.Parse(f.Element("CustomerMessageId")?.Value ?? string.Empty),
                            CustomerParam = f.Element("CustomerParam")?.Value,
                            SenderNumber = f.Element("SenderNumber")?.Value,
                            SegmentsNumber = int.Parse(f.Element("SegmentsNumber")?.Value ?? string.Empty),
                            NotificationDate = DateTime.Parse(f.Element("NotificationDate")?.Value ?? string.Empty),
                            SentMessage = f.Element("SentMessage")?.Value,
                        }
                }).FirstOrDefault();
            return result;
        }

        public Task<IEnumerable<SmsPreventiveReminder>> GetPreventiveReminderByDateInterval(DateTime from, DateTime to)
        {
            try
            {
                return _smsBroker.GetPreventiveReminderByDateInterval(from, to);
            }
            catch (Exception e)
            {
                _logger.LogError(e, e.Message);
                throw new GetFailedException();
            }
        }

        private Task<PullClientDLRResponse> PullClientDlr(int batchSize)
        {
            return _smsBroker.PullClientDlrAsync(batchSize);
        }

        private bool CheckIfResponseStatusCodeIsSuccess(SendSmsDetailedResponse item)
        {
            var xmlDoc = new XmlDocument();
            xmlDoc.LoadXml(item.Body.SendSmsDetailedResult);
            var status = xmlDoc.GetElementsByTagName("Status")[0]?.InnerText;
            return status != null && status.Equals("1");
        }

        private InforuSms BuildAppointmentSms(SmsAppointment appointment)
        {
            return new()
            {
                Id = appointment.Id,
                Category = appointment.Category,
                Message = BuildAppointmentMassageFromTemplate(appointment),
                Recipients = appointment.Recipient
            };
        }

        private List<InforuSms> BuildRemindersToSendList(List<SmsPreventiveReminder> reminders)
        {
            var toSendList = new List<InforuSms>();
            while (reminders.Any())
            {
                SmsPreventiveReminder selected = reminders.First();

                List<string> animalsNames = reminders.Where(reminder => reminder.Phone.Equals(selected.Phone))
                    .Select(reminder => reminder.Name).Distinct().ToList();

                List<string> treatments = reminders.Where(reminder => reminder.Phone.Equals(selected.Phone))
                    .Select(reminder => reminder.Treatment).Distinct().ToList();

                reminders.RemoveAll(reminder => reminder.Phone.Equals(selected.Phone));

                toSendList.Add(BuildReminderSms(selected, animalsNames, treatments));
            }
            return toSendList;
        }

        private InforuSms BuildReminderSms(SmsPreventiveReminder reminder, List<string> animalsNames, List<string> treatments)
        {
            return new()
            {
                Id = reminder.Id,
                Category = reminder.Category,
                Message = BuildReminderMassageFromTemplate(reminder, animalsNames),
                Recipients = reminder.Phone
            };
        }

        private string BuildAppointmentMassageFromTemplate(SmsAppointment appointment)
        {
            var tomorrow = DateTime.Today.AddDays(1);
            if (appointment.Date.Date == tomorrow.Date)
            {
                return BuildAppointmentTomorrowMassageFromTemplate(appointment);
            }
            return BuildAppointmentByDateMassageFromTemplate(appointment);
        }

        private string BuildAppointmentTomorrowMassageFromTemplate(SmsAppointment appointment)
        {
            string template = _templatesList.First(t => t.Title.Equals(SmsTemplateConstants.AppointmentTomorrowTitle))?.Template;

            string massage = template?.Replace(SmsTemplateConstants.FirstName, appointment.Name);
            massage = massage?.Replace(SmsTemplateConstants.Time, GetAppointmentLocalTime(appointment));

            return massage;
        }

        private string BuildAppointmentByDateMassageFromTemplate(SmsAppointment appointment)
        {
            string template = _templatesList.First(t => t.Title.Equals(SmsTemplateConstants.AppointmentByDateTitle))?.Template;

            string massage = template?.Replace(SmsTemplateConstants.FirstName, appointment.Name);
            massage = massage?.Replace(SmsTemplateConstants.Date, appointment.Date.ToString("dd/MM/yy"));
            massage = massage?.Replace(SmsTemplateConstants.Time, GetAppointmentLocalTime(appointment));
            
            return massage;
        }
            
        private string GetAppointmentLocalTime(SmsAppointment appointment)
        {
            return appointment.Date.ToLocalTime().ToString("HH:mm");
        }

        private string BuildReminderMassageFromTemplate(SmsPreventiveReminder reminder, List<string> animalsNames)
        {
            string template = _templatesList.First(t => t.Title.Equals(SmsTemplateConstants.PreventiveReminderTitle))?.Template;

            string animalsNamesStr = string.Join(",", animalsNames);
            string massage = template?.Replace(SmsTemplateConstants.FirstName, reminder.FirstName);

            massage = massage?.Replace(SmsTemplateConstants.AmimalName, animalsNamesStr);
            
            return massage;
        }

        private async void ClearPullQueue()
        {
            await PullClientDlr(100);
        }
    }
}
