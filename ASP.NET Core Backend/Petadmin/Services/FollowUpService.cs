using System;
using System.Collections.Generic;
using System.Linq;
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
    public class FollowUpService : IFollowUpService
    {
        #region Class Initialization
        private readonly ILogger<FollowUpService> _logger;
        private readonly IFollowUpBroker _followUpBroker;
        public FollowUpService(IFollowUpBroker followUpBroker, ILogger<FollowUpService> logger)
        {
            _followUpBroker = followUpBroker;
            _logger = logger;
        }
        #endregion

        #region GET
        public Task<FollowUpPage> GetFollowUpPageByAnimalIdAsync(int animalId)
        {
            try
            {
                Task<Animal> taskAnimal = _followUpBroker.GetAnimalByIdAsync(animalId);
                Task<Owner> taskOwner = _followUpBroker.GetOwnerByAnimalIdAsync(animalId);
                Task<IEnumerable<FollowUp>> taskFollowUpsList = _followUpBroker.GetFollowUpsListByAnimalIdAsync(animalId);

                return Task<FollowUpPage>.Factory.ContinueWhenAll(
                    new Task[]
                    {
                        taskAnimal, taskOwner, taskFollowUpsList
                    },
                    tasks => new FollowUpPage
                    {
                        Animal = taskAnimal.Result,
                        Owner = taskOwner.Result,
                        FollowUpsList = taskFollowUpsList.Result.ToList(),

                    });
            }
            catch (Exception e)
            {
                _logger.LogError(e, e.Message);
                throw new GetFailedException();
            }
        }

        public Task<IEnumerable<FollowUp>> GetFollowUpsListByAnimalIdAsync(int animalId)
        {
            try
            {
                return _followUpBroker.GetFollowUpsListByAnimalIdAsync(animalId);
            }
            catch (Exception e)
            {
                _logger.LogError(e, e.Message);
                throw new GetFailedException();
            }
        }

        public Task<IEnumerable<FollowUpAllItem>> GetFollowUpAllList(DateTime from)
        {
            return _followUpBroker.GetFollowUpAllList(from);
        }
        #endregion

        #region ADD
        public Task<int> AddFollowUpAsync(FollowUp followUp)
        {
            try
            {
                return _followUpBroker.AddFollowUpAsync(followUp);
            }
            catch (Exception e)
            {
                _logger.LogError(e, e.Message);
                throw new AddFailedException();
            }
        }
        #endregion
        
        #region UPDATE
        public Task<bool> UpdateFollowUpAsync(FollowUp followUp)
        {
            try
            {
                return _followUpBroker.UpdateFollowUpAsync(followUp);
            }
            catch (Exception e)
            {
                _logger.LogError(e, e.Message);
                throw new UpdateFailedException();
            }
        }
        #endregion

        #region DELETE
        public Task<bool> DeleteFollowUpAsync(FollowUp followUp)
        {
            try
            {
                return _followUpBroker.DeleteFollowUpAsync(followUp);
            }
            catch (Exception e)
            {
                _logger.LogError(e, e.Message);
                throw new DeleteFailedException();
            }
        }
        #endregion
    }
}
