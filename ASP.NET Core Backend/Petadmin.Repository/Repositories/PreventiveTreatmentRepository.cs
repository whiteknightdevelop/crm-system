using Petadmin.Core.Models;
using Petadmin.Repository.DbContext;
using Petadmin.Repository.Repositories.Interfaces;

namespace Petadmin.Repository.Repositories
{
    /// <summary>
    /// Contains methods for interacting with the preventive treatment backend using 
    /// MySQL via Stored Procedures
    /// </summary>
    public class PreventiveTreatmentRepository : Repository<PreventiveTreatment>, IPreventiveTreatmentRepository
    {
        private readonly IPetadminDbContext _context;
        public PreventiveTreatmentRepository(IPetadminDbContext context) : base(context)
        {
            _context = context;
        }
    }
}
