using System.Collections.Generic;
using System.Threading.Tasks;
using Petadmin.Core.Models;
using PetAdmin.Core.Models;
using Petadmin.Models;

namespace Petadmin.Services.Interfaces
{
    public interface IAnimalService
    {
        Task<AnimalPageLists> GetAnimalPageLists();
        Task<AnimalPage> GetAnimalPageByIdAsync(int animalId);
        Task<AnimalPrintPage> GetAnimalPrintPageByIdAsync(int animalId);
        Task<IEnumerable<PreventiveReminder>> GetPreventiveRemindersListAsync(int animalId);
        Task<int> AddAnimalAsync(Animal animal);
        Task<int> AddPreventiveReminderAsync(PreventiveReminder preventiveReminder);
        Task<bool> UpdateAnimalAsync(Animal animal);
        Task<bool> DeleteAnimalAsync(Animal animal);
        Task<bool> DeleteSelectedRemindersAsync(List<PreventiveReminder> list);
        Task<IEnumerable<AnimalSearch>> FindAnimalAsync(Animal animal);
    }
}
