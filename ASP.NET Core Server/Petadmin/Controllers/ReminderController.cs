using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Petadmin.Core.Models;
using Petadmin.Identity;
using Petadmin.Services.Interfaces;

namespace Petadmin.Controllers
{
    [Authorize (Roles = UserRoles.Admin)]
    [Route("api/[controller]")]
    [ApiController]
    public class ReminderController : ControllerBase
    {
        private readonly IAnimalService _animalService;
        public ReminderController(IAnimalService animalService)
        {
            _animalService = animalService;
        }

        // GET api/reminder/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<PreventiveReminder>> GetPreventiveRemindersListAsync(int id)
        {
            try
            {
                IEnumerable<PreventiveReminder> data = await _animalService.GetPreventiveRemindersListAsync(id);
                if (data != null)
                {
                    return Ok(data);
                }
                return NotFound();
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        // POST api/reminder
        [HttpPost]
        public async Task<ActionResult<int>> AddPreventiveReminderAsync([FromBody] PreventiveReminder preventiveReminder)
        {
            try
            {
                int storagePreventiveReminderId = await _animalService.AddPreventiveReminderAsync(preventiveReminder);
                return Created(Url.RouteUrl(storagePreventiveReminderId), storagePreventiveReminderId);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        // DELETE api/reminder
        [HttpDelete]
        public async Task<ActionResult<bool>> DeletePreventiveReminderAsync([FromBody] List<PreventiveReminder> list)
        {
            try
            {
                bool ans = await _animalService.DeleteSelectedRemindersAsync(list);
                if (ans)
                {
                    return Ok(true);
                }
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
    }
}
