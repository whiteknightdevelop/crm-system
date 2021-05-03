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
    public class DebtsListController : ControllerBase
    {
        private readonly IDebtService _debtService;
        public DebtsListController(IDebtService debtService)
        {
            _debtService = debtService;
        }

        // GET api/DebtsList/{ownerId}
        [HttpGet("{ownerId}")]
        public async Task<ActionResult<List<Debt>>> GetDebtsListByOwnerIdAsync(int ownerId)
        {
            try
            {
                List<Debt> list = (await _debtService.GetDebtsListByOwnerIdAsync(ownerId)).ToList();
                return Ok(list);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
    }
}
