using System.Threading;
using System.Threading.Tasks;
using Petadmin.Core.Models;

namespace Petadmin.Repository.Repositories.Interfaces
{ 
    /// <summary>
    /// Defines methods for interacting with the roles backend.
    /// </summary>
    public interface IRoleRepository : IRepository<ApplicationRole>
    {
        Task<ApplicationRole> FindByRoleNameAsync(string normalizedRoleName, CancellationToken cancellationToken);
    }
}
