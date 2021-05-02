using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Petadmin.Repository.Repositories.Interfaces
{
    /// <summary>
    /// Defines methods for interacting with the userRole backend.
    /// </summary>
    public interface IUserRoleRepository
    {
        /// <summary>
        /// Get list of user roles from database asynchronously. 
        /// </summary>
        /// <param name="userId">user id</param>
        /// <param name="cancellationToken">Task cancellation token</param>
        /// <returns>
        /// Return list of roles names.
        /// </returns>
        Task<List<string>> GetRolesByUserIdAsync(int userId, CancellationToken cancellationToken);

        /// <summary>
        /// Add new UserRole to database asynchronously. 
        /// </summary>
        /// <param name="entity">ApplicationUserRole type</param>
        /// <param name="token">Task cancellation token</param>
        /// <returns>
        /// Return confirmation integer.
        /// </returns>
        Task<int> AddAsync(ApplicationUserRole entity, CancellationToken token);

        /// <summary>
        /// Update entity in database asynchronously.  
        /// </summary>
        /// <param name="entity">ApplicationUserRole type</param>
        /// <param name="token">Task cancellation token</param>
        /// <returns>
        /// Return boolean that indicates if rows was affected.
        /// </returns>
        Task<bool> UpdateAsync(ApplicationUserRole entity, CancellationToken token);

        /// <summary>
        /// Remove entity from database asynchronously. 
        /// </summary>
        /// <param name="entity">ApplicationUserRole type</param>
        /// <param name="token">Task cancellation token</param>
        /// <returns>
        /// Return boolean that indicates if rows was affected.
        /// </returns>
        Task<bool> RemoveAsync(ApplicationUserRole entity, CancellationToken token);

        /// <summary>
        /// Remove entity from database asynchronously. 
        /// </summary>
        /// <param name="userId">user id</param>
        /// <param name="roleId">role id</param>
        /// <param name="cancellationToken">Task cancellation token</param>
        /// <returns>
        /// Return ApplicationUserRole.
        /// </returns>
        Task<ApplicationUserRole> FindUserRoleAsync(int userId, int roleId, CancellationToken cancellationToken);
    }
}
