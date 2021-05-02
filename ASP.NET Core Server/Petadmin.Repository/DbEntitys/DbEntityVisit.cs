using System;
using System.Threading;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;
using PetAdmin.Core.Interfaces;
using Petadmin.Core.Models;
using Petadmin.Repository.Interfaces;

namespace Petadmin.Repository.DbEntitys
{
    public class DbEntityVisit : IDbEntity
    {
        #region Class Initialization
        private readonly IDbCommon _dbCommon;
        private readonly IDbMappers _dbMappers;
        public DbEntityVisit(IDbCommon dbCommon, IDbMappers dbMappers)
        {
            _dbCommon = dbCommon;
            _dbMappers = dbMappers;
        }
        #endregion

        public bool ShouldServe(IGenericDbEntity genericEntity)
        {
            return genericEntity is Visit;
        }

        #region GET
        public TEntity Get<TEntity>(int id)
        {
            using var connection = new MySqlConnection(_dbCommon.ConnectionString);
            connection.Open();
            var cmd = _dbCommon.ProcedureQuery(connection, "get_visit_by_id");
            cmd.Parameters.AddWithValue("@id_num", id);

            var entity = (TEntity)_dbCommon.GenericReaderToMapper(cmd, _dbMappers.VisitMapper);

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
            if (!(entity is Visit v)) return 0;

            var ans = 0;

            using var connection = new MySqlConnection(_dbCommon.ConnectionString);
            connection.Open();
            var cmd = _dbCommon.ProcedureQuery(connection, "add_visit");
            cmd.Parameters.AddWithValue("in_animal_id", v.AnimalId);
            cmd.Parameters.AddWithValue("in_visit_time", v.VisitTime);
            cmd.Parameters.AddWithValue("in_cause", v.Cause);
            cmd.Parameters.AddWithValue("in_symptoms", v.Symptoms);
            cmd.Parameters.AddWithValue("in_visit_lab_result", v.LabResults);
            cmd.Parameters.AddWithValue("in_comment", v.Comment);
            cmd.Parameters.AddWithValue("in_temperature", v.Temperature);
            cmd.Parameters.AddWithValue("in_weight", v.Weight);
            cmd.Parameters.AddWithValue("in_pulse", v.Pulse);
            cmd.Parameters.AddWithValue("in_diagnosis_1", v.Diagnosis1);
            cmd.Parameters.AddWithValue("in_diagnosis_2", v.Diagnosis2);
            cmd.Parameters.AddWithValue("in_diagnosis_3", v.Diagnosis3);
            cmd.Parameters.AddWithValue("in_treatment_1", v.Treatment1);
            cmd.Parameters.AddWithValue("in_treatment_2", v.Treatment2);
            cmd.Parameters.AddWithValue("in_treatment_3", v.Treatment3);
            cmd.Parameters.AddWithValue("in_treatment_4", v.Treatment4);
            cmd.Parameters.AddWithValue("in_treatment_5", v.Treatment5);
            cmd.Parameters.AddWithValue("in_treatment_6", v.Treatment6);
            cmd.Parameters.AddWithValue("in_visit_user_id", v.User.Id);

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
            if (!(entity is Visit v)) return false;

            using var connection = new MySqlConnection(_dbCommon.ConnectionString);
            connection.Open();
            var cmd = _dbCommon.ProcedureQuery(connection, "update_visit");
            cmd.Parameters.AddWithValue("in_visit_id", v.VisitId);
            cmd.Parameters.AddWithValue("in_animal_id", v.AnimalId);
            cmd.Parameters.AddWithValue("in_visit_time", v.VisitTime);
            cmd.Parameters.AddWithValue("in_cause", v.Cause);
            cmd.Parameters.AddWithValue("in_symptoms", v.Symptoms);
            cmd.Parameters.AddWithValue("in_visit_lab_result", v.LabResults);
            cmd.Parameters.AddWithValue("in_comment", v.Comment);
            cmd.Parameters.AddWithValue("in_temperature", v.Temperature);
            cmd.Parameters.AddWithValue("in_weight", v.Weight);
            cmd.Parameters.AddWithValue("in_pulse", v.Pulse);
            cmd.Parameters.AddWithValue("in_diagnosis_1", v.Diagnosis1);
            cmd.Parameters.AddWithValue("in_diagnosis_2", v.Diagnosis2);
            cmd.Parameters.AddWithValue("in_diagnosis_3", v.Diagnosis3);
            cmd.Parameters.AddWithValue("in_treatment_1", v.Treatment1);
            cmd.Parameters.AddWithValue("in_treatment_2", v.Treatment2);
            cmd.Parameters.AddWithValue("in_treatment_3", v.Treatment3);
            cmd.Parameters.AddWithValue("in_treatment_4", v.Treatment4);
            cmd.Parameters.AddWithValue("in_treatment_5", v.Treatment5);
            cmd.Parameters.AddWithValue("in_treatment_6", v.Treatment6);

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
            if (!(entity is Visit v)) return false;

            var ans = false;

            using var connection = new MySqlConnection(_dbCommon.ConnectionString);
            connection.Open();
            var cmd = _dbCommon.ProcedureQuery(connection, "archive_visit");
            cmd.Parameters.AddWithValue("in_id_visit", v.VisitId);

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
