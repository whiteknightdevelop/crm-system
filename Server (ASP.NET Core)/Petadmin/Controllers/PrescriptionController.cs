using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Petadmin.Core.Models;
using Petadmin.Identity;
using Petadmin.Models;
using Petadmin.Services.Interfaces;

namespace Petadmin.Controllers
{
    [Authorize (Roles = UserRoles.Admin)]
    [Route("api/[controller]")]
    [ApiController]
    public class PrescriptionController : ControllerBase
    {
        private readonly IPrescriptionService _prescriptionService;

        public PrescriptionController(IPrescriptionService prescriptionService)
        {
            _prescriptionService = prescriptionService;
        }

        // GET api/prescription/{visitId}
        [HttpGet("{visitId}")]
        public async Task<ActionResult<PrescriptionPage>> GetPrescriptionPageByVisitIdAsync(int visitId)
        {
            try
            {
                PrescriptionPage data = await _prescriptionService.GetPrescriptionPageByVisitIdAsync(visitId);
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

        // POST api/prescription
        [HttpPost]
        public async Task<ActionResult<int>> AddPrescriptionAsync([FromBody] Prescription prescription)
        {
            try
            {
                int storagePrescriptiontId = await _prescriptionService.AddPrescriptionAsync(prescription);
                return Created(Url.RouteUrl(storagePrescriptiontId), storagePrescriptiontId);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        // DELETE api/prescription
        [HttpDelete]
        public async Task<ActionResult<bool>> DeletePrescriptionAsync([FromBody] Prescription prescription)
        {
            try
            {
                bool ans = await _prescriptionService.DeletePrescriptiontAsync(prescription);
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
