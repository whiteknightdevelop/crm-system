using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Petadmin.Core.Models;
using PetAdmin.Core.Models;
using Petadmin.Identity;
using Petadmin.Services.Interfaces;

namespace Petadmin.Controllers
{
    [Authorize (Roles = UserRoles.Admin)]
    [Route("api/[controller]")]
    [ApiController]
    public class SearchAnimalController : ControllerBase
    {
        private readonly IAnimalService _animalService;
        public SearchAnimalController(IAnimalService animalService)
        {
            _animalService = animalService;
        }

        // POST api/SearchAnimal/
        [HttpPost]
        public async Task<ActionResult<List<AnimalSearch>>> FindAnimalAsync([FromBody] Animal animal)
        {
            try
            {
                List<AnimalSearch> list = (await _animalService.FindAnimalAsync(animal)).ToList();
                return Ok(list);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
    }
}
