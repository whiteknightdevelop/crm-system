using System.Collections.Generic;
using System.Threading.Tasks;
using Petadmin.Core.Models;
using Petadmin.Repository.DbContext;
using Petadmin.Repository.Repositories.Interfaces;

namespace Petadmin.Repository.Repositories
{
    public class DrugRepository : Repository<Drug>, IDrugRepository
    {
        #region Class Initialization
        private readonly IPetadminDbContext _context;
        public DrugRepository(IPetadminDbContext context) : base(context)
        {
            _context = context;
        }
        #endregion

        public Task<IEnumerable<string>> GetDrugsListAsync()
        {
            return _context.GetDrugsListAsync();
        }

        public Task<IEnumerable<string>> GetDrugPeriodsListAsync()
        {
            return _context.GetDrugPeriodsListAsync();
        }

        public Task<IEnumerable<string>> GetDrugFrequencysListAsync()
        {
            return _context.GetDrugFrequencysListAsync();
        }

        public Task<IEnumerable<string>> GetDrugDosagesListAsync()
        {
            return _context.GetDrugDosagesListAsync();
        }
    }
}
