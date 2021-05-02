using System.Threading;
using System.Threading.Tasks;
using Petadmin.Core.Models;
using Petadmin.Repository.DbContext;
using Petadmin.Repository.Repositories.Interfaces;

namespace Petadmin.Repository.Repositories
{
    /// <summary>
    /// Contains methods for interacting with the user roles backend using 
    /// MySQL via Stored Procedures
    /// </summary>
    public class RoleRepository : Repository<ApplicationRole>, IRoleRepository
    {
        #region Class Initialization
        private readonly IPetadminDbContext _context;
        public RoleRepository(IPetadminDbContext context) : base(context)
        {
            _context = context;
        }
        #endregion

        public Task<ApplicationRole> FindByRoleNameAsync(string normalizedRoleName, CancellationToken cancellationToken)
        {
            return _context.FindByRoleNameAsync(normalizedRoleName, cancellationToken);
        }
    }
}
