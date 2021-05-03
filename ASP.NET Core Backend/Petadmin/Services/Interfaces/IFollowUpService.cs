using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Petadmin.Core.Models;
using Petadmin.Models;

namespace Petadmin.Services.Interfaces
{
    public interface IFollowUpService
    {
        Task<FollowUpPage> GetFollowUpPageByAnimalIdAsync(int animalId);
        Task<IEnumerable<FollowUp>> GetFollowUpsListByAnimalIdAsync(int animalId);
        Task<IEnumerable<FollowUpAllItem>> GetFollowUpAllList(DateTime from);
        Task<int> AddFollowUpAsync(FollowUp followUp);
        Task<bool> UpdateFollowUpAsync(FollowUp followUp);
        Task<bool> DeleteFollowUpAsync(FollowUp followUp);
    }
}
