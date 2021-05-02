using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Petadmin.Core.Models;
using Petadmin.Identity;
using Petadmin.Models;
using Petadmin.Services.Interfaces;
using SmsSender.Models;

namespace Petadmin.Controllers
{
    [Authorize (Roles = UserRoles.Admin)]
    [Route("api/[controller]")]
    [ApiController]
    public class SmsController : ControllerBase
    {
        private readonly ISmsService _smsService;

        public SmsController(ISmsService smsService)
        {
            _smsService = smsService;
        }

        // POST api/sms/send-appointments
        [HttpPost]
        [Route("send-appointments")]
        public async Task<ActionResult<List<Message>>> SendAppointments([FromBody] List<SmsAppointment> appointments)
        {
            try
            {
                List<Message> list = (await _smsService.SendAppointments(appointments)).ToList();
                return list;
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        // POST api/sms/preventive-reminder
        [HttpPost]
        [Route("preventive-reminder")]
        public async Task<ActionResult<List<SmsPreventiveReminder>>> GetPreventiveReminderByDateInterval([FromBody] DateIntervalRequest dateInterval)
        {
            try
            {
                List<SmsPreventiveReminder> list = (await _smsService.GetPreventiveReminderByDateInterval(dateInterval.From, dateInterval.To)).ToList();
                return list;
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        // POST api/sms/send-reminders
        [HttpPost]
        [Route("send-reminders")]
        public async Task<ActionResult<List<Message>>> SendPreventiveReminders([FromBody] List<SmsPreventiveReminder> reminders)
        {
            try
            {
                List<Message> list = (await _smsService.SendPreventiveReminders(reminders)).ToList();
                return list;
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
    }
}
