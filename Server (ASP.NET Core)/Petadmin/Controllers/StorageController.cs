using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
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
    public class StorageController : ControllerBase
    {
        private readonly IStorageService _storageService;

        public StorageController(IStorageService storageService)
        {
            _storageService = storageService;
        }

        // POST api/storage/upload/visit
        [HttpPost]
        [Route("upload/visit")]
        [DisableFormValueModelBinding]
        public async Task<ActionResult<List<PetadminStorageFile>>> UploadVisitFile(int visitId)
        {
            try
            {
                List<PetadminStorageFile> files = (await _storageService.UploadVisitFile(Request, visitId)).ToList();
                return files;
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new Response { Status = "Error" , Message= "Failed to upload file." });
            }
        }

        // DELETE api/storage/delete/visit
        [HttpDelete]
        [Route("delete/visit")]
        public ActionResult<List<PetadminStorageFile>> DeleteVisitFile([FromBody] PetadminStorageFile file)
        {
            try
            {
                List<PetadminStorageFile> files = _storageService.DeleteVisitFile(file).ToList();
                return files;
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
    }
}
