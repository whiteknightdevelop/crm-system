using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Petadmin.Core.Models;
using Petadmin.Repository.DbContext;
using Petadmin.Repository.Repositories.Interfaces;

namespace Petadmin.Repository.Repositories
{
    public class FollowUpRepository : Repository<FollowUp>, IFollowUpRepository
    {
        #region Class Initialization
        private readonly IPetadminDbContext _context;
        public FollowUpRepository(IPetadminDbContext context) : base(context)
        {
            _context = context;
        }
        #endregion

        public Task<IEnumerable<FollowUp>> GetFollowUpsListByAnimalIdAsync(int animalId)
        {
            return _context.GetFollowUpsListByAnimalIdAsync(animalId);
        }

        public Task<IEnumerable<FollowUpAllItem>> GetFollowUpAllList(DateTime from)
        {
            return _context.GetFollowUpAllList(from);
        }
    }
}
