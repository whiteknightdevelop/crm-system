using Microsoft.AspNetCore.Mvc;
using System;
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
    public class VisitController : ControllerBase
    {
        private readonly IVisitService _visitService;
        public VisitController(IVisitService visitService)
        {
            _visitService = visitService;
        }
        
        // GET api/visit
        [HttpGet]
        public async Task<ActionResult<VisitPageLists>> GetAsync()
        {
            try
            {
                VisitPageLists data = await _visitService.GetVisitPageLists();
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

        // GET api/visit/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<VisitPage>> GetByIdAsync(int id)
        {
            try
            {
                VisitPage data = await _visitService.GetVisitPageByIdAsync(id);
                if (data.Visit != null)
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

        // POST api/visit
        [HttpPost]
        public async Task<ActionResult<int>> AddVisitAsync([FromBody] Visit visit)
        {
            try
            {
                int storageVisitId = await _visitService.AddVisitAsync(visit);
                return Created(Url.RouteUrl(storageVisitId), storageVisitId);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        // PUT api/visit
        [HttpPut]
        public async Task<ActionResult<bool>> UpdateVisitAsync([FromBody] Visit visit)
        {
            try
            {
                bool ans = await _visitService.UpdateVisitAsync(visit);
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

        // DELETE api/visit
        [HttpDelete]
        public async Task<ActionResult<bool>> DeleteVisitAsync([FromBody] Visit visit)
        {
            try
            {
                bool ans = await _visitService.DeleteVisitAsync(visit);
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
