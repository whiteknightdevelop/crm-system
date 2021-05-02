using System;
using System.Threading;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;
using PetAdmin.Core.Interfaces;
using Petadmin.Core.Models;
using Petadmin.Repository.Interfaces;

namespace Petadmin.Repository.DbEntitys
{
    public class DbEntityApplicationUser : IDbEntity
    {
        #region Class Initialization
        private readonly IDbCommon _dbCommon;
        private readonly IDbMappers _dbMappers;
        public DbEntityApplicationUser(IDbCommon dbCommon, IDbMappers dbMappers)
        {
            _dbCommon = dbCommon;
            _dbMappers = dbMappers;
        }
        #endregion

        public bool ShouldServe(IGenericDbEntity genericEntity)
        {
            return genericEntity is ApplicationUser;
        }

        #region GET
        public TEntity Get<TEntity>(int id)
        {
            using var connection = new MySqlConnection(_dbCommon.ConnectionString);
            connection.Open();
            var cmd = _dbCommon.ProcedureQuery(connection, "get_user_by_id");
            cmd.Parameters.AddWithValue("@in_id_num", id);

            var entity = (TEntity)_dbCommon.GenericReaderToMapper(cmd, _dbMappers.ApplicationUserMapper);

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
            if (!(entity is ApplicationUser u)) return 0;

            var ans = 0;

            using var connection = new MySqlConnection(_dbCommon.ConnectionString);
            connection.Open();
            var cmd = _dbCommon.ProcedureQuery(connection, "add_user");
            cmd.Parameters.AddWithValue("in_username", u.UserName);
            cmd.Parameters.AddWithValue("in_normalized_user_name", u.NormalizedUserName);
            cmd.Parameters.AddWithValue("in_first_name", u.FirstName);
            cmd.Parameters.AddWithValue("in_last_name", u.LastName);
            cmd.Parameters.AddWithValue("in_gender", u.Gender);
            cmd.Parameters.AddWithValue("in_license", u.License);
            cmd.Parameters.AddWithValue("in_email", u.Email);
            cmd.Parameters.AddWithValue("in_normalized_email", u.NormalizedEmail);
            cmd.Parameters.AddWithValue("in_email_confirmed", u.EmailConfirmed);
            cmd.Parameters.AddWithValue("in_phone_number", u.PhoneNumber);
            cmd.Parameters.AddWithValue("in_phone_number_confirmed", u.PhoneNumberConfirmed);
            cmd.Parameters.AddWithValue("in_password_hash", u.PasswordHash);
            cmd.Parameters.AddWithValue("in_security_stamp", u.SecurityStamp);
            cmd.Parameters.AddWithValue("in_concurrency_stamp", u.ConcurrencyStamp);
            cmd.Parameters.AddWithValue("in_two_factor_enabled", u.TwoFactorEnabled);
            cmd.Parameters.AddWithValue("in_lockout_enabled", u.LockoutEnabled);
            cmd.Parameters.AddWithValue("in_access_failed_count", u.AccessFailedCount);

            ans = cmd.ExecuteNonQuery();
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
            if (!(entity is ApplicationUser u)) return false;

            using var connection = new MySqlConnection(_dbCommon.ConnectionString);
            connection.Open();
            var cmd = _dbCommon.ProcedureQuery(connection, "update_user");
            cmd.Parameters.AddWithValue("in_user_id", u.Id);
            cmd.Parameters.AddWithValue("in_username", u.UserName);
            cmd.Parameters.AddWithValue("in_normalized_user_name", u.NormalizedUserName);
            cmd.Parameters.AddWithValue("in_first_name", u.FirstName);
            cmd.Parameters.AddWithValue("in_last_name", u.LastName);
            cmd.Parameters.AddWithValue("in_gender", u.Gender);
            cmd.Parameters.AddWithValue("in_license", u.License);
            cmd.Parameters.AddWithValue("in_email", u.Email);
            cmd.Parameters.AddWithValue("in_normalized_email", u.NormalizedEmail);
            cmd.Parameters.AddWithValue("in_email_confirmed", u.EmailConfirmed);
            cmd.Parameters.AddWithValue("in_phone_number", u.PhoneNumber);
            cmd.Parameters.AddWithValue("in_phone_number_confirmed", u.PhoneNumberConfirmed);
            cmd.Parameters.AddWithValue("in_password_hash", u.PasswordHash);
            cmd.Parameters.AddWithValue("in_security_stamp", u.SecurityStamp);
            cmd.Parameters.AddWithValue("in_concurrency_stamp", u.ConcurrencyStamp);
            cmd.Parameters.AddWithValue("in_two_factor_enabled", u.TwoFactorEnabled);
            cmd.Parameters.AddWithValue("in_lockout_enabled", u.LockoutEnabled);
            cmd.Parameters.AddWithValue("in_access_failed_count", u.AccessFailedCount);
            cmd.Parameters.AddWithValue("in_refresh_token", u.RefreshToken.Token);
            cmd.Parameters.AddWithValue("in_token_expires", u.RefreshToken.Expires);
            cmd.Parameters.AddWithValue("in_remote_ip", u.RefreshToken.RemoteIpAddress);

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
            if (!(entity is ApplicationUser u)) return false;

            var ans = false;

            using var connection = new MySqlConnection(_dbCommon.ConnectionString);
            connection.Open();
            var cmd = _dbCommon.ProcedureQuery(connection, "remove_user");
            cmd.Parameters.AddWithValue("in_user_id", u.Id);

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
