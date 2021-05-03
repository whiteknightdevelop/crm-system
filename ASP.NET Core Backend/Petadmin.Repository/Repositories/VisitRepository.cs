using Petadmin.Core.Models;
using Petadmin.Repository.DbContext;
using Petadmin.Repository.Repositories.Interfaces;

namespace Petadmin.Repository.Repositories
{
    /// <summary>
    /// Contains methods for interacting with the visit backend using 
    /// MySQL via Stored Procedures
    /// </summary>
    public class VisitRepository : Repository<Visit>, IVisitRepository
    {
        private readonly IPetadminDbContext _context;
        public VisitRepository(IPetadminDbContext context) : base(context)
        {
            _context = context;
        }
    }
}
