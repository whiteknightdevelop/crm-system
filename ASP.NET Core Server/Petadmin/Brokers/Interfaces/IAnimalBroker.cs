using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Petadmin.Core.Models;
using PetAdmin.Core.Models;

namespace Petadmin.Brokers.Interfaces
{
    public interface IAnimalBroker
    {
        Animal GetAnimalById(int animalId);
        Task<Animal> GetAnimalByIdAsync(int animalId);
        Task<Owner> GetAnimalOwnerByIdAsync(int animalId);
        Task<IEnumerable<Visit>> GetVisitsListByAnimalIdAsync(int animalId);
        Task<IEnumerable<string>> GetAnimalsTypesListAsync();
        Task<IEnumerable<string>> GetAnimalsGendersListAsync();
        Task<IEnumerable<Breed>> GetAnimalsBreedsListAsync();
        Task<IEnumerable<string>> GetAnimalsColorsListAsync();
        Task<IEnumerable<string>> GetAnimalsRemindersListAsync();
        Task<IEnumerable<PreventiveReminder>> GetPreventiveRemindersListAsync(int animalId);
        Task<int> AddAnimalAsync(Animal animal);
        Task<int> AddPreventiveReminderAsync(PreventiveReminder preventiveReminder);
        Task<bool> UpdateAnimalAsync(Animal animal);
        Task<bool> DeleteAnimalAsync(Animal animal);
        Task<bool> DeleteReminderAsync(PreventiveReminder reminder);
        Task<bool> DeleteReminderAsync(PreventiveReminder reminder, CancellationToken token);
        Task<bool> DeleteReminderAsync(PreventiveReminder reminder, CancellationTokenSource cts, CancellationToken token);
        Task<IEnumerable<AnimalSearch>> FindAnimalAsync(Animal animal);
    }
}
