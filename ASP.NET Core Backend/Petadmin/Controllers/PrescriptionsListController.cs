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
    public class PrescriptionsListController : ControllerBase
    {
        private readonly IPrescriptionService _prescriptionService;
        public PrescriptionsListController(IPrescriptionService prescriptionService)
        {
            _prescriptionService = prescriptionService;
        }

        // GET api/PrescriptionsList/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<List<Prescription>>> GetPrescriptionsListByVisitIdAsync(int id)
        {
            try
            {
                List<Prescription> list = (await _prescriptionService.GetPrescriptionsListByVisitIdAsync(id)).ToList();
                return Ok(list);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
    }
}
