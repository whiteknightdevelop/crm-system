using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Petadmin.Brokers.Interfaces;
using Petadmin.Core.Models;
using PetAdmin.Core.Models;
using Petadmin.Repository.Interfaces;

namespace Petadmin.Brokers
{
    public class FollowUpBroker : IFollowUpBroker
    {
        private readonly IUnitOfWork _unitOfWork;
        public FollowUpBroker(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public Task<Animal> GetAnimalByIdAsync(int animalId)
        {
            return _unitOfWork.Animals.GetAsync(animalId);
        }

        public Task<Owner> GetOwnerByAnimalIdAsync(int animalId)
        {
            return _unitOfWork.Animals.GetAnimalOwnerByIdAsync(animalId);
        }

        public Task<IEnumerable<FollowUp>> GetFollowUpsListByAnimalIdAsync(int animalId)
        {
            return _unitOfWork.FollowUps.GetFollowUpsListByAnimalIdAsync(animalId);
        }

        public Task<IEnumerable<FollowUpAllItem>> GetFollowUpAllList(DateTime from)
        {
            return _unitOfWork.FollowUps.GetFollowUpAllList(from);
        }

        public Task<int> AddFollowUpAsync(FollowUp followUp)
        {
            return _unitOfWork.FollowUps.AddAsync(followUp);
        }

        public Task<bool> UpdateFollowUpAsync(FollowUp followUp)
        {
            return _unitOfWork.FollowUps.UpdateAsync(followUp);
        }

        public Task<bool> DeleteFollowUpAsync(FollowUp followUp)
        {
            return _unitOfWork.FollowUps.RemoveAsync(followUp);
        }
    }
}
