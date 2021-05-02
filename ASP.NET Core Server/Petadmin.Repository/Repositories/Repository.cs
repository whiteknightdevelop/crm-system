using System.Threading;
using System.Threading.Tasks;
using Petadmin.Repository.DbContext;
using Petadmin.Repository.Repositories.Interfaces;

namespace Petadmin.Repository.Repositories
{
    public class Repository<TEntity> : IRepository<TEntity> where TEntity : class, new()
    {
        private readonly IPetadminDbContext _context;
        public Repository(IPetadminDbContext context)
        {
            _context = context;
        }

        public TEntity Get(int id)
        {
            return _context.Get<TEntity>(id);
        }
        public Task<TEntity> GetAsync(int id)
        {
            return _context.GetAsync<TEntity>(id);
        }

        public int Add(TEntity entity)
        {
            return _context.Add<TEntity>(entity);
        }

        public Task<int> AddAsync(TEntity entity)
        {
            return _context.AddAsync<TEntity>(entity);
        }

        public bool Update(TEntity entity)
        {
            return _context.Update<TEntity>(entity);
        }

        public Task<bool> UpdateAsync(TEntity entity)
        {
            return _context.UpdateAsync<TEntity>(entity);
        }

        public bool Remove(TEntity entity)
        {
            return _context.Remove<TEntity>(entity);
        }

        public Task<bool> RemoveAsync(TEntity entity)
        {
            return _context.RemoveAsync<TEntity>(entity);
        }

        public Task<bool> RemoveAsync(TEntity entity, CancellationToken token)
        {
            return _context.RemoveAsync<TEntity>(entity, token);
        }

        public Task<bool> RemoveAsync(TEntity entity, CancellationTokenSource cts, in CancellationToken token)
        {
            return _context.RemoveAsync<TEntity>(entity, cts, token);
        }
    }
}
