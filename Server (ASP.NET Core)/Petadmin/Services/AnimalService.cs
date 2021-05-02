using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Petadmin.Brokers.Interfaces;
using Petadmin.Core.Exceptions;
using Petadmin.Core.Models;
using PetAdmin.Core.Models;
using Petadmin.Models;
using Petadmin.Services.Interfaces;

namespace Petadmin.Services
{
    public class AnimalService : IAnimalService
    {
        #region Class Initialization
        private readonly ILogger<AnimalService> _logger;
        private readonly IAnimalBroker _animalBroker;
        private readonly IVisitBroker _visitBroker;

        public AnimalService(IAnimalBroker animalBroker, IVisitBroker visitBroker, ILogger<AnimalService> logger)
        {
            _animalBroker = animalBroker;
            _visitBroker = visitBroker;
            _logger = logger;
        }
        #endregion

        #region GET
        public Task<AnimalPageLists> GetAnimalPageLists()
        {
            try
            {
                Task<IEnumerable<string>> taskAnimalsTypesList = _animalBroker.GetAnimalsTypesListAsync();
                Task<IEnumerable<string>> taskAnimalsGendersList = _animalBroker.GetAnimalsGendersListAsync();
                Task<IEnumerable<Breed>> taskAnimalsBreedsList = _animalBroker.GetAnimalsBreedsListAsync();
                Task<IEnumerable<string>> taskAnimalsColorsList = _animalBroker.GetAnimalsColorsListAsync();

                return Task<AnimalPageLists>.Factory.ContinueWhenAll(
                    new Task[]{taskAnimalsTypesList, taskAnimalsGendersList,
                        taskAnimalsBreedsList, taskAnimalsColorsList},
                    tasks => new AnimalPageLists
                    {
                        TypesList = taskAnimalsTypesList.Result.ToList(),
                        GendersList = taskAnimalsGendersList.Result.ToList(),
                        BreedsList = taskAnimalsBreedsList.Result.ToList(),
                        ColorsList = taskAnimalsColorsList.Result.ToList(),
                    });
            }
            catch (Exception e)
            {
                _logger.LogError(e, e.Message);
                throw new GetFailedException();
            }
        }

        public Task<AnimalPage> GetAnimalPageByIdAsync(int animalId)
        {
            try
            {
                Task<Animal> taskAnimal = _animalBroker.GetAnimalByIdAsync(animalId);
                Task<Owner> taskAnimalOwner = _animalBroker.GetAnimalOwnerByIdAsync(animalId);
                Task<IEnumerable<Visit>> taskVisitsList= _animalBroker.GetVisitsListByAnimalIdAsync(animalId);
                Task<IEnumerable<string>> taskAnimalsTypesList = _animalBroker.GetAnimalsTypesListAsync();
                Task<IEnumerable<string>> taskAnimalsGendersList = _animalBroker.GetAnimalsGendersListAsync();
                Task<IEnumerable<Breed>> taskAnimalsBreedsList = _animalBroker.GetAnimalsBreedsListAsync();
                Task<IEnumerable<string>> taskAnimalsColorsList = _animalBroker.GetAnimalsColorsListAsync();
                Task<IEnumerable<string>> taskAnimalsRemindersList = _animalBroker.GetAnimalsRemindersListAsync();
                Task<IEnumerable<PreventiveReminder>> taskAnimalsPreventiveReminderList = _animalBroker.GetPreventiveRemindersListAsync(animalId);

                return Task<AnimalPage>.Factory.ContinueWhenAll(
                    new Task[]{taskAnimal, taskAnimalOwner, taskVisitsList,
                        taskAnimalsTypesList, taskAnimalsGendersList,
                        taskAnimalsBreedsList, taskAnimalsColorsList, taskAnimalsRemindersList, taskAnimalsPreventiveReminderList},
                    tasks => new AnimalPage
                    {
                        Animal = taskAnimal.Result,
                        AnimalOwner = taskAnimalOwner.Result,
                        VisitsList = taskVisitsList.Result.ToList(),
                        Lists = new AnimalPageLists
                        {
                            TypesList = taskAnimalsTypesList.Result.ToList(),
                            GendersList = taskAnimalsGendersList.Result.ToList(),
                            BreedsList = taskAnimalsBreedsList.Result.ToList(),
                            ColorsList = taskAnimalsColorsList.Result.ToList(),
                        },
                        preventiveRemindersList = taskAnimalsPreventiveReminderList.Result?.ToList(),
                        RemindersList = taskAnimalsRemindersList.Result.ToList(),
                    });
            }
            catch (Exception e)
            {
                _logger.LogError(e, e.Message);
                throw new GetFailedException();
            }
        }

        public async Task<AnimalPrintPage> GetAnimalPrintPageByIdAsync(int animalId)
        {
            try
            {
                Task<Animal> taskAnimal = _animalBroker.GetAnimalByIdAsync(animalId);
                Task<Owner> taskAnimalOwner = _animalBroker.GetAnimalOwnerByIdAsync(animalId);
                List<Visit> visitsList = (await _animalBroker.GetVisitsListByAnimalIdAsync(animalId)).ToList();
                foreach (var visit in visitsList)
                {
                    visit.PreventiveTreatmentsList = (await _visitBroker.GetPreventiveTreatmentsListByVisitIdAsync(visit.VisitId)).ToList();
                }

                return await Task<AnimalPrintPage>.Factory.ContinueWhenAll(
                    new Task[]{taskAnimal, taskAnimalOwner,},
                    tasks => new AnimalPrintPage
                    {
                        Animal = taskAnimal.Result,
                        Owner = taskAnimalOwner.Result,
                        VisitsList = visitsList
                    });
            }
            catch (Exception e)
            {
                _logger.LogError(e, e.Message);
                throw new GetFailedException();
            }
        }

        public Task<IEnumerable<PreventiveReminder>> GetPreventiveRemindersListAsync(int animalId)
        {
            return _animalBroker.GetPreventiveRemindersListAsync(animalId);
        }

        #endregion

        #region ADD
        public Task<int> AddAnimalAsync(Animal animal)
        {
            try
            {
                return _animalBroker.AddAnimalAsync(animal);
            }
            catch (Exception e)
            {
                _logger.LogError(e, e.Message);
                throw new AddFailedException();
            }
        }

        public Task<int> AddPreventiveReminderAsync(PreventiveReminder preventiveReminder)
        {
            try
            {
                return _animalBroker.AddPreventiveReminderAsync(preventiveReminder);
            }
            catch (Exception e)
            {
                _logger.LogError(e, e.Message);
                throw new AddFailedException();
            }
        }
        #endregion

        #region UPDATE
        public Task<bool> UpdateAnimalAsync(Animal animal)
        {
            try
            {
                return _animalBroker.UpdateAnimalAsync(animal);
            }
            catch (Exception e)
            {
                _logger.LogError(e, e.Message);
                throw new UpdateFailedException();
            }
        }
        #endregion

        #region DELETE
        public Task<bool> DeleteAnimalAsync(Animal animal)
        {
            try
            {
                return _animalBroker.DeleteAnimalAsync(animal);
            }
            catch (Exception e)
            {
                _logger.LogError(e, e.Message);
                throw new DeleteFailedException();
            }
        }

        public async Task<bool> DeleteSelectedRemindersAsync(List<PreventiveReminder> list)
        {
            try
            {
                var cts = new CancellationTokenSource();
                CancellationToken token = cts.Token;
                var tasks = new List<Task<bool>>();

                foreach (var item in list)
                {
                    tasks.Add(_animalBroker.DeleteReminderAsync(item, cts, token));
                }

                return await Task<bool>.Factory.ContinueWhenAll(
                    tasks.ToArray(), (results) =>
                    {
                        cts.Dispose();
                        return true;
                    }, token);
            }
            catch (Exception e)
            {
                _logger.LogError(e, e.Message);
                throw;
            }
        }
        #endregion

        #region SEARCH
        public Task<IEnumerable<AnimalSearch>> FindAnimalAsync(Animal animal)
        {
            try
            {
                return _animalBroker.FindAnimalAsync(animal);
            }
            catch (Exception e)
            {
                _logger.LogError(e, e.Message);
                throw new SearchFailedException();
            }
        }
        #endregion
    }
}
