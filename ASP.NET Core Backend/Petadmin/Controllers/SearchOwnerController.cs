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
    public class SearchOwnerController : ControllerBase
    {
        private readonly IOwnerService _ownerService;
        public SearchOwnerController(IOwnerService ownerService)
        {
            _ownerService = ownerService;
        }

        // POST api/SearchOwner/
        [HttpPost]
        public async Task<ActionResult<List<Owner>>> FindOwnerAsync([FromBody] Owner owner)
        {
            try
            {
                List<Owner> list = (await _ownerService.FindOwnerAsync(owner)).ToList();
                return Ok(list);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
    }
}
