using System.Collections.Generic;
using System.Threading.Tasks;
using Petadmin.Core.Models;
using Petadmin.Repository.DbContext;
using Petadmin.Repository.Repositories.Interfaces;

namespace Petadmin.Repository.Repositories
{
    /// <summary>
    /// Contains methods for interacting with the diagnosis backend using 
    /// MySQL via Stored Procedures
    /// </summary>
    public class TreatmentRepository : Repository<Treatment>, ITreatmentRepository
    {
        private readonly IPetadminDbContext _context;
        public TreatmentRepository(IPetadminDbContext context) : base(context)
        {
            _context = context;
        }

        public Task<IEnumerable<Treatment>> GetTreatmentListAsync()
        {
            return _context.GetTreatmentListAsync();
        }

        public Task<IEnumerable<PreventiveTreatment>> GetPreventiveTreatmentsListByVisitIdAsync(int visitId)
        {
            return _context.GetPreventiveTreatmentsListByVisitIdAsync(visitId);
        }

        public Task<IEnumerable<PreventiveTreatment>> GetPreventiveTreatmentsListAsync()
        {
            return _context.GetPreventiveTreatmentsListAsync();
        }
    }
}
