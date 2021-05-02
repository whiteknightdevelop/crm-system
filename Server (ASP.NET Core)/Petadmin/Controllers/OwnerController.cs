using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Petadmin.Core.Models;
using Petadmin.Identity;
using Petadmin.Models;
using Petadmin.Services.Interfaces;

namespace Petadmin.Controllers
{
    [Authorize (Roles = UserRoles.Admin)]
    [Route("api/[controller]")]
    [ApiController]
    public class OwnerController : ControllerBase
    {
        private readonly IOwnerService _ownerService;
        public OwnerController(IOwnerService ownerService)
        {
            _ownerService = ownerService;
        }

        // GET api/owner/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<OwnerPage>> GetByIdAsync(int id)
        {
            try
            {
                OwnerPage data = await _ownerService.GetOwnerPageByIdAsync(id);
                if (data.Owner != null)
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

        // POST api/owner
        [HttpPost]
        public async Task<ActionResult<int>> AddOwnerAsync([FromBody] Owner owner)
        {
            try
            {
                int storageOwnerId = await _ownerService.AddOwnerAsync(owner);
                return Created(Url.RouteUrl(storageOwnerId), storageOwnerId);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        // PUT api/owner
        [HttpPut]
        public async Task<ActionResult<bool>> UpdateOwnerAsync([FromBody] Owner owner)
        {
            try
            {
                bool ans = await _ownerService.UpdateOwnerAsync(owner);
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

        // DELETE api/owner
        [HttpDelete]
        public async Task<ActionResult<bool>> DeleteOwnerAsync([FromBody] Owner owner)
        {
            try
            {
                bool ans = await _ownerService.DeleteOwnerAsync(owner);
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
