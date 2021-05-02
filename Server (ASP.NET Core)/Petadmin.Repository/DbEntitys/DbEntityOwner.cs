using System;
using System.Threading;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;
using PetAdmin.Core.Interfaces;
using Petadmin.Core.Models;
using Petadmin.Repository.Interfaces;

namespace Petadmin.Repository.DbEntitys
{
    public class DbEntityOwner : IDbEntity
    {
        #region Class Initialization
        private readonly IDbCommon _dbCommon;
        private readonly IDbMappers _dbMappers;
        public DbEntityOwner(IDbCommon dbCommon, IDbMappers dbMappers)
        {
            _dbCommon = dbCommon;
            _dbMappers = dbMappers;
        }
        #endregion


        public bool ShouldServe(IGenericDbEntity genericEntity)
        {
            return genericEntity is Owner;
        }

        #region GET
        public TEntity Get<TEntity>(int id)
        {
            using var connection = new MySqlConnection(_dbCommon.ConnectionString);
            connection.Open();
            var cmd = _dbCommon.ProcedureQuery(connection, "get_owner_by_id");
            cmd.Parameters.AddWithValue("@id_num", id);

            var entity = (TEntity)_dbCommon.GenericReaderToMapper(cmd, _dbMappers.OwnerMapper);

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
            if (!(entity is Owner o)) return 0;

            var ans = 0;

            using var connection = new MySqlConnection(_dbCommon.ConnectionString);
            connection.Open();
            var cmd = _dbCommon.ProcedureQuery(connection, "add_owner");
            cmd.Parameters.AddWithValue("in_id_number", o.IdNumber);
            cmd.Parameters.AddWithValue("in_f_name", o.FirstName);
            cmd.Parameters.AddWithValue("in_l_name", o.LastName);
            cmd.Parameters.AddWithValue("in_birth_date", o.DateOfBirth);
            cmd.Parameters.AddWithValue("in_city", o.City);
            cmd.Parameters.AddWithValue("in_city_2", o.City2);
            cmd.Parameters.AddWithValue("in_street", o.Street);
            cmd.Parameters.AddWithValue("in_street_2", o.Street2);
            cmd.Parameters.AddWithValue("in_house_number", o.HouseNumber);
            cmd.Parameters.AddWithValue("in_house_number_2", o.HouseNumber2);
            cmd.Parameters.AddWithValue("in_apartment_number", o.ApartmentNumber);
            cmd.Parameters.AddWithValue("in_apartment_number_2", o.ApartmentNumber2);
            cmd.Parameters.AddWithValue("in_postal_code", o.PostalCode);
            cmd.Parameters.AddWithValue("in_postal_code_2", o.PostalCode2);
            cmd.Parameters.AddWithValue("in_phone", o.Phone);
            cmd.Parameters.AddWithValue("in_mobile", o.Mobile);
            cmd.Parameters.AddWithValue("in_mailbox", o.MailBox);
            cmd.Parameters.AddWithValue("in_email", o.Email);
            cmd.Parameters.AddWithValue("in_comment", o.Comment);
            cmd.Parameters.AddWithValue("in_owner_user_id", o.User.Id);

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
            if (!(entity is Owner o)) return false;

            using var connection = new MySqlConnection(_dbCommon.ConnectionString);
            connection.Open();
            var cmd = _dbCommon.ProcedureQuery(connection, "update_owner");
            cmd.Parameters.AddWithValue("owner_id_num", o.OwnerId);
            cmd.Parameters.AddWithValue("in_id_number", o.IdNumber);
            cmd.Parameters.AddWithValue("in_f_name", o.FirstName);
            cmd.Parameters.AddWithValue("in_l_name", o.LastName);
            cmd.Parameters.AddWithValue("in_birth_date", o.DateOfBirth);
            cmd.Parameters.AddWithValue("in_city", o.City);
            cmd.Parameters.AddWithValue("in_city_2", o.City2);
            cmd.Parameters.AddWithValue("in_street", o.Street);
            cmd.Parameters.AddWithValue("in_street_2", o.Street2);
            cmd.Parameters.AddWithValue("in_house_number", o.HouseNumber);
            cmd.Parameters.AddWithValue("in_house_number_2", o.HouseNumber2);
            cmd.Parameters.AddWithValue("in_apartment_number", o.ApartmentNumber);
            cmd.Parameters.AddWithValue("in_apartment_number_2", o.ApartmentNumber2);
            cmd.Parameters.AddWithValue("in_postal_code", o.PostalCode);
            cmd.Parameters.AddWithValue("in_postal_code_2", o.PostalCode2);
            cmd.Parameters.AddWithValue("in_phone", o.Phone);
            cmd.Parameters.AddWithValue("in_mobile", o.Mobile);
            cmd.Parameters.AddWithValue("in_mailbox", o.MailBox);
            cmd.Parameters.AddWithValue("in_email", o.Email);
            cmd.Parameters.AddWithValue("in_comment", o.Comment);

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
            if (!(entity is Owner o)) return false;

            var ans = false;

            using var connection = new MySqlConnection(_dbCommon.ConnectionString);
            connection.Open();
            var cmd = _dbCommon.ProcedureQuery(connection, "archive_owner");
            cmd.Parameters.AddWithValue("id_num", o.OwnerId);

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
