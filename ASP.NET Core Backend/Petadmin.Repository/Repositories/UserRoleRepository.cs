using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Petadmin.Repository.DbContext;
using Petadmin.Repository.Repositories.Interfaces;

namespace Petadmin.Repository.Repositories
{
    /// <summary>
    /// Contains methods for interacting with the user roles backend using 
    /// MySQL via Stored Procedures
    /// </summary>
    public class UserRoleRepository : IUserRoleRepository
    {
        #region Class Initialization
        private readonly IPetadminDbContext _context;
        public UserRoleRepository(IPetadminDbContext context)
        {
            _context = context;
        }
        #endregion

        public Task<List<string>> GetRolesByUserIdAsync(int userId, CancellationToken cancellationToken)
        {
            return _context.GetRolesByUserIdAsync(userId, cancellationToken);
        }

        public Task<int> AddAsync(ApplicationUserRole entity, CancellationToken token)
        {
            return _context.AddUserRoleAsync(entity, token);
        }

        public Task<bool> UpdateAsync(ApplicationUserRole entity, CancellationToken token)
        {
            return _context.UpdateUserRoleAsync(entity, token);
        }

        public Task<bool> RemoveAsync(ApplicationUserRole entity, CancellationToken token)
        {
            return _context.RemoveUserRoleAsync(entity, token);
        }

        public Task<ApplicationUserRole> FindUserRoleAsync(int userId, int roleId, CancellationToken cancellationToken)
        {
            return _context.FindUserRoleAsync(userId, roleId, cancellationToken);
        }
    }
}
