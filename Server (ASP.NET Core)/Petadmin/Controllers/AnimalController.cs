using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using PetAdmin.Core.Models;
using Petadmin.Identity;
using Petadmin.Models;
using Petadmin.Services.Interfaces;

namespace Petadmin.Controllers
{
    [Authorize (Roles = UserRoles.Admin)]
    [Route("api/[controller]")]
    [ApiController]
    public class AnimalController : ControllerBase
    {
        private readonly IAnimalService _animalService;
        public AnimalController(IAnimalService animalService)
        {
            _animalService = animalService;
        }
        
        // GET api/animal
        [HttpGet]
        public async Task<ActionResult<AnimalPageLists>> GetAsync()
        {
            try
            {
                AnimalPageLists data = await _animalService.GetAnimalPageLists();
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

        // GET api/animal/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<AnimalPage>> GetByIdAsync(int id)
        {
            try
            {
                AnimalPage data = await _animalService.GetAnimalPageByIdAsync(id);

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

        // POST api/animal
        [HttpPost]
        public async Task<ActionResult<int>> AddAnimalAsync([FromBody] Animal animal)
        {
            try
            {
                int storageAnimalId = await _animalService.AddAnimalAsync(animal);
                return Created(Url.RouteUrl(storageAnimalId), storageAnimalId);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        // PUT api/animal/{id}
        [HttpPut("{id}")]
        public async Task<ActionResult<bool>> UpdateAnimalAsync(int id, [FromBody] Animal animal)
        {
            try
            {
                bool ans = await _animalService.UpdateAnimalAsync(animal);
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

        // DELETE api/animal/{id}
        [HttpDelete("{id}")]
        public async Task<ActionResult<bool>> DeleteAnimalAsync(int id, [FromBody] Animal animal)
        {
            try
            {
                bool ans = await _animalService.DeleteAnimalAsync(animal);
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
