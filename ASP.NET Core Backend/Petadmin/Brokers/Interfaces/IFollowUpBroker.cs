using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Petadmin.Core.Models;
using PetAdmin.Core.Models;

namespace Petadmin.Brokers.Interfaces
{
    public interface IFollowUpBroker
    {
        Task<Animal> GetAnimalByIdAsync(int animalId);
        Task<Owner> GetOwnerByAnimalIdAsync(int animalId);
        Task<IEnumerable<FollowUp>> GetFollowUpsListByAnimalIdAsync(int animalId);
        Task<IEnumerable<FollowUpAllItem>> GetFollowUpAllList(DateTime from);
        Task<int> AddFollowUpAsync(FollowUp followUp);
        Task<bool> UpdateFollowUpAsync(FollowUp followUp);
        Task<bool> DeleteFollowUpAsync(FollowUp followUp);
    }
}
