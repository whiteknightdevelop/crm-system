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
    public class DiagnosisRepository : Repository<Diagnosis>, IDiagnosisRepository
    {
        private readonly IPetadminDbContext _context;
        public DiagnosisRepository(IPetadminDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Diagnosis>> GetDiagnosesListAsync()
        {
            return await _context.GetDiagnosesListAsync();
        }
    }
}
