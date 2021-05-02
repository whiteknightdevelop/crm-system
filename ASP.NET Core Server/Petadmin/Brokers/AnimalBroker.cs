using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Petadmin.Brokers.Interfaces;
using Petadmin.Core.Models;
using PetAdmin.Core.Models;
using Petadmin.Repository.Interfaces;

namespace Petadmin.Brokers
{
    public class AnimalBroker : IAnimalBroker
    {
        private readonly IUnitOfWork _unitOfWork;
        public AnimalBroker(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public Animal GetAnimalById(int animalId)
        {
            return _unitOfWork.Animals.Get(animalId);
        }

        public Task<Animal> GetAnimalByIdAsync(int animalId)
        {
            return _unitOfWork.Animals.GetAsync(animalId);
        }

        public Task<Owner> GetAnimalOwnerByIdAsync(int animalId)
        {
            return _unitOfWork.Animals.GetAnimalOwnerByIdAsync(animalId);
        }

        public Task<IEnumerable<Visit>> GetVisitsListByAnimalIdAsync(int animalId)
        {
            return _unitOfWork.Animals.GetVisitsListByAnimalIdAsync(animalId);
        }

        public Task<IEnumerable<string>> GetAnimalsTypesListAsync()
        {
            return _unitOfWork.Animals.GetAnimalsTypesListAsync();
        }

        public Task<IEnumerable<string>> GetAnimalsGendersListAsync()
        {
            return _unitOfWork.Animals.GetAnimalsGendersListAsync();
        }

        public Task<IEnumerable<Breed>> GetAnimalsBreedsListAsync()
        {
            return _unitOfWork.Animals.GetAnimalsBreedsListAsync();
        }

        public Task<IEnumerable<string>> GetAnimalsColorsListAsync()
        {
            return _unitOfWork.Animals.GetAnimalsColorsListAsync();
        }

        public Task<IEnumerable<string>> GetAnimalsRemindersListAsync()
        {
            return _unitOfWork.Animals.GetAnimalsRemindersListAsync();
        }

        public Task<IEnumerable<PreventiveReminder>> GetPreventiveRemindersListAsync(int animalId)
        {
            return _unitOfWork.Animals.GetPreventiveRemindersListAsync(animalId);
        }

        public Task<int> AddAnimalAsync(Animal animal)
        {
            return _unitOfWork.Animals.AddAsync(animal);
        }

        public Task<int> AddPreventiveReminderAsync(PreventiveReminder preventiveReminder)
        {
            return _unitOfWork.PreventiveReminders.AddAsync(preventiveReminder);
        }

        public Task<bool> UpdateAnimalAsync(Animal animal)
        {
            return _unitOfWork.Animals.UpdateAsync(animal);
        }

        public Task<bool> DeleteAnimalAsync(Animal animal)
        {
            return _unitOfWork.Animals.RemoveAsync(animal);
        }

        public Task<bool> DeleteReminderAsync(PreventiveReminder reminder)
        {
            return _unitOfWork.PreventiveReminders.RemoveAsync(reminder);
        }

        public Task<bool> DeleteReminderAsync(PreventiveReminder reminder, CancellationToken token)
        {
            return _unitOfWork.PreventiveReminders.RemoveAsync(reminder, token);
        }

        public Task<bool> DeleteReminderAsync(PreventiveReminder reminder, CancellationTokenSource cts, CancellationToken token)
        {
            return _unitOfWork.PreventiveReminders.RemoveAsync(reminder, cts, token);
        }

        public Task<IEnumerable<AnimalSearch>> FindAnimalAsync(Animal animal)
        {
            return _unitOfWork.Animals.FindAnimalAsync(animal);
        }
    }
}
