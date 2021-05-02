using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Petadmin.Core.Models;
using Petadmin.Identity;
using Petadmin.Models;
using Petadmin.Services.Interfaces;

namespace Petadmin.Controllers
{
    [Authorize (Roles = UserRoles.Admin)]
    [Route("api/[controller]")]
    [ApiController]
    public class FollowUpController : ControllerBase
    {
        private readonly IFollowUpService _followUpService;
        public FollowUpController(IFollowUpService followUpService)
        {
            _followUpService = followUpService;
        }

        // GET api/followup/{animalId}
        [HttpGet("{animalId}")]
        public async Task<ActionResult<FollowUpPage>> GetFollowUpPageByAnimalIdAsync(int animalId)
        {
            try
            {
                FollowUpPage data = await _followUpService.GetFollowUpPageByAnimalIdAsync(animalId);
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

        // POST api/followup
        [HttpPost]
        public async Task<ActionResult<int>> AddFollowUpAsync([FromBody] FollowUp followUp)
        {
            try
            {
                int storageFollowUpId = await _followUpService.AddFollowUpAsync(followUp);
                return Created(Url.RouteUrl(storageFollowUpId), storageFollowUpId);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        // POST api/followup/followup-all
        [HttpPost]
        [Route("followup-all")]
        public async Task<ActionResult<List<FollowUpAllItem>>> GetFollowUpAll([FromBody] DateTime from)
        {
            try
            {
                List<FollowUpAllItem> list = (await _followUpService.GetFollowUpAllList(from)).ToList();
                return list;
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        // PUT api/followup
        [HttpPut]
        public async Task<ActionResult<bool>> UpdateFollowUpAsync([FromBody] FollowUp followUp)
        {
            try
            {
                bool ans = await _followUpService.UpdateFollowUpAsync(followUp);
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

        // DELETE api/followup
        [HttpDelete]
        public async Task<ActionResult<bool>> DeleteFollowUpAsync([FromBody] FollowUp followUp)
        {
            try
            {
                bool ans = await _followUpService.DeleteFollowUpAsync(followUp);
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
