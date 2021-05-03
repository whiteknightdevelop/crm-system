using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Petadmin.Core.Models;
using Petadmin.Identity;
using Petadmin.Models;
using Petadmin.Services.Interfaces;
using Petadmin.Utilities;

namespace Petadmin.Controllers
{
    [Authorize (Roles = UserRoles.Admin)]
    [Route("api/[controller]")]
    [ApiController]
    public class SystemController : ControllerBase
    {
        private readonly ISystemService _systemService;
        public SystemController(ISystemService systemService)
        {
            _systemService = systemService;
        }

        // GET api/system/backup
        [HttpGet]
        [Route("backup")]
        public async Task<IActionResult> Backup([FromQuery(Name = "connectionId")] string connectionId)
        {
            try
            {
                Backup backup = await _systemService.GetBackupFile(connectionId);
                if (backup != null)
                {
                    return File(backup.FileBytes, System.Net.Mime.MediaTypeNames.Application.Octet, backup.FileName);
                }
                return StatusCode(StatusCodes.Status500InternalServerError, new Response { Status = "Error" , Message= "Failed to create backup file." });
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        // GET api/system/restore
        [HttpPost]
        [Route("restore")]
        [DisableFormValueModelBinding]
        public async Task<IActionResult> Restore([FromQuery(Name = "connectionId")] string connectionId)
        {
            try
            {
                Progress<ProgressReport> progress = new Progress<ProgressReport>();
                progress.ProgressChanged += _systemService.ReportRestoreProgress;
                bool ans = await _systemService.Restore(Request, connectionId, progress);
                progress.ProgressChanged -= _systemService.ReportRestoreProgress;
                if (ans)
                {
                    return Created(nameof(SystemController), null);
                }
                return StatusCode(StatusCodes.Status500InternalServerError, new Response { Status = "Error" , Message = "Failed to create backup file." });
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
    }
}
