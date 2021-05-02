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
    public class DebtController : ControllerBase
    {
        private readonly IDebtService _debtService;
        public DebtController(IDebtService debtService)
        {
            _debtService = debtService;
        }


        // GET api/debt/{ownerId}
        [HttpGet("{ownerId}")]
        public async Task<ActionResult<DebtPage>> GetDebtPageByOwnerIdAsync(int ownerId)
        {
            try
            {
                DebtPage data = await _debtService.GetDebtPageByOwnerIdAsync(ownerId);
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

        // POST api/debt
        [HttpPost]
        public async Task<ActionResult<int>> AddDebtAsync([FromBody] Debt debt)
        {
            try
            {
                int storageDebtId = await _debtService.AddDebtAsync(debt);
                return Created(Url.RouteUrl(storageDebtId), storageDebtId);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        // PUT api/debt
        [HttpPut]
        public async Task<ActionResult<bool>> UpdateDebtAsync([FromBody] Debt debt)
        {
            try
            {
                bool ans = await _debtService.UpdateDebtAsync(debt);
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


        // DELETE api/debt
        [HttpDelete]
        public async Task<ActionResult<bool>> DeleteDebtAsync([FromBody] Debt debt)
        {
            try
            {
                bool ans = await _debtService.DeleteDebtAsync(debt);
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
