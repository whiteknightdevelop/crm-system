using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;
using PetAdmin.Core.Interfaces;

namespace Petadmin.Repository.DbContext
{
    public partial class PetadminContext
    {
        /// <summary>
        /// Get
        /// </summary>
        public TEntity Get<TEntity>(int id) where TEntity : class, new()
        {
            TEntity obj = null;
            foreach (var entity in _entitiesList)
            {
                if (entity.ShouldServe(new TEntity() as IGenericDbEntity))
                {
                    obj = entity.Get<TEntity>(id);
                }
            }
            return obj;
        }

        /// <summary>
        /// Get Async
        /// </summary>
        public Task<TEntity> GetAsync<TEntity>(int id) where TEntity : class, new()
        {
            Task<TEntity> ans = null;
            foreach (var entity in _entitiesList)
            {
                if (entity.ShouldServe(new TEntity() as IGenericDbEntity))
                {
                    ans = entity.GetAsync<TEntity>(id);
                }
            }
            return ans;
        }

        /// <summary>
        /// Add
        /// </summary>
        public int Add<TEntity>(TEntity e) where TEntity : class, new()
        {
            var obj = 0;
            foreach (var entity in _entitiesList)
            {
                if (entity.ShouldServe(new TEntity() as IGenericDbEntity))
                {
                    obj = entity.Add(e);
                }
            }
            return obj;
        }

        /// <summary>
        /// Add Async
        /// </summary>
        public Task<int> AddAsync<TEntity>(TEntity e) where TEntity : class, new()
        {
            Task<int> ans = null;
            foreach (var entity in _entitiesList)
            {
                if (entity.ShouldServe(new TEntity() as IGenericDbEntity))
                {
                    ans = entity.AddAsync(e);
                }
            }
            return ans;
        }

        /// <summary>
        /// Update
        /// </summary>
        public bool Update<TEntity>(TEntity e) where TEntity : class, new()
        {
            var ans = false;
            foreach (var entity in _entitiesList)
            {
                if (entity.ShouldServe(new TEntity() as IGenericDbEntity))
                {
                    ans = entity.Update(e);
                }
            }
            return ans;
        }

        /// <summary>
        /// Update Async
        /// </summary>
        public Task<bool> UpdateAsync<TEntity>(TEntity e) where TEntity : class, new()
        {
            Task<bool> ans = null;
            foreach (var entity in _entitiesList)
            {
                if (entity.ShouldServe(new TEntity() as IGenericDbEntity))
                {
                    ans = entity.UpdateAsync(e);
                }
            }
            return ans;
        }

        /// <summary>
        /// Remove
        /// </summary>
        public bool Remove<TEntity>(TEntity e) where TEntity : class, new()
        {
            var ans = false;
            foreach (var entity in _entitiesList)
            {
                if (entity.ShouldServe(new TEntity() as IGenericDbEntity))
                {
                    ans = entity.Remove(e);
                }
            }
            return ans;
        }

        /// <summary>
        /// Remove Async
        /// </summary>
        public Task<bool> RemoveAsync<TEntity>(TEntity e) where TEntity : class, new()
        {
            Task<bool> ans = null;
            foreach (var entity in _entitiesList)
            {
                if (entity.ShouldServe(new TEntity() as IGenericDbEntity))
                {
                    ans = entity.RemoveAsync(e);
                }
            }
            return ans;
        }

        public Task<bool> RemoveAsync<TEntity>(TEntity e, CancellationToken token) where TEntity : class, new()
        {
            Task<bool> ans = null;
            foreach (var entity in _entitiesList)
            {
                if (entity.ShouldServe(new TEntity() as IGenericDbEntity))
                {
                    ans = entity.RemoveAsync(e, token);
                }
            }
            return ans;
        }

        public Task<bool> RemoveAsync<TEntity>(TEntity e, CancellationTokenSource cts, CancellationToken token) where TEntity : class, new()
        {
            Task<bool> ans = null;
            foreach (var entity in _entitiesList)
            {
                if (entity.ShouldServe(new TEntity() as IGenericDbEntity))
                {
                    ans = entity.RemoveAsync(e, cts, token);
                }
            }
            return ans;
        }

        private IEnumerable<T> GetListFromDbAsync<T>(string procedureName, Func<MySqlDataReader, T> mapper, IDictionary<string, object> dictionary = null)
        {
            using var connection = new MySqlConnection(_dbCommon.ConnectionString);
            connection.Open();
            var cmd = _dbCommon.ProcedureQuery(connection, procedureName);

            if (dictionary != null)
                foreach (var (key, value) in dictionary)
                {
                    cmd.Parameters.AddWithValue(key, value);
                }

            var list = _dbCommon.GenericReaderToMapperList(cmd, mapper);

            connection.Close();
            return list;
        }
    }
}
