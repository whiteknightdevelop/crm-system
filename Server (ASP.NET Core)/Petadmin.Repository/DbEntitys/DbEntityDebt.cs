using System;
using System.Threading;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;
using PetAdmin.Core.Interfaces;
using Petadmin.Core.Models;
using Petadmin.Repository.Interfaces;

namespace Petadmin.Repository.DbEntitys
{
    public class DbEntityDebt : IDbEntity
    {
        #region Class Initialization
        private readonly IDbCommon _dbCommon;
        private readonly IDbMappers _dbMappers;
        public DbEntityDebt(IDbCommon dbCommon, IDbMappers dbMappers)
        {
            _dbCommon = dbCommon;
            _dbMappers = dbMappers;
        }
        #endregion

        public bool ShouldServe(IGenericDbEntity genericEntity)
        {
            return genericEntity is Debt;
        }

        #region GET
        public TEntity Get<TEntity>(int id)
        {
            using var connection = new MySqlConnection(_dbCommon.ConnectionString);
            connection.Open();
            var cmd = _dbCommon.ProcedureQuery(connection, "get_debt_by_id");
            cmd.Parameters.AddWithValue("@in_id_num", id);

            var entity = (TEntity)_dbCommon.GenericReaderToMapper(cmd, _dbMappers.DebtMapper);

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
            if (!(entity is Debt d)) return 0;

            var ans = 0;

            using var connection = new MySqlConnection(_dbCommon.ConnectionString);
            connection.Open();
            var cmd = _dbCommon.ProcedureQuery(connection, "add_debt");
            cmd.Parameters.AddWithValue("in_owner_id_num", d.OwnerId);
            cmd.Parameters.AddWithValue("in_animal_name", d.AnimalName);
            cmd.Parameters.AddWithValue("in_cause_of_debt", d.Cause);
            cmd.Parameters.AddWithValue("in_debt_amount", d.DebtAmount);
            cmd.Parameters.AddWithValue("in_paid", d.PaidAmount);
            cmd.Parameters.AddWithValue("in_debt_date", d.DebtDate);

            using (var reader = cmd.ExecuteReader())
            {
                while (reader.Read())
                {
                    ans = reader.IsDBNull(0) ? 0 : reader.GetInt32("LAST_INSERT_ID()");
                }
            }

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
            if (!(entity is Debt d)) return false;

            using var connection = new MySqlConnection(_dbCommon.ConnectionString);
            connection.Open();
            var cmd = _dbCommon.ProcedureQuery(connection, "update_debt");
            cmd.Parameters.AddWithValue("in_debt_id", d.DebtId);
            cmd.Parameters.AddWithValue("in_owner_id_num", d.OwnerId);
            cmd.Parameters.AddWithValue("in_animal_name", d.AnimalName);
            cmd.Parameters.AddWithValue("in_cause_of_debt", d.Cause);
            cmd.Parameters.AddWithValue("in_debt_amount", d.DebtAmount);
            cmd.Parameters.AddWithValue("in_paid", d.PaidAmount);
            cmd.Parameters.AddWithValue("in_debt_date", d.DebtDate);

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
            if (!(entity is Debt d)) return false;

            var ans = false;

            using var connection = new MySqlConnection(_dbCommon.ConnectionString);
            connection.Open();
            var cmd = _dbCommon.ProcedureQuery(connection, "remove_debt");
            cmd.Parameters.AddWithValue("in_debt_id", d.DebtId);
            cmd.Parameters.AddWithValue("in_owner_id", d.OwnerId);

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
