using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Petadmin.Core.Models;

namespace Petadmin.Repository.Repositories.Interfaces
{
    public interface IFollowUpRepository : IRepository<FollowUp>
    {
        Task<IEnumerable<FollowUp>> GetFollowUpsListByAnimalIdAsync(int animalId);
        Task<IEnumerable<FollowUpAllItem>> GetFollowUpAllList(DateTime from);
    }
}
