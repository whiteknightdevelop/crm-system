using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Petadmin.Core.Models;
using Petadmin.Identity;
using Petadmin.Services.Interfaces;

namespace Petadmin.Controllers
{
    [Authorize (Roles = UserRoles.Admin)]
    [Route("api/[controller]")]
    [ApiController]
    public class FollowUpListController : ControllerBase
    {
        private readonly IFollowUpService _followUpService;
        public FollowUpListController(IFollowUpService followUpService)
        {
            _followUpService = followUpService;
        }

        // GET api/followuplist/{animalId}
        [HttpGet("{animalId}")]
        public async Task<ActionResult<List<FollowUp>>> GetFollowUpsListByAnimalIdAsync(int animalId)
        {
            try
            {
                List<FollowUp> list = (await _followUpService.GetFollowUpsListByAnimalIdAsync(animalId)).ToList();
                return Ok(list);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
    }
}
