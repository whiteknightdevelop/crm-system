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
    public class PreventiveTreatmentController : ControllerBase
    {
        private readonly IVisitService _visitService;
        public PreventiveTreatmentController(IVisitService visitService)
        {
            _visitService = visitService;
        }

        // GET api/preventivetreatment/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<PreventiveTreatment>> GetPreventiveTreatmentsListAsync(int id)
        {
            try
            {
                IEnumerable<PreventiveTreatment> data = await _visitService.GetPreventiveTreatmentsListByVisitIdAsync(id);
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

        // POST api/preventivetreatment
        [HttpPost]
        public async Task<ActionResult<int>> AddPreventiveTreatmentAsync([FromBody] List<PreventiveTreatment> list)
        {
            try
            {
                int storageTreatmentId = await _visitService.AddPreventiveTreatmentListAsync(list);
                return Created(Url.RouteUrl(storageTreatmentId), storageTreatmentId);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        // DELETE api/preventivetreatment
        [HttpDelete]
        public async Task<ActionResult<bool>> DeletePreventiveTreatmentsAsync([FromBody] List<PreventiveTreatment> list)
        {
            try
            {
                bool ans = await _visitService.DeleteSelectedPreventiveTreatmentsAsync(list);
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
