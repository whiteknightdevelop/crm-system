using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Petadmin.Core.Models;
using Petadmin.Repository.DbContext;
using Petadmin.Repository.Repositories.Interfaces;

namespace Petadmin.Repository.Repositories
{
    public class UserRepository : Repository<ApplicationUser>, IUserRepository
    {
        #region Class Initialization
        private readonly IPetadminDbContext _context;
        public UserRepository(IPetadminDbContext context) : base(context)
        {
            _context = context;
        }
        #endregion

        public Task<ApplicationUser> FindByUserNameAsync(string normalizedUserName, CancellationToken cancellationToken)
        {
            return _context.FindByUserNameAsync(normalizedUserName, cancellationToken);
        }

        public Task<IEnumerable<string>> GetGendersListAsync()
        {
            return _context.GetGendersListAsync();
        }
    }
}
