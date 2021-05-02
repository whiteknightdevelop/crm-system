using System.Collections.Generic;
using System.Threading.Tasks;
using Petadmin.Core.Models;
using PetAdmin.Core.Models;
using Petadmin.Repository.DbContext;
using Petadmin.Repository.Repositories.Interfaces;

namespace Petadmin.Repository.Repositories
{
    /// <summary>
    /// Contains methods for interacting with the animal owner backend using 
    /// MySQL via Stored Procedures
    /// </summary>
    public class AnimalRepository : Repository<Animal>, IAnimalRepository
    {
        #region Class Initialization
        private readonly IPetadminDbContext _context;
        public AnimalRepository(IPetadminDbContext context) : base(context)
        {
            _context = context;
        }
        #endregion

        public Task<Owner> GetAnimalOwnerByIdAsync(int animalId)
        {
            return _context.GetAnimalOwnerByIdAsync(animalId);
        }

        public Task<IEnumerable<Visit>> GetVisitsListByAnimalIdAsync(int animalId)
        {
            return _context.GetVisitsListByAnimalIdAsync(animalId);
        }

        public Task<IEnumerable<string>> GetAnimalsTypesListAsync()
        {
            return _context.GetAnimalsTypesListAsync();
        }

        public Task<IEnumerable<string>> GetAnimalsGendersListAsync()
        {
            return _context.GetAnimalsGendersListAsync();
        }

        public Task<IEnumerable<Breed>> GetAnimalsBreedsListAsync()
        {
            return _context.GetAnimalsBreedsListAsync();
        }

        public Task<IEnumerable<string>> GetAnimalsColorsListAsync()
        {
            return _context.GetAnimalsColorsListAsync();
        }

        public Task<IEnumerable<string>> GetAnimalsRemindersListAsync()
        {
            return _context.GetAnimalsRemindersListAsync();
        }

        public Task<IEnumerable<PreventiveReminder>> GetPreventiveRemindersListAsync(int animalId)
        {
            return _context.GetPreventiveRemindersListAsync(animalId);
        }

        public Task<Animal> GetAnimalByVisitIdAsync(int visitId)
        {
            return _context.GetAnimalByVisitIdAsync(visitId);
        }

        public Task<IEnumerable<AnimalSearch>> FindAnimalAsync(Animal animal)
        {
            return _context.FindAnimalAsync(animal);
        }
    }
}
