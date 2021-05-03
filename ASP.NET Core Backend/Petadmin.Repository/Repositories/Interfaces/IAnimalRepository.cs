using System.Collections.Generic;
using System.Threading.Tasks;
using Petadmin.Core.Models;
using PetAdmin.Core.Models;

namespace Petadmin.Repository.Repositories.Interfaces
{
    /// <summary>
    /// Defines methods for interacting with the animals backend.
    /// </summary>
    public interface IAnimalRepository : IRepository<Animal>
    {
        /// <summary>
        /// Gets animal owner by animal id.
        /// </summary>
        Task<Owner> GetAnimalOwnerByIdAsync(int animalId);

        /// <summary>
        /// Gets animal visits list by animal id.
        /// </summary>
        Task<IEnumerable<Visit>> GetVisitsListByAnimalIdAsync(int animalId);

        /// <summary>
        /// Gets animals types list.
        /// </summary>
        Task<IEnumerable<string>> GetAnimalsTypesListAsync();

        /// <summary>
        /// Gets animals gender list.
        /// </summary>
        Task<IEnumerable<string>> GetAnimalsGendersListAsync();

        /// <summary>
        /// Gets animals breed list.
        /// </summary>
        Task<IEnumerable<Breed>> GetAnimalsBreedsListAsync();

        /// <summary>
        /// Gets animals color list.
        /// </summary>
        Task<IEnumerable<string>> GetAnimalsColorsListAsync();

        /// <summary>
        /// Gets reminders list.
        /// </summary>
        Task<IEnumerable<string>> GetAnimalsRemindersListAsync();

        /// <summary>
        /// Gets animals preventive reminder list.
        /// </summary>
        Task<IEnumerable<PreventiveReminder>> GetPreventiveRemindersListAsync(int animalId);

        /// <summary>
        /// Gets animal by visit id.
        /// </summary>
        Task<Animal> GetAnimalByVisitIdAsync(int visitId);

        /// <summary>
        /// Search animal by parameter
        /// </summary>
        Task<IEnumerable<AnimalSearch>> FindAnimalAsync(Animal animal);
    }
}
