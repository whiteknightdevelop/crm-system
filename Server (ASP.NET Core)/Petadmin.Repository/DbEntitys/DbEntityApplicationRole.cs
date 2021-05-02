using System;
using System.Threading;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;
using PetAdmin.Core.Interfaces;
using Petadmin.Core.Models;
using Petadmin.Repository.Interfaces;

namespace Petadmin.Repository.DbEntitys
{
    public class DbEntityApplicationRole : IDbEntity
    {
        #region Class Initialization
        private readonly IDbCommon _dbCommon;
        private readonly IDbMappers _dbMappers;
        public DbEntityApplicationRole(IDbCommon dbCommon, IDbMappers dbMappers)
        {
            _dbCommon = dbCommon;
            _dbMappers = dbMappers;
        }
        #endregion

        public bool ShouldServe(IGenericDbEntity genericEntity)
        {
            return genericEntity is ApplicationRole;
        }

        #region GET
        public TEntity Get<TEntity>(int id)
        {
            using var connection = new MySqlConnection(_dbCommon.ConnectionString);
            connection.Open();
            var cmd = _dbCommon.ProcedureQuery(connection, "get_role_by_id");
            cmd.Parameters.AddWithValue("@in_id_num", id);

            var entity = (TEntity)_dbCommon.GenericReaderToMapper(cmd, _dbMappers.ApplicationRoleMapper);

            connection.Close();
            return entity;
        }
        public Task<TEntity> GetAsync<TEntity>(int id)
        {
            return Task.Run(() => Get<TEntity>(id));
        }
        #endregion

        #region Add
        public int Add<TEntity>(TEntity entity)
        {
            if (!(entity is ApplicationRole r)) return 0;

            using var connection = new MySqlConnection(_dbCommon.ConnectionString);
            connection.Open();
            var cmd = _dbCommon.ProcedureQuery(connection, "add_role");
            cmd.Parameters.AddWithValue("in_name", r.Name);
            cmd.Parameters.AddWithValue("in_normalized_name", r.NormalizedName);
            cmd.Parameters.AddWithValue("in_concurrency_stamp", r.ConcurrencyStamp);

            int ans = cmd.ExecuteNonQuery();
            connection.Close();
            return ans;
        }
        public Task<int> AddAsync<TEntity>(TEntity entity)
        {
            return Task.Run(() => Add(entity));
        }
        #endregion

        #region Update
        public bool Update<TEntity>(TEntity entity)
        {
            if (!(entity is ApplicationRole r)) return false;

            using var connection = new MySqlConnection(_dbCommon.ConnectionString);
            connection.Open();
            var cmd = _dbCommon.ProcedureQuery(connection, "update_role");
            cmd.Parameters.AddWithValue("in_role_id", r.Id);
            cmd.Parameters.AddWithValue("in_name", r.Name);
            cmd.Parameters.AddWithValue("in_normalized_name", r.NormalizedName);
            cmd.Parameters.AddWithValue("in_concurrency_stamp", r.ConcurrencyStamp);

            var ans = Convert.ToBoolean(cmd.ExecuteNonQuery());
            connection.Close();
            return ans;
        }
        public Task<bool> UpdateAsync<TEntity>(TEntity entity)
        { 
            return Task.Run(() => Update(entity));
        }
        #endregion

        #region Remove
        public bool Remove<TEntity>(TEntity entity)
        {
            if (!(entity is ApplicationRole r)) return false;

            var ans = false;

            using var connection = new MySqlConnection(_dbCommon.ConnectionString);
            connection.Open();
            var cmd = _dbCommon.ProcedureQuery(connection, "remove_role");
            cmd.Parameters.AddWithValue("in_role_id", r.Id);

            using (var reader = cmd.ExecuteReader())
            {
                while (reader.Read())
                {
                    ans = reader.IsDBNull(0) || reader.GetBoolean("response");
                }
            }
            connection.Close();
            return ans;
        }
        public Task<bool> RemoveAsync<TEntity>(TEntity entity)
        {
            return Task.Run(() => Remove(entity));
        }
        public Task<bool> RemoveAsync<TEntity>(TEntity entity, CancellationToken token)
        {
            if (token.IsCancellationRequested)
            {
                Console.WriteLine("Task {0} was cancelled before it got started.");
                token.ThrowIfCancellationRequested();
            }

            return Task.Run(() => Remove(entity), token);
        }
        public Task<bool> RemoveAsync<TEntity>(TEntity entity, CancellationTokenSource cts, in CancellationToken token)
        {
            if (token.IsCancellationRequested)
            {
                Console.WriteLine("{0} - RemoveAsync: Task was cancelled before it got started.", nameof(TEntity));
                token.ThrowIfCancellationRequested();
            }

            return Task.Run(() =>
            {
                bool ans = Remove(entity);
                if (!ans)
                {
                    cts.Cancel();
                }
                return ans;
            }, token);
        }
        #endregion
    }
}
