using System.Threading;
using System.Threading.Tasks;
using PetAdmin.Core.Interfaces;

namespace Petadmin.Repository.DbEntitys
{
    public interface IDbEntity
    {
        /// <summary>
        /// Check if the entity need to be served
        /// </summary>
        /// <param name="genericEntity">IGenericDbEntity type</param>
        /// <returns>
        /// Return Bool Type.
        /// </returns>
        bool ShouldServe(IGenericDbEntity genericEntity);

        TEntity Get<TEntity>(int id);
        Task<TEntity> GetAsync<TEntity>(int id);
        int Add<TEntity>(TEntity entity);
        Task<int> AddAsync<TEntity>(TEntity entity);
        bool Update<TEntity>(TEntity entity);
        Task<bool> UpdateAsync<TEntity>(TEntity entity);
        bool Remove<TEntity>(TEntity entity);
        Task<bool> RemoveAsync<TEntity>(TEntity entity);
        Task<bool> RemoveAsync<TEntity>(TEntity entity, CancellationToken token);
        Task<bool> RemoveAsync<TEntity>(TEntity entity, CancellationTokenSource cts, in CancellationToken token);
    }
}
