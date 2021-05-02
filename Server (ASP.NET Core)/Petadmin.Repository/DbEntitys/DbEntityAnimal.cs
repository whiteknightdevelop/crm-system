using System;
using System.Threading;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;
using PetAdmin.Core.Interfaces;
using PetAdmin.Core.Models;
using Petadmin.Repository.Interfaces;

namespace Petadmin.Repository.DbEntitys
{
    public class DbEntityAnimal : IDbEntity
    {
        #region Class Initialization
        private readonly IDbCommon _dbCommon;
        private readonly IDbMappers _dbMappers;
        public DbEntityAnimal(IDbCommon dbCommon, IDbMappers dbMappers)
        {
            _dbCommon = dbCommon;
            _dbMappers = dbMappers;
        }
        #endregion

        public bool ShouldServe(IGenericDbEntity genericEntity)
        {
            return genericEntity is Animal;
        }

        #region GET
        public TEntity Get<TEntity>(int id)
        {
            using var connection = new MySqlConnection(_dbCommon.ConnectionString);
            connection.Open();
            var cmd = _dbCommon.ProcedureQuery(connection, "get_animal_by_id");
            cmd.Parameters.AddWithValue("@id_num", id);

            var entity = (TEntity)_dbCommon.GenericReaderToMapper(cmd, _dbMappers.AnimalMapper);

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
            if (!(entity is Animal a)) return 0;

            var ans = 0;

            using var connection = new MySqlConnection(_dbCommon.ConnectionString);
            connection.Open();
            var cmd = _dbCommon.ProcedureQuery(connection, "add_animal");
            cmd.Parameters.AddWithValue("in_owner_id", a.OwnerId);
            cmd.Parameters.AddWithValue("in_name", a.Name);
            cmd.Parameters.AddWithValue("in_type", a.Type);
            cmd.Parameters.AddWithValue("in_breed", a.Breed);
            cmd.Parameters.AddWithValue("in_color", a.Color);
            cmd.Parameters.AddWithValue("in_gender", a.Gender);
            cmd.Parameters.AddWithValue("in_date_of_birth", a.DateOfBirth);
            cmd.Parameters.AddWithValue("in_sterilized", a.Sterilized);
            cmd.Parameters.AddWithValue("in_date_of_sterilization", a.DateOfSterilization);
            cmd.Parameters.AddWithValue("in_chip_number", a.ChipNumber);
            cmd.Parameters.AddWithValue("in_chip_mark_date", a.ChipMarkDate);
            cmd.Parameters.AddWithValue("in_comment", a.Comment);
            cmd.Parameters.AddWithValue("in_animal_user_id", a.User.Id);

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
            if (!(entity is Animal a)) return false;

            using var connection = new MySqlConnection(_dbCommon.ConnectionString);
            connection.Open();
            var cmd = _dbCommon.ProcedureQuery(connection, "update_animal");
            cmd.Parameters.AddWithValue("in_animal_id", a.AnimalId);
            cmd.Parameters.AddWithValue("in_owner_id", a.OwnerId);
            cmd.Parameters.AddWithValue("in_name", a.Name);
            cmd.Parameters.AddWithValue("in_type", a.Type);
            cmd.Parameters.AddWithValue("in_breed", a.Breed);
            cmd.Parameters.AddWithValue("in_color", a.Color);
            cmd.Parameters.AddWithValue("in_gender", a.Gender);
            cmd.Parameters.AddWithValue("in_date_of_birth", a.DateOfBirth);
            cmd.Parameters.AddWithValue("in_active", a.Active);
            cmd.Parameters.AddWithValue("in_sterilized", a.Sterilized);
            cmd.Parameters.AddWithValue("in_date_of_sterilization", a.DateOfSterilization);
            cmd.Parameters.AddWithValue("in_chip_number", a.ChipNumber);
            cmd.Parameters.AddWithValue("in_chip_mark_date", a.ChipMarkDate);
            cmd.Parameters.AddWithValue("in_comment", a.Comment);

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
            if (!(entity is Animal a)) return false;

            var ans = false;

            using var connection = new MySqlConnection(_dbCommon.ConnectionString);
            connection.Open();
            var cmd = _dbCommon.ProcedureQuery(connection, "archive_animal");
            cmd.Parameters.AddWithValue("in_id_animal", a.AnimalId);

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
