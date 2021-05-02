using System.Collections.Generic;
using System.Threading.Tasks;
using Petadmin.Core.Models;
using Petadmin.Repository.DbContext;
using Petadmin.Repository.Repositories.Interfaces;

namespace Petadmin.Repository.Repositories
{
    public class PrescriptionRepository : Repository<Prescription>, IPrescriptionRepository
    {
        #region Class Initialization
        private readonly IPetadminDbContext _context;
        public PrescriptionRepository(IPetadminDbContext context) : base(context)
        {
            _context = context;
        }
        #endregion

        public Task<IEnumerable<Prescription>> GetPrescriptionsListByVisitIdAsync(int visitId)
        {
            return _context.GetPrescriptionsListByVisitIdAsync(visitId);
        }

        public Task<int> GetVisitPrescriptionsNumberAsync(int visitId)
        {
            return _context.GetVisitPrescriptionsNumberAsync(visitId);
        }
    }
}
