using System.Collections.Generic;
using System.Threading.Tasks;
using Petadmin.Core.Models;
using Petadmin.Repository.DbContext;
using Petadmin.Repository.Repositories.Interfaces;

namespace Petadmin.Repository.Repositories
{
    public class DebtRepository : Repository<Debt>, IDebtRepository
    {
        #region Class Initialization
        private readonly IPetadminDbContext _context;
        public DebtRepository(IPetadminDbContext context) : base(context)
        {
            _context = context;
        }
        #endregion

        public Task<IEnumerable<Debt>> GetDebtsListByOwnerIdAsync(int ownerId)
        {
            return _context.GetDebtsListByOwnerIdAsync(ownerId);
        }
    }
}
