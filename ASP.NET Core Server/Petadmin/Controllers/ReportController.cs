using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Logging;
using Petadmin.Core.Models;
using Petadmin.Identity;
using Petadmin.Models;
using Petadmin.Services.Interfaces;

namespace Petadmin.Controllers
{
    [Authorize (Roles = UserRoles.Admin)]
    [Route("api/[controller]")]
    [ApiController]
    public class ReportController : ControllerBase
    {
        private readonly IReportService _reportService;
        private readonly IAnimalService _animalService;

        public ReportController(IReportService reportService, IAnimalService animalService)
        {
            _reportService = reportService;
            _animalService = animalService;
        }

        // GET api/report/debts-sheet
        [HttpGet]
        [Route("debts-sheet")]
        public async Task<ActionResult<List<DebtSheetItem>>> GetDebtSheet()
        {
            try
            {
                List<DebtSheetItem> list = (await _reportService.GetDebtSheet()).ToList();
                return list;
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        // POST api/report/visited-last-days
        [HttpPost]
        [Route("visited-last-days")]
        public async Task<ActionResult<List<VisitedOwnersItem>>> GetOwnersVisitedLastXDays([FromBody] int days)
        {
            try
            {
                List<VisitedOwnersItem> list = (await _reportService.GetOwnersVisitedLastXDays(days)).ToList();
                return list;
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        // POST api/report/not-visited-last-days
        [HttpPost]
        [Route("not-visited-last-days")]
        public async Task<ActionResult<List<VisitedOwnersItem>>> GetOwnersNotVisitedLastXDays([FromBody] int days)
        {
            try
            {
                List<VisitedOwnersItem> list = (await _reportService.GetOwnersNotVisitedLastXDays(days)).ToList();
                return list;
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        // POST api/report/rabies-list-by-date
        [HttpPost]
        [Route("rabies-list-by-date")]
        public async Task<ActionResult<List<RabiesReport>>> GetRabiesListByDateInterval([FromBody] DateIntervalRequest timeInterval)
        {
            try
            {
                List<RabiesReport> list = (await _reportService.GetRabiesListByDateInterval(timeInterval.From, timeInterval.To)).ToList();
                return list;
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        // GET api/report/animal-print
        [HttpGet]
        [Route("animal-print/{animalId}")]
        public async Task<ActionResult<AnimalPrintPage>> GetAnimalPrintAsync(int animalId)
        {
            try
            {
                AnimalPrintPage data = await _animalService.GetAnimalPrintPageByIdAsync(animalId);
                if (data.Animal != null)
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
    }
}
