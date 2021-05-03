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
    public class OwnerService : IOwnerService
    {
        #region Class Initialization
        private readonly ILogger<OwnerService> _logger;
        private readonly IOwnerBroker _ownerBroker;
        public OwnerService(IOwnerBroker ownerBroker, ILogger<OwnerService> logger)
        {
            _ownerBroker = ownerBroker;
            _logger = logger;
        }
        #endregion

        #region GET
        public Owner GetOwnerById(int ownerId)
        {
            try
            {
                return _ownerBroker.GetOwnerById(ownerId);
            }
            catch (Exception e)
            {
                _logger.LogError(e, e.Message);
                throw new GetFailedException();
            }
        }
        
        public Task<Owner> GetOwnerByIdAsync(int ownerId)
        {
            try
            {
                return _ownerBroker.GetOwnerByIdAsync(ownerId);
            }
            catch (Exception e)
            {
                _logger.LogError(e, e.Message);
                throw new GetFailedException();
            }
        }

        public Task<OwnerPage> GetOwnerPageByIdAsync(int ownerId)
        {
            try
            {
                Task<Owner> taskOwner = _ownerBroker.GetOwnerByIdAsync(ownerId);
                Task<int> taskOwnerTotalDebtAmount = _ownerBroker.GetOwnerTotalDebtAmountAsync(ownerId);
                Task<IEnumerable<Animal>> taskAnimalList= _ownerBroker.GetAnimalsListByOwnerIdAsync(ownerId);

                return Task<OwnerPage>.Factory.ContinueWhenAll(
                    new Task[]{taskOwner, taskOwnerTotalDebtAmount, taskAnimalList},
                    tasks => new OwnerPage
                    {
                        Owner = taskOwner.Result,
                        OwnerTotalDebtAmount = taskOwnerTotalDebtAmount.Result,
                        AnimalsList = taskAnimalList.Result.ToList()
                    });
            }
            catch (Exception e)
            {
                _logger.LogError(e, e.Message);
                throw new GetFailedException();
            }
        }
        #endregion

        #region ADD
        public Task<int> AddOwnerAsync(Owner owner)
        {
            try
            {
                return _ownerBroker.AddOwnerAsync(owner);
            }
            catch (Exception e)
            {
                _logger.LogError(e, e.Message);
                throw new AddFailedException();
            }
        }
        #endregion

        #region UPDATE
        public Task<bool> UpdateOwnerAsync(Owner owner)
        {
            try
            {
                return _ownerBroker.UpdateOwnerAsync(owner);
            }
            catch (Exception e)
            {
                _logger.LogError(e, e.Message);
                throw new UpdateFailedException();
            }
        }
        #endregion

        #region DELETE
        public Task<bool> DeleteOwnerAsync(Owner owner)
        {
            try
            {
                return _ownerBroker.DeleteOwnerAsync(owner);
            }
            catch (Exception e)
            {
                _logger.LogError(e, e.Message);
                throw new DeleteFailedException();
            }
        }
        #endregion

        #region SEARCH
        public Task<IEnumerable<Owner>> FindOwnerAsync(Owner owner)
        {
            try
            {
                return _ownerBroker.FindOwnerAsync(owner);
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
