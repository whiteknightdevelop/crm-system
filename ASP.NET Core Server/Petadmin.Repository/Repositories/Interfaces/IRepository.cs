using System.Threading;
using System.Threading.Tasks;

namespace Petadmin.Repository.Repositories.Interfaces
{
    public interface IRepository<TEntity> where TEntity : class
    {

        /// <summary>
        /// Returns database entity by id. 
        /// </summary>
        /// <param name="id">integer type</param>
        /// <returns>
        /// Return Task of TEntity.
        /// </returns>
        TEntity Get(int id);

        /// <summary>
        /// Returns database entity by id asynchronously. 
        /// </summary>
        /// <param name="id">integer type</param>
        /// <returns>
        /// Return Task of TEntity.
        /// </returns>
        Task<TEntity> GetAsync(int id);

        /// <summary>
        /// Add new entity to database. 
        /// </summary>
        /// <param name="entity">TEntity type</param>
        /// <returns>
        /// Return confirmation integer - LAST_INSERT_ID().
        /// </returns>
        int Add(TEntity entity);

        /// <summary>
        /// Add new entity to database asynchronously. 
        /// </summary>
        /// <param name="entity">TEntity type</param>
        /// <returns>
        /// Return confirmation integer - LAST_INSERT_ID().
        /// </returns>
        Task<int> AddAsync(TEntity entity);

        /// <summary>
        /// Update entity in database. 
        /// </summary>
        /// <param name="entity">TEntity type</param>
        /// <returns>
        /// Return boolean that indicates if number of rows was affected.
        /// </returns>
        bool Update(TEntity entity);

        /// <summary>
        /// Update entity in database asynchronously.  
        /// </summary>
        /// <param name="entity">TEntity type</param>
        /// <returns>
        /// Return boolean that indicates if number of rows was affected.
        /// </returns>
        Task<bool> UpdateAsync(TEntity entity);

        /// <summary>
        /// Remove entity from database. 
        /// </summary>
        /// <param name="entity">TEntity type</param>
        /// <returns>
        /// Return boolean that indicates if number of rows was affected.
        /// </returns>
        bool Remove(TEntity entity);

        /// <summary>
        /// Remove entity from database asynchronously. 
        /// </summary>
        /// <param name="entity">TEntity type</param>
        /// <returns>
        /// Return boolean that indicates if number of rows was affected.
        /// </returns>
        Task<bool> RemoveAsync(TEntity entity);
        Task<bool> RemoveAsync(TEntity entity, CancellationToken token);
        Task<bool> RemoveAsync(TEntity entity, CancellationTokenSource cts, in CancellationToken token);
    }
}