using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Petadmin.Core.Models;

namespace Petadmin.Repository.Repositories.Interfaces
{
    public interface IUserRepository : IRepository<ApplicationUser>
    {
        Task<ApplicationUser> FindByUserNameAsync(string normalizedUserName, CancellationToken cancellationToken);
        Task<IEnumerable<string>> GetGendersListAsync();
    }
}
